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



    public class AddressViewModel : ViewModelBase
    {
        public Address Model;
        private ContactTypeViewModel contactType;
        public ObservableCollection<GeoBase> CitiesLookup { get; set; }
        private GeoBase citiesLookupSelected;
        public GeoBase CitiesLookupSelected
        {
            get

            { return citiesLookupSelected; }
            set
            {
                citiesLookupSelected = value;
                OnPropertyChanged("CitiesLookupSelected");
                ZipCode = citiesLookupSelected.CityPostalCode.ToString();
                RegionName = citiesLookupSelected.RegionName + " " + citiesLookupSelected.RegionTypeFull;
                CountryName = "Россия";
            }
        }

        public ICommand ReloadCityInformation { get; private set; }

        public AddressViewModel(Address address)
        {
            ReloadCityInformation = new DelegateCommand(o => ReloadCity(), o => CityName.Length > 0);

            if (address == null)
            {
                throw new ArgumentNullException("address");
            }
            if (address.ContactType == null)
            {
                address.ContactType = DefaultManager.Instance.DefaultContactType;
            }
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
            this.Model = address;
            CitiesLookup = new ObservableCollection<GeoBase>();
        }
        public ObservableCollection<ContactTypeViewModel> ContactTypeLookup { get; private set; }

        private void ReloadCity()
        {
            var geos = DataManager.Instance.FindCityGeo(CityName);
            CitiesLookup.Clear();
            foreach (var geo in geos)
            {
                CitiesLookup.Add(geo);
            }
            CitiesLookupSelected = CitiesLookup.First();
        }

        public string ZipCode
        {
            get { return this.Model.ZipCode; }
            set
            {
                this.Model.ZipCode = value;
                this.OnPropertyChanged("ZipCode");
            }
        }

        public string CountryName
        {
            get { return this.Model.CountryName; }
            set
            {
                this.Model.CountryName = value;
                this.OnPropertyChanged("CountryName");
            }
        }
        public string RegionName
        {
            get { return this.Model.RegionName; }
            set
            {
                this.Model.RegionName = value;
                this.OnPropertyChanged("RegionName");
            }
        }
        public string CityName
        {
            get { return this.Model.CityName; }
            set
            {
                this.Model.CityName = value;
                if (value.Length > 3)
                {
                    ReloadCity();
                }
                this.OnPropertyChanged("CityName");
            }
        }
        public string StreetHouseName
        {
            get { return this.Model.StreetHouseName; }
            set
            {
                this.Model.StreetHouseName = value;
                this.OnPropertyChanged("StreetHouseName");
            }
        }
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
