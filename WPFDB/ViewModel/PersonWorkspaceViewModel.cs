using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.View;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class PersonWorkspaceViewModel : ViewModelBase
    {
        private PersonViewModel currentPerson;
        private ConferenceViewModel filterConference;
        private bool filterAllConferences;
     
        public PersonWorkspaceViewModel()
        {
            AllPersons = new ObservableCollection<PersonViewModel>();

            foreach (var item in DataManager.Instance.GetAllPersons())
            {
                AllPersons.Add(new PersonViewModel(item));
            }
            this.CurrentPerson = AllPersons.Count > 0 ? AllPersons[0] : null;

            this.AllPersons.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentPerson))
                {
                    this.CurrentPerson = null;
                }
            };

            this.AddPersonCommand = new DelegateCommand((o) => this.AddPerson());
            this.DeletePersonCommand = new DelegateCommand((o) => this.DeleteCurrentPerson());
            this.RefreshCommand = new DelegateCommand((o) => this.RefreshPersons());
            this.OpenPersonCommand = new DelegateCommand((o) => this.OpenPerson());
            this.ApplyFiltersCommand = new DelegateCommand((o) => this.ApplyFilters());

        }

        public ICommand AddPersonCommand { get; private set; }
        public ICommand DeletePersonCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand OpenPersonCommand { get; private set; }
        public ICommand ApplyFiltersCommand { get; private set; }
 

        public ObservableCollection<PersonViewModel> AllPersons { get; private set; }

        public PersonViewModel CurrentPerson
        {
            get { return this.currentPerson; }
            set
            {
                this.currentPerson = value; 
                this.OnPropertyChanged("CurrentPerson");
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

        public bool FilterAllConferences
        {
            get { return this.filterAllConferences; }
            set
            {
                this.filterAllConferences = value;
                OnPropertyChanged("FilterAllConferences");
            }
        }

     private void AddPerson()
        {
            Person p = DefaultManager.Instance.DefaultPerson;
            PersonViewModel vm = new PersonViewModel(p);
            this.AllPersons.Add(vm);
            this.CurrentPerson = vm;

             OpenPerson();
        }

        private void DeleteCurrentPerson()
        {
            DataManager.Instance.RemovePerson(this.CurrentPerson.Model);
            this.AllPersons.Remove(this.CurrentPerson);
            this.CurrentPerson = null;

        }

        private void RefreshPersons()
        {
            AllPersons = new ObservableCollection<PersonViewModel>();

            foreach (var item in DataManager.Instance.GetAllPersons())
            {
                AllPersons.Add(new PersonViewModel(item));
            }
        }

        private void OpenPerson()
        {
            PersonFormViewModel vm = new PersonFormViewModel(CurrentPerson);
            PersonFormView v = new PersonFormView{DataContext = vm};
            v.Show();
        }

        private void ApplyFilters()
        {
            //TODO ApplyFilters
        }

    }
}
