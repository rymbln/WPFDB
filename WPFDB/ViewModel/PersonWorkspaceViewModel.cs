using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private CurrentPersonViewModel currentPersonVM { get; set; }
        private Conference conferenceFilter { get; set; }
        private bool isFilterConference { get; set; }

        // private bool filterAllConferences;
        private string filterText = "";

        public PersonWorkspaceViewModel()
        {
            isFilterConference = false;

            AllPersons = new ObservableCollection<Person>();
            AllPersonsDB = new ObservableCollection<Person>();
            AllPersons.CollectionChanged += AllPersonsCollectionChanged;



            ConferenceLookup = new ObservableCollection<Conference>(DataManager.Instance.GetAllConferences());
            ConferenceFilter = DefaultManager.Instance.DefaultConference;
            RefreshPersons();
            AddPersonCommand = new DelegateCommand((o) => AddPerson());
            DeletePersonCommand = new DelegateCommand((o) => DeleteCurrentPerson());
            RefreshCommand = new DelegateCommand((o) => RefreshPersons());
            OpenPersonCommand = new DelegateCommand((o) => OpenPerson());
            ToogleConferenceFilterCommand = new DelegateCommand(o => ToggleConferenceFilter());

        }

        public ICommand AddPersonCommand { get; private set; }
        public ICommand DeletePersonCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand OpenPersonCommand { get; private set; }
        public ICommand ApplyFiltersCommand { get; private set; }
        public ICommand ToogleConferenceFilterCommand { get; private set; }



        public ObservableCollection<Person> AllPersons { get; private set; }
        public ObservableCollection<Person> AllPersonsDB { get; private set; }
        public ObservableCollection<Conference> ConferenceLookup { get; private set; }

        private void ToggleConferenceFilter()
        {
            IsFilterConference = !IsFilterConference;
        }

        public Conference ConferenceFilter
        {
            get { return conferenceFilter; }
            set
            {
                conferenceFilter = value;
                if (IsFilterConference)
                {
                    FilterPersons();
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
                    FilterPersons();
                }
                else
                {
                    RefreshPersons();
                }
            }
        }

        public Person CurrentPerson
        {
            get { return currentPerson; }
            set
            {
                currentPerson = value;
                if (CurrentPerson != null)
                {
                    CurrentPersonVM = new CurrentPersonViewModel(currentPerson);
                }
                OnPropertyChanged("CurrentPerson");
            }
        }

        public CurrentPersonViewModel CurrentPersonVM
        {
            get { return currentPersonVM; }
            set
            {
                currentPersonVM = value;
                OnPropertyChanged("CurrentPersonVM");
            }
        }

        private void AddPerson()
        {
            Person p = DefaultManager.Instance.DefaultPerson;
            AllPersons.Add(p);
            CurrentPerson = p;

            OpenPerson();
        }

        private void DeleteCurrentPerson()
        {
            DataManager.Instance.RemovePerson(CurrentPerson);
            AllPersons.Remove(CurrentPerson);
            OnPropertyChanged("AllPersons");
            //   CurrentPerson = null;

        }

        private void RefreshPersons()
        {
            filterText = "";
            isFilterConference = false;
            OnPropertyChanged("FilterText");
            OnPropertyChanged("IsFilterConference");
            AllPersonsDB = new ObservableCollection<Person>(DataManager.Instance.GetAllPersons());
            AllPersons = AllPersonsDB;
            OnPropertyChanged("AllPersons");
            CurrentPerson = AllPersons.Count > 0 ? AllPersons[0] : null;
        }
        private void FilterPersons()
        {
            if (isFilterConference)
            {
                AllPersonsDB =
                    new ObservableCollection<Person>(
                        DataManager.Instance.GetAllPersonForConference(ConferenceFilter.Id));
                AllPersons = new ObservableCollection<Person>(
                            AllPersonsDB.Where(o => o.ToFilterString.ToUpper().Contains(filterText.ToUpper())));
            }
            else
            {
                AllPersons =
                    new ObservableCollection<Person>(
                        AllPersonsDB.Where(o => o.ToFilterString.ToUpper().Contains(filterText.ToUpper())));
            }
            OnPropertyChanged("AllPersons");
            CurrentPerson = AllPersons.Count > 0 ? AllPersons[0] : null;
        }



        public void AllPersonsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CurrentPerson = AllPersons.Count > 0 ? AllPersons[0] : null;
            CurrentPersonVM = new CurrentPersonViewModel(AllPersons.Count > 0 ? AllPersons[0] : null);
        }

        private void OpenPerson()
        {
            var vm = new PersonViewModel(currentPerson);
            var v = new PersonFormView { DataContext = vm };
            v.Show();
        }

        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value.Trim();
                OnPropertyChanged("FilterText");
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
