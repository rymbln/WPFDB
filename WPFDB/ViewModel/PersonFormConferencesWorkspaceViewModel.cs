using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class PersonFormConferencesWorkspaceViewModel : ViewModelBase
    {
        private PersonConferenceDetailViewModel currentPersonConferenceDetail;
        private PersonConferencePaymentViewModel currentPersonConferencePayment;

        public ObservableCollection<PersonConferenceViewModel> AllPersonConferences { get; private set; }
        private PersonConferenceViewModel currentPersonConference;
        public string CurrentPersonConferenceName { get; private set; }
        private DataManager dm = DataManager.Instance;

        public ICommand AddPersonConferenceCommand { get; private set; }
        public ICommand RemovePersonConferenceCommand { get; private set; }

        public PersonFormConferencesWorkspaceViewModel(PersonViewModel person)
        {
            AllPersonConferences = new ObservableCollection<PersonConferenceViewModel>();
            foreach (var pc in dm.GetPersonConferencesForPerson(person.Model))
            {
                AllPersonConferences.Add(new PersonConferenceViewModel(pc));
            }

            this.CurrentPersonConference = AllPersonConferences.Count > 0 ? AllPersonConferences[0] : null;

            this.AllPersonConferences.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentPersonConference))
                {
                    this.CurrentPersonConference = null;
                }
            };
        }

        public PersonConferenceDetailViewModel CurrentPersonConferenceDetail
        {
            get { return this.currentPersonConferenceDetail; }
            set
            {
                this.currentPersonConferenceDetail = value;
                this.OnPropertyChanged("CurrentPersonConferenceDetail");
            }

        }


        public PersonConferencePaymentViewModel CurrentPersonConferencePayment
        {
            get { return this.currentPersonConferencePayment; }
            set
            {
                this.currentPersonConferencePayment = value;
                this.OnPropertyChanged("CurrentPersonConferencePayment");
            }

        }


        public PersonConferenceViewModel CurrentPersonConference
        {
            get { return this.currentPersonConference; }
            set
            {
                this.currentPersonConference = value;
                this.CurrentPersonConferenceDetail = new PersonConferenceDetailViewModel(CurrentPersonConference.Model.PersonConferences_Detail);
                this.CurrentPersonConferencePayment = new PersonConferencePaymentViewModel(CurrentPersonConference.Model.PersonConferences_Payment);
                this.CurrentPersonConferenceName = this.currentPersonConference.CurrentConferenceName;
                this.OnPropertyChanged("CurrentPersonConference");
            }
        }

        public void AddPersonConference()
        {
            Person p = CurrentPersonConference.CurrentPerson.Model;
            Conference c = DefaultManager.Instance.DefaultConference;
            PersonConference pc = dm.CreateObject<PersonConference>();
            pc.Person = p;
            pc.Conference = c;
            PersonConferenceViewModel pcvm = new PersonConferenceViewModel(pc);
            AllPersonConferences.Add(pcvm);
            CurrentPersonConference = pcvm;
        }

        public void RemoveCurrentPersonConference()
        {
          dm.RemoveObject(this.CurrentPersonConference.Model);
          AllPersonConferences.Remove(this.CurrentPersonConference);
          this.CurrentPersonConference = AllPersonConferences.Count > 0 ? AllPersonConferences[0] : null;
        }


    }
}
