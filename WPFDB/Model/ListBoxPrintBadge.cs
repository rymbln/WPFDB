using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDB.Model
{
    public class ListBoxPrintBadge : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PersonConference Model { get; set; }
        private bool? isSelected;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public ListBoxPrintBadge(PersonConference obj)
        {
            Model = obj;
            IsSelected = false;
        }

        public string F { get { return Model.Person.FirstName.ToUpper(); } }
        public string IO { get { return Model.Person.SecondName + " " + Model.Person.ThirdName ; } }
        public string Country { get { return Model.Person.Addresses.OrderByDescending(o => o.DateAdd).Select(a => a.CountryName).FirstOrDefault(); } }
        public string City { get { return Model.Person.Addresses.OrderByDescending(o => o.DateAdd).Select(a => a.CityName).FirstOrDefault(); } }
        public string Company { get { return Model.PersonConferences_Detail.Company.Name; } }
        public string ToString { get { return F + IO + Country + City + Company; } }
        public Guid Id { get { return Model.PersonConferenceId; } }


        public bool? IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }



        // Products are equal if their names and product numbers are equal.
        public bool Equals(ListBoxPrintBadge x, ListBoxPrintBadge y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.Id == y.Id && x.ToString == y.ToString;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(ListBoxPrintBadge item)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(item, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = item.ToString == null ? 0 : item.ToString.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = item.Id.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }


    }
}
