using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private Abstract currentAbstract;

        private PersonConference currentPersonConference;

        public Person Model { get; private set; }

        public PersonViewModel(Person person)
        {
            Model = person;

            ScienceDegreeLookup = new ObservableCollection<ScienceDegree>(DataManager.Instance.GetAllScienceDegrees());
            ScienceStatusLookup = new ObservableCollection<ScienceStatus>(DataManager.Instance.GetAllScienceStatuses());
            SexLookup = new ObservableCollection<Sex>(DataManager.Instance.GetAllSexes());
            SpecialityLookup = new ObservableCollection<Speciality>(DataManager.Instance.GetAllSpecialities());
            OrderStatusLookup = new ObservableCollection<OrderStatus>(DataManager.Instance.GetAllOrderStatuses());
            PaymentTypeLookup = new ObservableCollection<PaymentType>(DataManager.Instance.GetAllPaymentTypes());
            CompanyLookup = new ObservableCollection<Company>(DataManager.Instance.GetAllCompanies());
            RankLookup = new ObservableCollection<Rank>(DataManager.Instance.GetAllRanks());
            


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
            PersonEmails.CollectionChanged += (sender, e) =>
            {
                CurrentEmail = PersonEmails.FirstOrDefault();
            };
            if (Model.Addresses.Count > 0)
            {
                foreach (var adr in Model.Addresses)
                {
                    PersonAddresses.Add(new AddressViewModel(adr));
                }
            }
            PersonAddresses.CollectionChanged += (sender, e) =>
            {
                CurrentAddress = PersonAddresses.FirstOrDefault();
            };
            if (Model.Phones.Count > 0)
            {
                foreach (var phone in Model.Phones)
                {
                    PersonPhones.Add(new PhoneViewModel(phone));
                }
            }
            PersonPhones.CollectionChanged += (sender, e) =>
            {
                CurrentPhone = PersonPhones.FirstOrDefault();
            };

            AllConferences = new ObservableCollection<ConferenceViewModel>();
            foreach (var c in DataManager.Instance.GetAllConferences())
            {
                AllConferences.Add(new ConferenceViewModel(c));
            }
            AllConferences.CollectionChanged += AllConferences_CollectionChanged;

            // Проверяем режим работы конференция
            if (DefaultManager.Instance.ConferenceMode)
            {
                var defConf = DefaultManager.Instance.DefaultConference;
                var persConf = DataManager.Instance.GetPersonConference(Model, defConf);
                if (persConf == null)
                {
                    var newPersConf = DataManager.Instance.CreateObject<PersonConference>();
                    newPersConf.PersonConferenceId = GuidComb.Generate();
                    newPersConf.PersonId = Model.Id;
                    newPersConf.ConferenceId = defConf.Id;
                    newPersConf.PersonConferences_Detail =
                        DefaultManager.Instance.DefaultPersonConferenceDetail(newPersConf.PersonConferenceId);
                    newPersConf.PersonConferences_Payment =
                        DefaultManager.Instance.DefaultPersonConferencePayment(newPersConf.PersonConferenceId);
                    DataManager.Instance.AddPersonConference(newPersConf);
                }
            }
            // Проверяем режим работы регистрация
            if (DefaultManager.Instance.RegistrationMode)
            {
                var defConf = DefaultManager.Instance.DefaultConference;
                var persConf = DataManager.Instance.GetPersonConference(Model, defConf);
                if (persConf == null)
                {
                    var newPersConf = DataManager.Instance.CreateObject<PersonConference>();
                    newPersConf.PersonConferenceId = GuidComb.Generate();
                    newPersConf.PersonId = Model.Id;
                    newPersConf.ConferenceId = defConf.Id;
                    newPersConf.PersonConferences_Detail =
                        DefaultManager.Instance.DefaultPersonConferenceDetail(newPersConf.PersonConferenceId);
                    newPersConf.PersonConferences_Detail.IsArrive = true;
                    newPersConf.PersonConferences_Detail.DateArrive = DateTime.Now;
                    newPersConf.PersonConferences_Payment =
                        DefaultManager.Instance.DefaultPersonConferencePayment(newPersConf.PersonConferenceId);
                    DataManager.Instance.AddPersonConference(newPersConf);
                }
                else
                {
                    persConf.PersonConferences_Detail.IsArrive = true;
                    persConf.PersonConferences_Detail.DateArrive = DateTime.Now;
                    DataManager.Instance.Save();
                }
            }


            AllPersonConferences = new ObservableCollection<PersonConference>(DataManager.Instance.GetPersonConferencesForPerson(Model));
            CurrentPersonConference = AllPersonConferences.Count > 0 ? AllPersonConferences[0] : null;
            if (DefaultManager.Instance.ConferenceMode || DefaultManager.Instance.RegistrationMode)
            {
                CurrentPersonConference =
                    AllPersonConferences.SingleOrDefault(o => o.Conference == DefaultManager.Instance.DefaultConference);
            }
            

        AllAbstracts = new ObservableCollection<Abstract>(DataManager.Instance.GetAbstractsByPersonConferenceID(CurrentPersonConference.PersonConferenceId));

            CurrentAbstract = AllAbstracts.FirstOrDefault();
         

            PrintBadgeCommand = new DelegateCommand(o => PrintBadge());
            PrintOrderCommand = new DelegateCommand(o => PrintOrder());
            AddPersonConferenceCommand = new DelegateCommand(o => AddPersonConference());
            RemovePersonConferenceCommand = new DelegateCommand(o => RemoveCurrentPersonConference(), (o) => CurrentPersonConference != null);


            SaveCommand = new DelegateCommand(o => Save());
            CancelCommand = new DelegateCommand(o => Cancel());

            AddEmailCommand = new DelegateCommand(o => AddEmail());
            AddAddressCommand = new DelegateCommand(o => AddAddress());
            AddPhoneCommand = new DelegateCommand(o => AddPhone());
            DeleteEmailCommand = new DelegateCommand(o => DeleteEmail(), (o) => CurrentEmail != null);
            DeleteAddressCommand = new DelegateCommand(o => DeleteAddress(), (o) => CurrentAddress != null);
            DeletePhoneCommand = new DelegateCommand(o => DeletePhone(), (o) => CurrentPhone != null);

            AddAbstractCommand = new DelegateCommand(o => AddAbstract());
            RemoveAbstractCommand = new DelegateCommand(o => DeleteAbstract(), o => CurrentAbstract != null);

        }

        void AllConferences_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CurrentPersonConference = AllPersonConferences.LastOrDefault();
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

        public ICommand AddAbstractCommand { get; private set; }
        public ICommand RemoveAbstractCommand { get; private set; }


        public ObservableCollection<ScienceDegree> ScienceDegreeLookup { get; private set; }
        public ObservableCollection<ScienceStatus> ScienceStatusLookup { get; private set; }
        public ObservableCollection<Sex> SexLookup { get; private set; }
        public ObservableCollection<Speciality> SpecialityLookup { get; private set; }

        public ObservableCollection<Company> CompanyLookup { get; private set; }
        public ObservableCollection<PaymentType> PaymentTypeLookup { get; private set; }
        public ObservableCollection<OrderStatus> OrderStatusLookup { get; private set; }
        public ObservableCollection<Rank> RankLookup { get; private set; }

        public ObservableCollection<EmailViewModel> PersonEmails { get; private set; }
        public ObservableCollection<AddressViewModel> PersonAddresses { get; private set; }
        public ObservableCollection<PhoneViewModel> PersonPhones { get; private set; }

        public ObservableCollection<PersonConference> AllPersonConferences { get; private set; }
        public ObservableCollection<ConferenceViewModel> AllConferences { get; private set; }

        public ObservableCollection<Abstract>  AllAbstracts { get; private set; }


        public string CurrentPersonConferenceName
        {
            get { return CurrentPersonConference.Conference.Name; }
        }

        public ScienceDegree ScienceDegree
        {
            get { return Model.ScienceDegree ?? (Model.ScienceDegree = DefaultManager.Instance.DefaultScienceDegree); }

            set
            {
                Model.ScienceDegree = value;
                OnPropertyChanged("ScienceDegree");
            }
        }

        public ScienceStatus ScienceStatus
        {
            get { return Model.ScienceStatus ?? (Model.ScienceStatus = DefaultManager.Instance.DefaultScienceStatus); }

            set
            {
                Model.ScienceStatus = value;
                OnPropertyChanged("ScienceStatus");
            }
        }

        public Sex Sex
        {
            get { return Model.Sex ?? (Model.Sex = DefaultManager.Instance.DefaultSex); }

            set
            {
                Model.Sex = value;
                OnPropertyChanged("Sex");
            }
        }
        public Speciality Speciality
        {
            get { return Model.Speciality ?? (Model.Speciality = DefaultManager.Instance.DefaultSpeciality); }

            set
            {
                Model.Speciality = value;
                OnPropertyChanged("Speciality");
            }
        }


        public string FirstName
        {
            get
            {
                return Model.FirstName;
            }

            set
            {
                Model.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get
            {
                return Model.SecondName;
            }

            set
            {
                Model.SecondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string ThirdName
        {
            get
            {
                return Model.ThirdName;
            }

            set
            {
                Model.ThirdName = value;
                OnPropertyChanged("ThirdName");
            }
        }
        public DateTime? BirthDate
        {
            get
            {
                return Model.BirthDate;
            }

            set
            {
                Model.BirthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }
        public string WorkPlace
        {
            get
            {
                return Model.WorkPlace;
            }

            set
            {
                Model.WorkPlace = value;
                OnPropertyChanged("WorkPlace");
            }
        }
        public string Post
        {
            get
            {
                return Model.Post;
            }

            set
            {
                Model.Post = value;
                OnPropertyChanged("Post");
            }
        }
        public string Id
        {
            get { return Model.Id.ToString(); }
            set { }
        }
        public int SourceId
        {
            get
            {
                return Model.SourceId;
            }

            set
            {
                Model.SourceId = value;
                OnPropertyChanged("SourceId");
            }
        }
        public bool IsMember
        {
            get
            {
                return Model.Iacmac.IsMember;
            }

            set
            {
                Model.Iacmac.IsMember = value;
                OnPropertyChanged("IsMember");
            }
        }
        public string IsMemberName
        {
            get
            {
                if (Model.Iacmac.IsMember)
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
                return Model.Iacmac.IsCardCreate;
            }

            set
            {
                Model.Iacmac.IsCardCreate = value;
                OnPropertyChanged("IsCardCreate");
            }
        }
        public string IsCardCreateName
        {
            get
            {
                if (Model.Iacmac.IsCardCreate)
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
            get { return Model.FullName; }
        }
        public string FullNameInitials
        {
            get { return Model.FullNameInitials; }
        }
        public string Initials
        {
            get
            {
                return Model.FirstName.Substring(1, 1) + "." + Model.SecondName.Substring(1, 1) + "." +
                       Model.ThirdName.Substring(1, 1) + ".";
            }
        }
        public string WorkplacePostName
        {
            get { return Model.WorkPlace + " (" + Model.Post + ")"; }
        }
        public bool IsCardSent
        {
            get
            {
                return Model.Iacmac.IsCardSent;
            }

            set
            {
                Model.Iacmac.IsCardSent = value;
                OnPropertyChanged("IsCardSent");
            }
        }
        public string IsCardSentName
        {
            get
            {
                if (Model.Iacmac.IsCardSent)
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
                return Model.Iacmac.IsForm;
            }

            set
            {
                Model.Iacmac.IsForm = value;
                OnPropertyChanged("IsForm");
            }
        }
        public string IsFormName
        {
            get
            {
                if (Model.Iacmac.IsForm)
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
                return Model.Iacmac.DateRegistration;
            }

            set
            {
                Model.Iacmac.DateRegistration = value;
                OnPropertyChanged("DateRegistration");
            }
        }
        public int Number
        {
            get
            {
                return Model.Iacmac.Number;
            }

            set
            {
                Model.Iacmac.Number = value;
                OnPropertyChanged("Number");
            }
        }
        public string Code
        {
            get
            {
                return Model.Iacmac.Code;
            }

            set
            {
                Model.Iacmac.Code = value;
                OnPropertyChanged("Code");
            }
        }

        private void Save()
        {
            DataManager.Instance.Save();
        }

        private void Cancel()
        {
            DataManager.Instance.Rollback();
        }



        public EmailViewModel CurrentEmail
        {
            get
            {
                return currentEmail;
            }
            set
            {
                currentEmail = value;
                OnPropertyChanged("CurrentEmail");

            }
        }

        public AddressViewModel CurrentAddress
        {
            get
            {
                return currentAddress;
            }
            set
            {
                currentAddress = value;
                OnPropertyChanged("CurrentAddress");
            }
        }

        public PhoneViewModel CurrentPhone
        {
            get
            {
                return currentPhone;
            }
            set
            {
                currentPhone = value;
                OnPropertyChanged("CurrentPhone");
            }
        }

        private void AddEmail()
        {
            var email = DefaultManager.Instance.DefaultEmail;
            DataManager.Instance.AddEmailToPerson(Model, email);

            var vm = new EmailViewModel(email);
            PersonEmails.Add(vm);
            CurrentEmail = vm;
        }

        private void DeleteEmail()
        {
            DataManager.Instance.RemoveEmail(Model, CurrentEmail.Model);
            PersonEmails.Remove(CurrentEmail);
            CurrentEmail = null;
        }

        private void AddAddress()
        {
            var address = DefaultManager.Instance.DefaultAddress;
            DataManager.Instance.AddAddressToPerson(Model, address);

            var vm = new AddressViewModel(address);
            PersonAddresses.Add(vm);
            CurrentAddress = vm;
        }

        private void DeleteAddress()
        {
            DataManager.Instance.RemoveAddress(Model, CurrentAddress.Model);
            PersonAddresses.Remove(CurrentAddress);
            CurrentAddress = null;
        }

        private void AddPhone()
        {
            var phone = DefaultManager.Instance.DefaultPhone;
            DataManager.Instance.AddPhoneToPerson(Model, phone);

            var vm = new PhoneViewModel(phone);
            PersonPhones.Add(vm);
            CurrentPhone = vm;
        }

        private void DeletePhone()
        {
            DataManager.Instance.RemovePhone(Model, CurrentPhone.Model);
            PersonPhones.Remove(CurrentPhone);
            CurrentPhone = null;
        }

        private void AddAbstract()
        {
            var abs = DefaultManager.Instance.DefaultAbstract;
            abs.PersonConferenceId = CurrentPersonConference.PersonConferenceId;
            DataManager.Instance.AddAbstract(abs);
            AllAbstracts = new ObservableCollection<Abstract>(DataManager.Instance.GetAbstractsByPersonConferenceID(CurrentPersonConference.PersonConferenceId));
            CurrentAbstract = AllAbstracts.LastOrDefault();
            OnPropertyChanged("CurrentAbstract");
            OnPropertyChanged("AllAbstracts");
            
        }

        private void DeleteAbstract()
        {
            DataManager.Instance.RemoveAbstract(CurrentAbstract);
            AllAbstracts = new ObservableCollection<Abstract>(DataManager.Instance.GetAbstractsByPersonConferenceID(CurrentPersonConference.PersonConferenceId));
            CurrentAbstract = AllAbstracts.FirstOrDefault();
            OnPropertyChanged("CurrentAbstract");
            OnPropertyChanged("AllAbstracts");
        }


        public PersonConference CurrentPersonConference
        {
            get
            {
                return currentPersonConference;
            }
            set
            {
                currentPersonConference = value ?? AllPersonConferences.LastOrDefault();
                // Если человек никогда не был на конференциях
                if (currentPersonConference == null)
                {
                    var pc = new PersonConference();
                    pc.PersonConferenceId = GuidComb.Generate();
                    pc.PersonId = Model.Id;
                    pc.ConferenceId = DefaultManager.Instance.DefaultConference.Id;
                    DataManager.Instance.AddPersonConference(pc);
                    currentPersonConference = pc;
                }

                var abstracts = new ObservableCollection<Abstract>(DataManager.Instance.GetAbstractsByPersonConferenceID(currentPersonConference.PersonConferenceId));
                if (abstracts.Count > 0)
                {
                    AllAbstracts = abstracts;
                }
                else
                {
                    AllAbstracts = new ObservableCollection<Abstract>();
                }
                CurrentAbstract = AllAbstracts.FirstOrDefault();

                OnPropertyChanged("CurrentPersonConferences");
                OnPropertyChanged("AllPersonConferences");
                OnPropertyChanged("AllAbstracts");
                OnPropertyChanged("CurrentAbstract");
                OnPropertyChanged("Rank");
                OnPropertyChanged("Company");
                OnPropertyChanged("IsArrive");
                OnPropertyChanged("DateArrive");
                OnPropertyChanged("IsNeedBadge");
                OnPropertyChanged("IsBadge");
                OnPropertyChanged("IsAbstract");
                OnPropertyChanged("IsAutoreg");

                OnPropertyChanged("PaymentType");
                OnPropertyChanged("Payment_Company");
                OnPropertyChanged("PaymentDocument");
                OnPropertyChanged("PaymentDate");
                OnPropertyChanged("Money");
                OnPropertyChanged("IsComplect");
                OnPropertyChanged("OrderStatus");
                OnPropertyChanged("OrderNumber");

            }
        }



        public string PaymentDocument
        {
            get { return CurrentPersonConference.PersonConferences_Payment.PaymentDocument; }
            set
            {
                CurrentPersonConference.PersonConferences_Payment.PaymentDocument = value;
                OnPropertyChanged("PaymentDocument");
            }
        }
        public DateTime? PaymentDate
        {
            get { return CurrentPersonConference.PersonConferences_Payment.PaymentDate ?? DateTime.Now; }
            set { CurrentPersonConference.PersonConferences_Payment.PaymentDate = value; OnPropertyChanged("PaymentDate"); }
        }
        public Decimal Money
        {
            get { return CurrentPersonConference.PersonConferences_Payment.Money; }
            set { CurrentPersonConference.PersonConferences_Payment.Money = value; OnPropertyChanged("Money"); }
        }
        public bool IsComplect
        {
            get { return CurrentPersonConference.PersonConferences_Payment.IsComplect; }
            set { CurrentPersonConference.PersonConferences_Payment.IsComplect = value; OnPropertyChanged("IsComplect"); }
        }
        public int OrderNumber
        {
            get { return CurrentPersonConference.PersonConferences_Payment.OrderNumber; }
            set { CurrentPersonConference.PersonConferences_Payment.OrderNumber = value; OnPropertyChanged("OrderNumber"); }
        }

        public Company Payment_Company
        {
            get
            {
                if (CurrentPersonConference.PersonConferences_Payment.Company == null)
                {
                    CurrentPersonConference.PersonConferences_Payment.Company = DefaultManager.Instance.DefaultCompany;
                }
                return CurrentPersonConference.PersonConferences_Payment.Company;
            }
            set
            {
                CurrentPersonConference.PersonConferences_Payment.Company = value;
                OnPropertyChanged("Payment_Company");
            }
        }
        public OrderStatus OrderStatus
        {
            get {
                return CurrentPersonConference.PersonConferences_Payment.OrderStatus ??
                       (CurrentPersonConference.PersonConferences_Payment.OrderStatus =
                           DefaultManager.Instance.DefaultOrderStatus);
            }
            set
            {
                CurrentPersonConference.PersonConferences_Payment.OrderStatus = value;
                OnPropertyChanged("OrderStatus");
            }
        }

        public Rank Rank
        {
            get { return CurrentPersonConference.PersonConferences_Detail.Rank; }
            set
            {
                CurrentPersonConference.PersonConferences_Detail.Rank = value;
                OnPropertyChanged("Rank");
            }
        }

        public Company Company
        {
            get { return CurrentPersonConference.PersonConferences_Detail.Company; }
            set
            {
                CurrentPersonConference.PersonConferences_Detail.Company = value;
                OnPropertyChanged("Company");
            }
        }

        public bool IsArrive
        {
            get { return CurrentPersonConference.PersonConferences_Detail.IsArrive; }
            set
            {
                CurrentPersonConference.PersonConferences_Detail.IsArrive = value;
                OnPropertyChanged("IsArrive");
            }
        }

        public DateTime? DateArrive
        {
            get { return CurrentPersonConference.PersonConferences_Detail.DateArrive; }
            set
            {
                CurrentPersonConference.PersonConferences_Detail.DateArrive = value;
                OnPropertyChanged("DateArrive");
            }
        }

        public bool IsNeedBadge
        {
            get { return CurrentPersonConference.PersonConferences_Detail.IsNeedBadge; }
            set
            {
                CurrentPersonConference.PersonConferences_Detail.IsNeedBadge = value;
                OnPropertyChanged("IsNeedBadge");
            }
        }
        public bool IsBadge
        {
            get { return CurrentPersonConference.PersonConferences_Detail.IsBadge; }
            set
            {
                CurrentPersonConference.PersonConferences_Detail.IsBadge = value;
                OnPropertyChanged("IsBadge");
            }
        }
        public bool IsAbstract
        {
            get { return CurrentPersonConference.PersonConferences_Detail.IsAbstract; }
            set
            {
                CurrentPersonConference.PersonConferences_Detail.IsAbstract = value;
                OnPropertyChanged("IsAbstract");
            }
        }
        public bool IsAutoreg
        {
            get { return CurrentPersonConference.PersonConferences_Detail.IsAutoreg; }
            set
            {
                CurrentPersonConference.PersonConferences_Detail.IsAutoreg = value;
                OnPropertyChanged("IsAutoreg");
            }
        }

        public PaymentType PaymentType
        {
            get {
                return CurrentPersonConference.PersonConferences_Payment.PaymentType ??
                       (CurrentPersonConference.PersonConferences_Payment.PaymentType =
                           DefaultManager.Instance.DefaultPaymentType);
            }
            set
            {
                CurrentPersonConference.PersonConferences_Payment.PaymentType = value;
                OnPropertyChanged("PaymentType");
            }
        }

        public void AddPersonConference()
        {
            Conference c = DefaultManager.Instance.DefaultConference;
            var pc = DataManager.Instance.AddPersonConference(Model, c);
            AllPersonConferences.Add(pc);
            CurrentPersonConference = AllPersonConferences.LastOrDefault();
            //    OnPropertyChanged("AllPersonConferences");
        }

        public void RemoveCurrentPersonConference()
        {
            
            DataManager.Instance.RemovePersonConference(CurrentPersonConference);
     
            AllPersonConferences.Remove(CurrentPersonConference);
      //      CurrentPersonConference = AllPersonConferences.FirstOrDefault();
            //OnPropertyChanged("AllPersonConferences");
        }

        public void PrintBadge()
        {
            var person = Model;
            person.CompanyName = CurrentPersonConference.PersonConferences_Detail.Company.Name == null ? "" : CurrentPersonConference.PersonConferences_Detail.Company.Name; 
            var badge = DataManager.Instance.GetBadgeForRank(CurrentPersonConference.PersonConferences_Detail.Rank);
            PrintManager.Print(DocumentManager.Generate(DocumentType.BADGE, badge, person),DocumentType.BADGE);
        }

        public void PrintOrder()
        {

        }

        public string AbstractOtherAuthors
        {
            get
            {
                if (CurrentAbstract != null)
                {
                    return CurrentAbstract.OtherAuthors;
                }
                else
                {
                    return null;
                }
           
            }
            set
            {
                if (CurrentAbstract != null)
                {
                    CurrentAbstract.OtherAuthors = value;
                    OnPropertyChanged("AbstractOtherAuthors");
                }
            }
        }

        public string AbstractName
        {
            get
            {
                if (CurrentAbstract != null)
                {
                    return CurrentAbstract.Name;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (CurrentAbstract != null)
                {
                    CurrentAbstract.Name = value;
                    OnPropertyChanged("AbstractName");
                }
            }
        }
        public string AbstractText
        {
            get
            {
                if (CurrentAbstract != null)
                {
                    return CurrentAbstract.Text;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (CurrentAbstract != null)
                {
                    CurrentAbstract.Text = value;
                    OnPropertyChanged("AbstractText");
                }
            }
        }
        public string AbstractLink
        {
            get
            {
                if (CurrentAbstract != null)
                {
                    return CurrentAbstract.Link;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (CurrentAbstract != null)
                {
                    CurrentAbstract.Link = value;
                    OnPropertyChanged("AbstractLink");
                }
            }
        }


        public Abstract CurrentAbstract
        {
            get { return currentAbstract; }
            set
            {
                if (value != null)
                {
                    currentAbstract = value;
                    OnPropertyChanged("CurrentAbstract");

                    OnPropertyChanged("AbstractLink");
                    OnPropertyChanged("AbstractText");
                    OnPropertyChanged("AbstractName");
                    OnPropertyChanged("AbstractOtherAuthors");
                }
            }
        }
    }
}
