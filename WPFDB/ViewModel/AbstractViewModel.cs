using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Common;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class AbstractViewModel : ViewModelBase
    {
        private ConferenceViewModel conference;
        private Person currentPerson;

        public ObservableCollection<ConferenceViewModel> ConferenceLookup { get; private set; }

        public Abstract Model { get; private set; }

        public AbstractViewModel(Abstract obj, Person person)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            this.Model = obj;
            this.currentPerson = person;

            ConferenceLookup = new ObservableCollection<ConferenceViewModel>();
            foreach (var conference in DataManager.Instance.GetAllConferences())
            {
                ConferenceLookup.Add(new ConferenceViewModel(conference));
            }
            ConferenceLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.Conference))
                {
                    this.Conference = new ConferenceViewModel(DataManager.Instance.GetDefaultConference());
                }
            };
        }

        public ConferenceViewModel Conference
        {
            get
            {
                if (this.Model.PersonConferenceId == null)
                {
                    // Создаем новый элемент PersonConference для текущей конференции

                    
                }
                this.conference =
                    this.ConferenceLookup.SingleOrDefault(c => c.Model.Id == DataManager.Instance.GetPersonConferenceByID(this.Model.PersonConferenceId.ToString()).ConferenceId);
                return this.conference;
            }

            set
            {
                // Проверяем есть ли запись PersonConference
                var personConference = DataManager.Instance.GetPersonConference(this.currentPerson, value.Model);
                if (personConference == null)
                {
                    personConference = DataManager.Instance.CreateObject<PersonConference>();
                    personConference.PersonConferenceId = GuidComb.Generate();
                    personConference.PersonId = this.currentPerson.Id;
                    personConference.ConferenceId = value.Model.Id;
                    DataManager.Instance.AddPersonConference(personConference);
                    
                }
                this.Model.PersonConferenceId = personConference.PersonConferenceId;
                this.Model.PersonConference = personConference;
                DataManager.Instance.SetPersonConferenceAbstractOn(personConference);
                this.conference = new ConferenceViewModel(this.Model.PersonConference.Conference);
       
                OnPropertyChanged("Conference");
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

        public string ConferenceName
        {
            get { return this.Conference.Model.Name; }
        }

        public string Id
        {
            get { return this.Model.Id.ToString(); }
            set {}
        }
    }
}
