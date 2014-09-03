using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.View;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class PersonWorkspaceViewModel : ViewModelBase
    {

        private Person currentPerson { get; set; }
        private PersonViewModel currentPersonVM { get; set; }
        private ConferenceViewModel filterConference { get; set; }
        // private bool filterAllConferences;
        private string filterText = "";

        public PersonWorkspaceViewModel()
        {
            AllPersons = new ObservableCollection<Person>();
            AllPersonsDB = new ObservableCollection<Person>();
            AllPersons.CollectionChanged += AllPersonsCollectionChanged;
         //   AllPersons = new ObservableCollection<Person>(DataManager.Instance.GetAllPersons(FilterText, null));
            RefreshPersons();
           //// this.CurrentPerson = AllPersons.Count > 0 ? AllPersons[0] : null;

           // this.AllPersons.CollectionChanged += (sender, e) =>
           // {
           //     if (e.OldItems != null && e.OldItems.Contains(this.CurrentPerson))
           //     {
           //         this.CurrentPerson = AllPersons.FirstOrDefault();
           //     }
           // };


            this.AddPersonCommand = new DelegateCommand((o) => this.AddPerson());
            this.DeletePersonCommand = new DelegateCommand((o) => this.DeleteCurrentPerson());
            this.RefreshCommand = new DelegateCommand((o) => this.RefreshPersons());
            this.OpenPersonCommand = new DelegateCommand((o) => this.OpenPerson());
            //this.ApplyFiltersCommand = new DelegateCommand((o) => ApplyFilter(FilterText));
            //  this.ApplyFiltersCommand = new DelegateCommand((o) => this.ApplyFilters());

        }

        public ICommand AddPersonCommand { get; private set; }
        public ICommand DeletePersonCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand OpenPersonCommand { get; private set; }
        public ICommand ApplyFiltersCommand { get; private set; }


        public ObservableCollection<Person> AllPersons { get; private set; }
        public ObservableCollection<Person> AllPersonsDB { get; private set; }

        public Person CurrentPerson
        {
            get { return this.currentPerson; }
            set
            {
                this.currentPerson = value;
                if (CurrentPerson != null)
                {
                    this.CurrentPersonVM = new PersonViewModel(currentPerson);
                }
                this.OnPropertyChanged("CurrentPerson");
            }
        }

        public PersonViewModel CurrentPersonVM
        {
            get { return this.currentPersonVM; }
            set
            {
                currentPersonVM = value;
                OnPropertyChanged("CurrentPersonVM");
            }
        }
        public ConferenceViewModel FilterConference
        {
            get { return this.filterConference; }
            set
            {
                this.filterConference = value;
                OnPropertyChanged("FilterConference");
            }
        }

        private void AddPerson()
        {
            Person p = DefaultManager.Instance.DefaultPerson;
            this.AllPersons.Add(p);
            this.CurrentPerson = p;

            OpenPerson();
        }

        private void DeleteCurrentPerson()
        {
            DataManager.Instance.RemovePerson(this.CurrentPerson);
            this.AllPersons.Remove(this.CurrentPerson);
            this.CurrentPerson = null;

        }

        private void RefreshPersons()
        {
            filterText = "";
            this.OnPropertyChanged("FilterText");
            AllPersonsDB = new ObservableCollection<Person>(DataManager.Instance.GetAllPersons());
            AllPersons = AllPersonsDB;
            this.OnPropertyChanged("AllPersons");
            this.CurrentPerson = AllPersons.Count > 0 ? AllPersons[0] : null;
        }
        private void FilterPersons()
        {
            AllPersons = new ObservableCollection<Person>(AllPersonsDB.Where(o => o.ToFilterString.ToUpper().Contains(filterText.ToUpper())));
            this.OnPropertyChanged("AllPersons");
            this.CurrentPerson = AllPersons.Count > 0 ? AllPersons[0] : null;
        }



        public void AllPersonsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.CurrentPerson = AllPersons.Count > 0 ? AllPersons[0] : null;
            this.CurrentPersonVM = new PersonViewModel(AllPersons.Count > 0 ? AllPersons[0] : null);
        }

        private void OpenPerson()
        {
            PersonFormViewModel vm = new PersonFormViewModel(new PersonViewModel(currentPerson));
            PersonFormView v = new PersonFormView { DataContext = vm };
            v.Show();
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
                    RefreshPersons();
                }
                else
                {

                    FilterPersons();
                }
            }
        }



    }
}
