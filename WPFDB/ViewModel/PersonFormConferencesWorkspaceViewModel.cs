using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class PersonFormConferencesWorkspaceViewModel : ViewModelBase
    {
        private PersonConferenceDetailViewModel currentPersonConferenceDetail;
        private PersonConferencePaymentViewModel currentPersonConferencePayment;
        private PersonConferenceViewModel currentPersonConference;
        private ConferenceViewModel currentConference;
        private DataManager dm = DataManager.Instance;
        
        public PersonConference Model { get; private set; }

        public ObservableCollection<PersonConferenceViewModel> AllPersonConferences { get; private set; }
        public ObservableCollection<ConferenceViewModel> AllConferences { get; private set; }
        public string CurrentPersonConferenceName { get; private set; }
        
        public ICommand AddPersonConferenceCommand { get; private set; }
        public ICommand RemovePersonConferenceCommand { get; private set; }
        public ICommand PrintBadgeCommand { get; private set; }
        public ICommand PrintOrderCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public PersonFormConferencesWorkspaceViewModel(PersonViewModel person)
        {
            AllConferences = new ObservableCollection<ConferenceViewModel>();
            foreach (var c in dm.GetAllConferences())
            {
                AllConferences.Add(new ConferenceViewModel(c));
            }

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

            this.PrintBadgeCommand = new DelegateCommand((o)=> PrintBadge());
            this.PrintOrderCommand = new DelegateCommand((o) => PrintOrder());
            this.AddPersonConferenceCommand = new DelegateCommand((o) => this.AddPersonConference());
            this.RemovePersonConferenceCommand = new DelegateCommand((o) => this.RemoveCurrentPersonConference(), (o) => this.CurrentPersonConference != null);
            this.SaveCommand = new DelegateCommand((o) => this.Save());
        }

        public ConferenceViewModel CurrentConference
        {
            get
            {
                this.currentConference =
                    this.AllConferences.FirstOrDefault(o => o.Id == this.CurrentPersonConference.Model.ConferenceId.ToString());
                return this.currentConference;
            }
            set
            {
                this.currentConference = value;
                this.CurrentPersonConference.Model.Conference = (this.currentConference.Model);
                this.OnPropertyChanged("CurrentConference");
              
           }
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
                this.OnPropertyChanged("CurrentConference");
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
            var details = new PersonConferences_Detail
            {
                Company = DataManager.Instance.GetDefaultCompany(),
                DateArrive = DateTime.Now,
                IsAbstract = false,
                IsAutoreg = true,
                IsBadge = true,
                IsNeedBadge = true,
                IsArrive = true,
                Rank = DataManager.Instance.GetDefaultRank()
            };
            var payment = new PersonConferences_Payment
            {
                Company = DataManager.Instance.GetDefaultCompany(),
                PaymentType = DataManager.Instance.GetDefaultPaymentType(),
                PaymentDocument = "Order",
                PaymentDate = DateTime.Now,
                Money = 1200,
                IsComplect = true,
                OrderNumber = 5,
                OrderStatus = DataManager.Instance.GetDefaultOrderStatus()
            };
            pc.PersonConferences_Payment = payment;
            pc.PersonConferences_Detail = details;
            DataManager.Instance.AddPersonConference(pc);
            PersonConferenceViewModel pcvm = new PersonConferenceViewModel(pc);
            AllPersonConferences.Add(pcvm);
            CurrentPersonConference = pcvm;
        }

        public void RemoveCurrentPersonConference()
        {
            AllPersonConferences.Remove(this.CurrentPersonConference);
            dm.RemoveObject(dm.GetPersonConference(this.CurrentPersonConference.Model.Person, this.CurrentPersonConference.Model.Conference));
     
            this.CurrentPersonConference = AllPersonConferences.Count > 0 ? AllPersonConferences[0] : null;
        }

        public void PrintBadge()
        {
            
        }

        public void PrintOrder()
        {
            
        }

        private void Save()
        {
            this.dm.Save();
        }


    }
}
