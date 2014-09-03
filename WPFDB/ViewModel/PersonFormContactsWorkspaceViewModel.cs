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

namespace WPFDB.ViewModel
{
    public class PersonFormContactsWorkspaceViewModel : ViewModelBase
    {
        private DataManager dm = DataManager.Instance;
        private PersonViewModel currentPerson;
        private EmailViewModel currentEmail;
        private AddressViewModel currentAddress;
        private PhoneViewModel currentPhone;

        public PersonFormContactsWorkspaceViewModel(PersonViewModel person)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }
            this.currentPerson = person;

            PersonEmails = new ObservableCollection<EmailViewModel>();
            PersonAddresses = new ObservableCollection<AddressViewModel>();
            PersonPhones = new ObservableCollection<PhoneViewModel>();

            if (person.Model.Emails.Count > 0)
            {
                foreach (var email in person.Model.Emails)
                {
                    PersonEmails.Add(new EmailViewModel(email));
                }
            }
            this.PersonEmails.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentEmail))
                {
                    this.CurrentEmail = PersonEmails.FirstOrDefault();
                }
            };
            if (person.Model.Addresses.Count > 0)
            {
                foreach (var adr in person.Model.Addresses)
                {
                    PersonAddresses.Add(new AddressViewModel(adr));
                }
            }
            this.PersonAddresses.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentAddress))
                {
                    this.CurrentAddress = PersonAddresses.FirstOrDefault();
                }
            };
            if (person.Model.Phones.Count > 0)
            {
                foreach (var phone in person.Model.Phones)
                {
                    PersonPhones.Add(new PhoneViewModel(phone));
                }
            }
            this.PersonPhones.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentPhone))
                {
                    this.CurrentPhone = PersonPhones.FirstOrDefault();
                }
            };

            this.AddEmailCommand = new DelegateCommand((o) => this.AddEmail());
            this.AddAddressCommand = new DelegateCommand((o) => this.AddAddress());
            this.AddPhoneCommand = new DelegateCommand((o) => this.AddPhone());
            this.DeleteEmailCommand = new DelegateCommand((o) => this.DeleteEmail(), (o) => this.CurrentEmail != null);
            this.DeleteAddressCommand = new DelegateCommand((o) => this.DeleteAddress(), (o) => this.CurrentAddress != null);
            this.DeletePhoneCommand = new DelegateCommand((o) => this.DeletePhone(), (o) => this.CurrentPhone != null);

        }

        public ICommand AddEmailCommand { get; private set; }
        public ICommand DeleteEmailCommand { get; private set; }
        public ICommand AddAddressCommand { get; private set; }
        public ICommand DeleteAddressCommand { get; private set; }
        public ICommand AddPhoneCommand { get; private set; }
        public ICommand DeletePhoneCommand { get; private set; }
        public ObservableCollection<EmailViewModel> PersonEmails { get; private set; }
        public ObservableCollection<AddressViewModel> PersonAddresses { get; private set; }
        public ObservableCollection<PhoneViewModel> PersonPhones { get; private set; } 
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
            this.dm.AddEmailToPerson(currentPerson.Model, email);

            var vm = new EmailViewModel(email);
            this.PersonEmails.Add(vm);
            this.CurrentEmail = vm;
        }

        private void DeleteEmail()
        {
            this.dm.RemoveEmail(currentPerson.Model, CurrentEmail.Model);
            this.PersonEmails.Remove(CurrentEmail);
            this.CurrentEmail = null;
        }

        private void AddAddress()
        {
            var address = DefaultManager.Instance.DefaultAddress;
            this.dm.AddAddressToPerson(currentPerson.Model, address);

            var vm = new AddressViewModel(address);
            this.PersonAddresses.Add(vm);
            this.CurrentAddress = vm;
        }

        private void DeleteAddress()
        {
            this.dm.RemoveAddress(currentPerson.Model, CurrentAddress.Model);
            this.PersonAddresses.Remove(CurrentAddress);
            this.CurrentAddress = null;
        }

        private void AddPhone()
        {
            var phone = DefaultManager.Instance.DefaultPhone;
            this.dm.AddPhoneToPerson(currentPerson.Model, phone);

            var vm = new PhoneViewModel(phone);
            this.PersonPhones.Add(vm);
            this.CurrentPhone = vm;
        }

        private void DeletePhone()
        {
            this.dm.RemovePhone(currentPerson.Model, CurrentPhone.Model);
            this.PersonPhones.Remove(CurrentPhone);
            this.CurrentPhone = null;
        }
    }
}
