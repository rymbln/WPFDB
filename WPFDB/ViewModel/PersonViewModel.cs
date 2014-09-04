using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.View;
using WPFDB.ViewModel.Helpers;
using Xceed.Wpf.DataGrid.Converters;

namespace WPFDB.ViewModel
{
    public class PersonViewModel : ViewModelBase
    {

        private EmailViewModel currentEmail;
        private AddressViewModel currentAddress;
        private PhoneViewModel currentPhone;

        private PersonConferenceDetailViewModel currentPersonConferenceDetail;
        private PersonConferencePaymentViewModel currentPersonConferencePayment;
        private PersonConferenceViewModel currentPersonConference;
        private ConferenceViewModel currentConference;

        public Person Model { get; private set; }

        private DataManager dm = DataManager.Instance;

        public PersonViewModel(Person person)
        {
            this.Model = person;

            ScienceDegreeLookup = new ObservableCollection<ScienceDegree>(DataManager.Instance.GetAllScienceDegrees());
            ScienceStatusLookup = new ObservableCollection<ScienceStatus>(DataManager.Instance.GetAllScienceStatuses());
            SexLookup = new ObservableCollection<Sex>(DataManager.Instance.GetAllSexes());
            SpecialityLookup = new ObservableCollection<Speciality>(DataManager.Instance.GetAllSpecialities());

            PersonEmails = new ObservableCollection<EmailViewModel>();
            PersonAddresses = new ObservableCollection<AddressViewModel>();
            PersonPhones = new ObservableCollection<PhoneViewModel>();
            if (Model.Emails.Count > 0)
            {
                foreach (var email in Model.Emails)
                {
                    PersonEmails.Add(new EmailViewModel(email));
                }
            }
            this.PersonEmails.CollectionChanged += (sender, e) =>
            {
                this.CurrentEmail = PersonEmails.FirstOrDefault();
            };
            if (Model.Addresses.Count > 0)
            {
                foreach (var adr in Model.Addresses)
                {
                    PersonAddresses.Add(new AddressViewModel(adr));
                }
            }
            this.PersonAddresses.CollectionChanged += (sender, e) =>
            {
                this.CurrentAddress = PersonAddresses.FirstOrDefault();
            };
            if (Model.Phones.Count > 0)
            {
                foreach (var phone in Model.Phones)
                {
                    PersonPhones.Add(new PhoneViewModel(phone));
                }
            }
            this.PersonPhones.CollectionChanged += (sender, e) =>
            {
                this.CurrentPhone = PersonPhones.FirstOrDefault();
            };

            AllConferences = new ObservableCollection<ConferenceViewModel>();
            foreach (var c in dm.GetAllConferences())
            {
                AllConferences.Add(new ConferenceViewModel(c));
            }

            AllPersonConferences = new ObservableCollection<PersonConferenceViewModel>();
            foreach (var pc in dm.GetPersonConferencesForPerson(Model))
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

            this.PrintBadgeCommand = new DelegateCommand((o) => PrintBadge());
            this.PrintOrderCommand = new DelegateCommand((o) => PrintOrder());
            this.AddPersonConferenceCommand = new DelegateCommand((o) => this.AddPersonConference());
            this.RemovePersonConferenceCommand = new DelegateCommand((o) => this.RemoveCurrentPersonConference(), (o) => this.CurrentPersonConference != null);


            this.SaveCommand = new DelegateCommand((o) => this.Save());
            this.CancelCommand = new DelegateCommand((o) => this.Cancel());

            this.AddEmailCommand = new DelegateCommand((o) => this.AddEmail());
            this.AddAddressCommand = new DelegateCommand((o) => this.AddAddress());
            this.AddPhoneCommand = new DelegateCommand((o) => this.AddPhone());
            this.DeleteEmailCommand = new DelegateCommand((o) => this.DeleteEmail(), (o) => this.CurrentEmail != null);
            this.DeleteAddressCommand = new DelegateCommand((o) => this.DeleteAddress(), (o) => this.CurrentAddress != null);
            this.DeletePhoneCommand = new DelegateCommand((o) => this.DeletePhone(), (o) => this.CurrentPhone != null);



        }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public ICommand AddEmailCommand { get; private set; }
        public ICommand DeleteEmailCommand { get; private set; }
        public ICommand AddAddressCommand { get; private set; }
        public ICommand DeleteAddressCommand { get; private set; }
        public ICommand AddPhoneCommand { get; private set; }
        public ICommand DeletePhoneCommand { get; private set; }

        public ICommand AddPersonConferenceCommand { get; private set; }
        public ICommand RemovePersonConferenceCommand { get; private set; }
        public ICommand PrintBadgeCommand { get; private set; }
        public ICommand PrintOrderCommand { get; private set; }


        public ObservableCollection<ScienceDegree> ScienceDegreeLookup { get; private set; }
        public ObservableCollection<ScienceStatus> ScienceStatusLookup { get; private set; }
        public ObservableCollection<Sex> SexLookup { get; private set; }
        public ObservableCollection<Speciality> SpecialityLookup { get; private set; }

        public ObservableCollection<EmailViewModel> PersonEmails { get; private set; }
        public ObservableCollection<AddressViewModel> PersonAddresses { get; private set; }
        public ObservableCollection<PhoneViewModel> PersonPhones { get; private set; }

        public ObservableCollection<PersonConferenceViewModel> AllPersonConferences { get; private set; }
        public ObservableCollection<ConferenceViewModel> AllConferences { get; private set; }
        public string CurrentPersonConferenceName { get; private set; }



        public ScienceDegree ScienceDegree
        {
            get
            {
                if (this.Model.ScienceDegree == null)
                {
                    this.Model.ScienceDegree = DefaultManager.Instance.DefaultScienceDegree;

                }
                return this.Model.ScienceDegree;
            }

            set
            {
                this.Model.ScienceDegree = value;
                this.OnPropertyChanged("ScienceDegree");
            }
        }

        public ScienceStatus ScienceStatus
        {
            get
            {
                if (this.Model.ScienceStatus == null)
                {
                    this.Model.ScienceStatus = DefaultManager.Instance.DefaultScienceStatus;
                }
                return this.Model.ScienceStatus;
            }

            set
            {
                this.Model.ScienceStatus = value;
                this.OnPropertyChanged("ScienceStatus");
            }
        }

        public Sex Sex
        {
            get
            {
                if (this.Model.Sex == null)
                {
                    this.Model.Sex = DefaultManager.Instance.DefaultSex;

                }
                return this.Model.Sex;
            }

            set
            {
                this.Model.Sex = value;
                this.OnPropertyChanged("Sex");
            }
        }
        public Speciality Speciality
        {
            get
            {
                if (this.Model.Speciality == null)
                {
                    this.Model.Speciality = DefaultManager.Instance.DefaultSpeciality;
                }
                return this.Model.Speciality;
            }

            set
            {
                this.Model.Speciality = value;
                this.OnPropertyChanged("Speciality");
            }
        }


        public string FirstName
        {
            get
            {
                return this.Model.FirstName;
            }

            set
            {
                this.Model.FirstName = value;
                this.OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get
            {
                return this.Model.SecondName;
            }

            set
            {
                this.Model.SecondName = value;
                this.OnPropertyChanged("SecondName");
            }
        }
        public string ThirdName
        {
            get
            {
                return this.Model.ThirdName;
            }

            set
            {
                this.Model.ThirdName = value;
                this.OnPropertyChanged("ThirdName");
            }
        }
        public DateTime? BirthDate
        {
            get
            {
                return this.Model.BirthDate;
            }

            set
            {
                this.Model.BirthDate = value;
                this.OnPropertyChanged("BirthDate");
            }
        }
        public string WorkPlace
        {
            get
            {
                return this.Model.WorkPlace;
            }

            set
            {
                this.Model.WorkPlace = value;
                this.OnPropertyChanged("WorkPlace");
            }
        }
        public string Post
        {
            get
            {
                return this.Model.Post;
            }

            set
            {
                this.Model.Post = value;
                this.OnPropertyChanged("Post");
            }
        }
        public string Id
        {
            get { return this.Model.Id.ToString(); }
            set { }
        }
        public int SourceId
        {
            get
            {
                return this.Model.SourceId;
            }

            set
            {
                this.Model.SourceId = value;
                this.OnPropertyChanged("SourceId");
            }
        }
        public bool IsMember
        {
            get
            {
                return this.Model.Iacmac.IsMember;
            }

            set
            {
                this.Model.Iacmac.IsMember = value;
                this.OnPropertyChanged("IsMember");
            }
        }
        public string IsMemberName
        {
            get
            {
                if (this.Model.Iacmac.IsMember)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                };
            }
        }
        public bool IsCardCreate
        {
            get
            {
                return this.Model.Iacmac.IsCardCreate;
            }

            set
            {
                this.Model.Iacmac.IsCardCreate = value;
                this.OnPropertyChanged("IsCardCreate");
            }
        }
        public string IsCardCreateName
        {
            get
            {
                if (this.Model.Iacmac.IsCardCreate)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                };
            }
        }
        public string FullName
        {
            get { return this.Model.FullName; }
        }
        public string FullNameInitials
        {
            get { return this.Model.FullNameInitials; }
        }
        public string Initials
        {
            get
            {
                return this.Model.FirstName.Substring(1, 1) + "." + this.Model.SecondName.Substring(1, 1) + "." +
                       this.Model.ThirdName.Substring(1, 1) + ".";
            }
        }
        public string WorkplacePostName
        {
            get { return this.Model.WorkPlace + " (" + this.Model.Post + ")"; }
        }
        public bool IsCardSent
        {
            get
            {
                return this.Model.Iacmac.IsCardSent;
            }

            set
            {
                this.Model.Iacmac.IsCardSent = value;
                this.OnPropertyChanged("IsCardSent");
            }
        }
        public string IsCardSentName
        {
            get
            {
                if (this.Model.Iacmac.IsCardSent)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                };
            }


        }
        public bool IsForm
        {
            get
            {
                return this.Model.Iacmac.IsForm;
            }

            set
            {
                this.Model.Iacmac.IsForm = value;
                this.OnPropertyChanged("IsForm");
            }
        }
        public string IsFormName
        {
            get
            {
                if (this.Model.Iacmac.IsForm)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                };
            }
        }
        public DateTime? DateRegistration
        {
            get
            {
                return this.Model.Iacmac.DateRegistration;
            }

            set
            {
                this.Model.Iacmac.DateRegistration = value;
                this.OnPropertyChanged("DateRegistration");
            }
        }
        public int Number
        {
            get
            {
                return this.Model.Iacmac.Number;
            }

            set
            {
                this.Model.Iacmac.Number = value;
                this.OnPropertyChanged("Number");
            }
        }
        public string Code
        {
            get
            {
                return this.Model.Iacmac.Code;
            }

            set
            {
                this.Model.Iacmac.Code = value;
                this.OnPropertyChanged("Code");
            }
        }

        private void Save()
        {
            dm.Save();
        }

        private void Cancel()
        {
            dm.Rollback();
        }

      

        public EmailViewModel CurrentEmail
        {
            get
            {
                return this.currentEmail;
            }
            set
            {
                this.currentEmail = value;
                this.OnPropertyChanged("CurrentEmail");

            }
        }

        public AddressViewModel CurrentAddress
        {
            get
            {
                return this.currentAddress;
            }
            set
            {
                this.currentAddress = value;
                this.OnPropertyChanged("CurrentAddress");
            }
        }

        public PhoneViewModel CurrentPhone
        {
            get
            {
                return this.currentPhone;
            }
            set
            {
                this.currentPhone = value;
                this.OnPropertyChanged("CurrentPhone");
            }
        }

        private void AddEmail()
        {
            var email = DefaultManager.Instance.DefaultEmail;
            this.dm.AddEmailToPerson(Model, email);

            var vm = new EmailViewModel(email);
            this.PersonEmails.Add(vm);
            this.CurrentEmail = vm;
        }

        private void DeleteEmail()
        {
            this.dm.RemoveEmail(Model, CurrentEmail.Model);
            this.PersonEmails.Remove(CurrentEmail);
            this.CurrentEmail = null;
        }

        private void AddAddress()
        {
            var address = DefaultManager.Instance.DefaultAddress;
            this.dm.AddAddressToPerson(Model, address);

            var vm = new AddressViewModel(address);
            this.PersonAddresses.Add(vm);
            this.CurrentAddress = vm;
        }

        private void DeleteAddress()
        {
            this.dm.RemoveAddress(Model, CurrentAddress.Model);
            this.PersonAddresses.Remove(CurrentAddress);
            this.CurrentAddress = null;
        }

        private void AddPhone()
        {
            var phone = DefaultManager.Instance.DefaultPhone;
            this.dm.AddPhoneToPerson(Model, phone);

            var vm = new PhoneViewModel(phone);
            this.PersonPhones.Add(vm);
            this.CurrentPhone = vm;
        }

        private void DeletePhone()
        {
            this.dm.RemovePhone(Model, CurrentPhone.Model);
            this.PersonPhones.Remove(CurrentPhone);
            this.CurrentPhone = null;
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
            Conference c = DefaultManager.Instance.DefaultConference;
            var pc = DataManager.Instance.AddPersonConference(Model, c);
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
    }
}
