﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Common;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class PhoneViewModel:ViewModelBase
    {
        public Phone Model;
        private ContactTypeViewModel contactType;

        public PhoneViewModel(Phone phone)
        {
            if (phone == null)
            {
                throw new ArgumentNullException("phone");
            }
            if (phone.ContactType == null)
            {
                phone.ContactType = DefaultManager.Instance.DefaultContactType;
            }
            this.Model = phone;

            ContactTypeLookup = new ObservableCollection<ContactTypeViewModel>();
            foreach (var contactType in DataManager.Instance.GetAllContactTypes())
            {
                ContactTypeLookup.Add(new ContactTypeViewModel(contactType));
            }
            ContactTypeLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.ContactType))
                {
                    this.ContactType = new ContactTypeViewModel(DefaultManager.Instance.DefaultContactType);
                }
            };

        }

        public string Number
        {
            get { return this.Model.Number; }
            set
            {
                this.Model.Number = value;
                this.OnPropertyChanged("Number");
            }
        }

        public ObservableCollection<ContactTypeViewModel> ContactTypeLookup { get; private set; }
        public ContactTypeViewModel ContactType
        {
            get
            {
                if (this.Model.ContactType == null)
                {
                    this.Model.ContactType = DefaultManager.Instance.DefaultContactType;
                }
                this.contactType =
                        this.ContactTypeLookup.SingleOrDefault(s => s.Model == this.Model.ContactType);

                return this.contactType;
            }
            set
            {
                this.contactType = value;
                this.Model.ContactType = (value == null) ? null : value.Model;
                this.OnPropertyChanged("ContactType");
            }
        }
    }
}
