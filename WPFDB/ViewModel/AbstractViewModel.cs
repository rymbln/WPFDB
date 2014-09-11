﻿using System;
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
using WPFDB.ViewModel.Helpers;


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
            SendEmailCommand = new DelegateCommand(o => SendEmail(), o=> CurrentAbstractWork != null);
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
        private void SendEmail()
        {

            var lstEmail = new List<string> {"ivan.trushin@antibiotic.ru"};
            EmailManager.Instance.SendMailForAbstract(lstEmail, "Test Email From WPFDB Conference",
                "This email is sent for testing email manager module", @"D:\ELI_Validation_20140612.xls");
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
