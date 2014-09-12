using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.ViewModel.Helpers;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;


namespace WPFDB.ViewModel
{
    public class AbstractViewModel : ViewModelBase
    {
        public Abstract Model { get; private set; }
        private AbstractWorkViewModel currentAbstractWork { get; set; }

        public AbstractViewModel(Abstract obj)
        {
            this.Model = obj;
            this.AbstractWorks = new ObservableCollection<AbstractWorkViewModel>();
            this.AbstractWorks.CollectionChanged += (sender, e) =>
            {
                this.CurrentAbstractWork = AbstractWorks.FirstOrDefault();
            };
            foreach (var source in Model.AbstractWorks.ToList())
            {
                AbstractWorks.Add(new AbstractWorkViewModel(source));
            }
            AddAbstractWorkCommand = new DelegateCommand(o => AddAbstractWork());
            RemoveAbstractWorkCommand = new DelegateCommand(o => RemoveAbstractWork(), o => CurrentAbstractWork != null);

            this.SelectFileCommand = new DelegateCommand(o => SelectFile());
            this.OpenFolderCommand = new DelegateCommand(o => OpenFolder());
            this.OpenFileCommand = new DelegateCommand(o => OpenFile());
            SaveCommand = new DelegateCommand(o => Save());
            SendEmailCommand = new DelegateCommand(o => SendEmailForAbstract(), o=> CurrentAbstractWork != null);
        }

        public ObservableCollection<AbstractWorkViewModel> AbstractWorks { get; private set; }
        public ICommand AddAbstractWorkCommand { get; private set; }
        public ICommand RemoveAbstractWorkCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SendEmailCommand { get; private set; }
        public ICommand OpenFolderCommand { get; private set; }
        public ICommand OpenFileCommand { get; private set; }
        public ICommand SelectFileCommand { get; private set; }

        private void AddAbstractWork()
        {
            var abstractWork = DefaultManager.Instance.DefaultAbstractWork;
            DataManager.Instance.AddAbstractWorkToAbstract(Model, abstractWork);

            var vm = new AbstractWorkViewModel(abstractWork);
            this.AbstractWorks.Add(vm);
            OnPropertyChanged("AbstractWorks");
            CurrentAbstractWork = vm;
            
        }
        private void RemoveAbstractWork()
        {
            DataManager.Instance.RemoveAbstractWork(CurrentAbstractWork.Model);
            AbstractWorks.Remove(CurrentAbstractWork);
            OnPropertyChanged("AbstractWorks");
        }
        private void Save()
        {
            DataManager.Instance.Save();
            
        }


        private void SendEmailForAbstract()
        {

            MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите отправить тезисы по e-mail?", "Подтверждение действия", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var emails = new List<string>(CurrentAbstractWork.Model.Abstract.PersonConference.Person.Emails.Select(o => o.Name));
                var topic = DefaultManager.Instance.MailHeaderAbstract + " Тезисы № " + CurrentAbstractWork.Model.Abstract.SourceId;
                var message = "---";
                switch (CurrentAbstractWork.AbstractStatusName)
                {
                    case "Принят":
                        message = DefaultManager.Instance.MailMessagePositive.Replace("<ABSTRACT_NAME>", CurrentAbstractWork.Model.Abstract.Name); ;
                        break;
                    case "Отклонен":
                        message = DefaultManager.Instance.MailMessageNegative.Replace("<ABSTRACT_NAME>", CurrentAbstractWork.Model.Abstract.Name); ;
                        break;
                    case "Отклонен повторно":
                        message = DefaultManager.Instance.MailMessageNegativeSecond.Replace("<ABSTRACT_NAME>", CurrentAbstractWork.Model.Abstract.Name); ;
                        break;
                    case "В работе":
                        message = DefaultManager.Instance.MailMessageWork.Replace("<ABSTRACT_NAME>", CurrentAbstractWork.Model.Abstract.Name); ;
                        break;
                    default:
                        break;
                }
                var file = WordManager.AbstractToWord(CurrentAbstractWork.Model.Abstract);
                var emailFrom = CurrentAbstractWork.Reviewer.Email;
                EmailManager.Instance.SendMailForAbstract(emails, emailFrom, topic, message, file);
            }


          
        }

        private void OpenFolder()
        {
            var strPath = Link.Substring(0,Link.LastIndexOf("\\"));
            Process.Start("explorer.exe", strPath);
        }
        private void OpenFile()
        {
            var strPath = Link;
            Process.Start("explorer.exe", strPath);
        }

        private void SelectFile()
        {
            // Create OpenFileDialog 
            var dlg = new OpenFileDialog();

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                this.Link = filename;
            }
        }

        public AbstractWorkViewModel CurrentAbstractWork
        {
            get { return this.currentAbstractWork; }
            set
            {
                this.currentAbstractWork = value;
                this.OnPropertyChanged("CurrentAbstractWork");
            }
        }

        public string OtherAuthors
        {
            get { return this.Model.OtherAuthors; }
            set
            {
                this.Model.OtherAuthors = value;
                this.OnPropertyChanged("OtherAuthors");
            }
        }

        public string Name
        {
            get { return this.Model.Name; }
            set
            {
                this.Model.Name = value;
                OnPropertyChanged("Name");
            }
        }


        public string Text
        {
            get { return this.Model.Text; }
            set
            {
                this.Model.Text = value;
                OnPropertyChanged("Text");
            }
        }
        public string Link
        {
            get { return this.Model.Link; }
            set
            {
                this.Model.Link = value;
                OnPropertyChanged("Link");
            }
        }

        public string AuthorName
        {
            get { return this.Model.AuthorName; }
            set {}
        }

        public string AuthorEmail
        {
            get { return this.Model.AuthorEmail; }
            set { }
        }

    }
}
