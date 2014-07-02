using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Common;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class PersonConferenceViewModel: ViewModelBase
    {
        private PersonViewModel currentPerson;
        private ConferenceViewModel currentConference;
        public PersonConference Model { get; private set; }

        private DataManager dm = DataManager.Instance;

        public PersonConferenceViewModel(PersonConference personConference)
        {
            this.currentConference = new ConferenceViewModel(personConference.Conference);
            this.currentPerson = new PersonViewModel(personConference.Person);
            this.Model = personConference;
        }

        public string CurrentConferenceName
        {
            get
            {
                return this.currentConference.Name;
            }

            //set
            //{
            //    this.currentPerson = value;
            //    this.OnPropertyChanged("CurrentPerson");
            //}
        }

        public PersonViewModel CurrentPerson
        {
            get
            {
                return this.currentPerson;
            }

            set
            {
                this.currentPerson = value;
                this.OnPropertyChanged("CurrentPerson");
            }
        }

        public ConferenceViewModel CurrentConference
        {
            get
            {
                return this.currentConference;
            }

            set
            {
                this.currentConference = value;
                this.OnPropertyChanged("CurrentConference");
            }
        }

    }
}
