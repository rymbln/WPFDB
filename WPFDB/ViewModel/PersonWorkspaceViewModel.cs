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
     
        private DataManager dm = DataManager.Instance;

        public PersonWorkspaceViewModel()
        {
            AllPersons = new ObservableCollection<PersonViewModel>();

            foreach (var item in dm.GetAllPersons())
            {
                AllPersons.Add(new PersonViewModel(item));
            }
            this.CurrentPerson = AllPersons.Count > 0 ? AllPersons[0] : null;

            //this.PersonConference = new PersonConferenceWorkspaceViewModel(
            //    DataManager.Instance.GetPersonConference(CurrentPerson.Model,
            //    DefaultManager.Instance.DefaultConference));

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

        }

        public ICommand AddPersonCommand { get; private set; }
        public ICommand DeletePersonCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand OpenPersonCommand { get; private set; }
 

        public ObservableCollection<PersonViewModel> AllPersons { get; private set; }

        public PersonViewModel CurrentPerson
        {
            get { return this.currentPerson; }
            set
            {
                this.currentPerson = value; this.OnPropertyChanged("CurrentPerson");
               


        //        this.PersonConference = new PersonConferenceWorkspaceViewModel(
        //DataManager.Instance.GetPersonConference(CurrentPerson.Model,
        //DefaultManager.Instance.DefaultConference));
            }
        }

     private void AddPerson()
        {
            Person p = this.dm.CreateObject<Person>();
            p.Id = GuidComb.Generate();
            this.dm.AddPerson(p);

            PersonViewModel vm = new PersonViewModel(p);
            this.AllPersons.Add(vm);
            this.CurrentPerson = vm;
        }

        private void DeleteCurrentPerson()
        {
            this.dm.RemovePerson(this.CurrentPerson.Model);
            this.AllPersons.Remove(this.CurrentPerson);
            this.CurrentPerson = null;

        }

        private void RefreshPersons()
        {
            AllPersons = new ObservableCollection<PersonViewModel>();

            foreach (var item in dm.GetAllPersons())
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

    }
}
