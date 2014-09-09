using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
      

        private string filterText = "";

        public AbstractWorkspaceViewModel()
        {

            AllAbstracts = new ObservableCollection<Abstract>();
            AllAbstractsDB = new ObservableCollection<Abstract>();

            RefreshAbstracts();


            this.SelectFileCommand = new DelegateCommand(o => SelectFile());
            this.OpenFolderCommand = new DelegateCommand(o => OpenFolder());
            this.OpenFileCommand = new DelegateCommand(o => OpenFile());

            this.OpenAbstractCommand = new DelegateCommand((o) => this.OpenAbstract());
            this.DeleteAbstractCommand = new DelegateCommand((o) => this.DeleteAbstract());
            this.RefreshCommand = new DelegateCommand((o) => this.RefreshAbstracts());


        }
        public ObservableCollection<Abstract> AllAbstracts { get; private set; }
        public ObservableCollection<Abstract> AllAbstractsDB { get; private set; }

        public ICommand RefreshCommand { get; private set; }
        public ICommand ApplyFiltersCommand { get; private set; }


        public ICommand OpenFolderCommand { get; private set; }
        public ICommand OpenFileCommand { get; private set; }
        public ICommand SelectFileCommand { get; private set; }

        public ICommand OpenAbstractCommand{ get; private set; }
        public ICommand DeleteAbstractCommand { get; private set; }

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
            this.OnPropertyChanged("FilterText");
            AllAbstractsDB = new ObservableCollection<Abstract>(DataManager.Instance.GetAllAbstracts());
            AllAbstracts = AllAbstractsDB;
            this.OnPropertyChanged("AllAbstracts");
            this.CurrentAbstract = AllAbstracts.Count > 0 ? AllAbstracts[0] : null;
        }

        private void FilterAbstracts()
        {
            AllAbstracts = new ObservableCollection<Abstract>(AllAbstractsDB.Where(o => o.ToFilterString.ToUpper().Contains(filterText.ToUpper())));
            this.OnPropertyChanged("AllAbstracts");
            this.CurrentAbstract = AllAbstracts.Count > 0 ? AllAbstracts[0] : null;
        }

        private void OpenAbstract()
        {
            AbstractViewModel vm = new AbstractViewModel(currentAbstract);
            AbstractView v = new AbstractView{DataContext = vm};
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
            OpenFileDialog dlg = new OpenFileDialog();

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                CurrentAbstract.Link = filename;
            }
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

