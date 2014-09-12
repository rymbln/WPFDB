using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.View;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class AbstractWorkspaceViewModel : ViewModelBase
    {
        private Abstract currentAbstract { get; set; }
        private AbstractViewModel currentAbstractVM { get; set; }
        private Conference conferenceFilter { get; set; }
        private bool isFilterConference;

        private string filterText = "";

        public AbstractWorkspaceViewModel()
        {

            AllAbstracts = new ObservableCollection<Abstract>();
            AllAbstractsDB = new ObservableCollection<Abstract>();

       

            ConferenceLookup = new ObservableCollection<Conference>(DataManager.Instance.GetAllConferences());
            ConferenceFilter = DefaultManager.Instance.DefaultConference;
            IsFilterConference = true;
         //   OnPropertyChanged("IsFilterConference");
       //     RefreshAbstracts();

            this.SelectFileCommand = new DelegateCommand(o => SelectFile());
            this.OpenFolderCommand = new DelegateCommand(o => OpenFolder());
            this.OpenFileCommand = new DelegateCommand(o => OpenFile());

            this.OpenAbstractCommand = new DelegateCommand((o) => this.OpenAbstract());
            this.DeleteAbstractCommand = new DelegateCommand((o) => this.DeleteAbstract());
            this.RefreshCommand = new DelegateCommand((o) => this.RefreshAbstracts());

            AbstractToWordCommand = new DelegateCommand(o => AbstractToWord(), o => CurrentAbstract != null);
            AllAbstractToWordCommand = new DelegateCommand(o => AllAbstractToWord());
            PosterEmailCommand = new DelegateCommand(o => PosterEmail());
            ToogleConferenceFilterCommand = new DelegateCommand(o => ToggleConferenceFilter());


        }
        public ObservableCollection<Abstract> AllAbstracts { get; private set; }
        public ObservableCollection<Abstract> AllAbstractsDB { get; private set; }
        public ObservableCollection<Conference> ConferenceLookup { get; private set; }

        public ICommand RefreshCommand { get; private set; }
        public ICommand ApplyFiltersCommand { get; private set; }


        public ICommand OpenFolderCommand { get; private set; }
        public ICommand OpenFileCommand { get; private set; }
        public ICommand SelectFileCommand { get; private set; }

        public ICommand OpenAbstractCommand { get; private set; }
        public ICommand DeleteAbstractCommand { get; private set; }

        public ICommand AbstractToWordCommand { get; private set; }
        public ICommand AllAbstractToWordCommand { get; private set; }
        public ICommand PosterEmailCommand { get; private set; }

        public ICommand ToogleConferenceFilterCommand { get; private set; }

        private void ToggleConferenceFilter()
        {
            IsFilterConference = !IsFilterConference;
        }

        public Conference ConferenceFilter
        {
            get { return this.conferenceFilter; }
            set
            {
                conferenceFilter = value;
                if (IsFilterConference)
                {
                    FilterAbstracts();
                }
                OnPropertyChanged("ConferenceFilter");
            }
        }

        public bool IsFilterConference
        {
            get
            {
                return isFilterConference;

            }
            set
            {
                isFilterConference = value;
                OnPropertyChanged("IsFilterConference");
                if (isFilterConference)
                {
                    FilterAbstracts();
                }
                else
                {
                    RefreshAbstracts();
                }
            }
        }

        private void AbstractToWord()
        {
            var filePath = WordManager.AbstractToWord(CurrentAbstract);
            MessageBoxResult messageBoxResult = MessageBox.Show("Файл создан по адресу " + filePath + ". Открыть?", "Информация", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Process.Start("explorer.exe", filePath);
            }
        }

        private void AllAbstractToWord()
        {
            var cnt = 0;
            var abstractList = DataManager.Instance.GetAbstractsForPosterSession();
            foreach (var abs in abstractList)
            {
                if (WordManager.AbstractToWord(abs) != "---")
                {
                    cnt++;
                };

            }
            MessageBoxResult messageBoxResult = MessageBox.Show(cnt + " файлов созданы по адресу " + DefaultManager.Instance.AbstractFilePath + ". Открыть?", "Информация", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Process.Start("explorer.exe", DefaultManager.Instance.AbstractFilePath);
            }

        }

        private void PosterEmail()
        {

            MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите отправить приглашения на постерную сессию по e-mail?", "Подтверждение действия", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var abstractList = DataManager.Instance.GetAbstractsForPosterSession();
                foreach (var abs in abstractList)
                {
                    var emails = new List<string>(abs.PersonConference.Person.Emails.Select(o => o.Name));
                    var topic = DefaultManager.Instance.MailHeaderPoster;
                    var message = DefaultManager.Instance.MailMessagePosterSession.Replace("<ABSTRACT_NAME>", abs.Name);
                    var file = WordManager.AbstractToWord(abs);
                    var emailFrom = abs.Reviewer.Email;
                    EmailManager.Instance.SendMailForAbstract(emails, emailFrom, topic, message, file);
                }

            }
        }

        public Abstract CurrentAbstract
        {
            get { return this.currentAbstract; }
            set
            {
                this.currentAbstract = value;
                if (CurrentAbstract != null)
                {
                    this.CurrentAbstractVM = new AbstractViewModel(currentAbstract);
                }
                this.OnPropertyChanged("CurrentAbstract");
            }
        }

        public AbstractViewModel CurrentAbstractVM
        {
            get { return this.currentAbstractVM; }
            set
            {
                currentAbstractVM = value;
                OnPropertyChanged("CurrentAbstractVM");
            }
        }

        private void DeleteAbstract()
        {
            DataManager.Instance.RemoveAbstract(CurrentAbstract);
            AllAbstracts.Remove(CurrentAbstract);
            OnPropertyChanged("AllAbstracts");
        }

        private void RefreshAbstracts()
        {
            filterText = "";
       //     isFilterConference = true;
            this.OnPropertyChanged("FilterText");
            this.OnPropertyChanged("IsFilterConference");
            AllAbstractsDB = new ObservableCollection<Abstract>(DataManager.Instance.GetAllAbstracts());
            AllAbstracts = AllAbstractsDB;
            this.OnPropertyChanged("AllAbstracts");
            this.CurrentAbstract = AllAbstracts.Count > 0 ? AllAbstracts[0] : null;
        }

        private void FilterAbstracts()
        {
            if (isFilterConference)
            {
                AllAbstractsDB = new ObservableCollection<Abstract>(
                    DataManager.Instance.GetAllAbstractsForConference(ConferenceFilter.Id));
                AllAbstracts = new ObservableCollection<Abstract>(
                    AllAbstractsDB.Where(o => o.ToFilterString.ToUpper().Contains(filterText.ToUpper())));
            }
            else
            {
                AllAbstracts =
                    new ObservableCollection<Abstract>(
                        AllAbstractsDB.Where(o => o.ToFilterString.ToUpper().Contains(filterText.ToUpper())));

            }
            this.OnPropertyChanged("AllAbstracts");
            this.CurrentAbstract = AllAbstracts.Count > 0 ? AllAbstracts[0] : null;
        }

        private void OpenAbstract()
        {
            var vm = new AbstractViewModel(currentAbstract);
            var v = new AbstractView { DataContext = vm };
            v.Show();
        }
        private void OpenFolder()
        {
            var strPath = CurrentAbstract.Link.Substring(0, CurrentAbstract.Link.LastIndexOf("\\"));
            Process.Start("explorer.exe", strPath);
        }
        private void OpenFile()
        {
            var strPath = CurrentAbstract.Link;
            Process.Start("explorer.exe", strPath);
        }



        private void SelectFile()
        {
            // Create OpenFileDialog 
            var dlg = new OpenFileDialog();

            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result != true) return;
            // Open document 
            string filename = dlg.FileName;
            CurrentAbstract.Link = filename;
        }
        public string FilterText
        {
            get { return this.filterText; }
            set
            {
                this.filterText = value.Trim();
                this.OnPropertyChanged("FilterText");
                if (filterText.Equals(""))
                {
                    RefreshAbstracts();
                }
                else
                {

                    FilterAbstracts();
                }
            }
        }
    }
}

