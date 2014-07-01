using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class PersonConferencePaymentViewModel: ViewModelBase
    {
       // private 

        public PersonConferences_Payment Model { get; private set; }

        public PersonConferencePaymentViewModel(PersonConferences_Payment obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            this.Model = obj;
        }

        public string PaymentDocument
        {
            get { return this.Model.PaymentDocument; }
            set { this.Model.PaymentDocument = value; this.OnPropertyChanged("PaymentDocument"); }
        }
        public DateTime PaymentDate
        {
            get { return this.Model.PaymentDate; }
            set { this.Model.PaymentDate = value; this.OnPropertyChanged("PaymentDate"); }
        }
        public Decimal Money
        {
            get { return this.Model.Money; }
            set { this.Model.Money = value; this.OnPropertyChanged("Money"); }
        }
        public bool IsComplect
        {
            get { return this.Model.IsComplect; }
            set { this.Model.IsComplect = value; this.OnPropertyChanged("IsComplect"); }
        }
        public int OrderNumber
        {
            get { return this.Model.OrderNumber; }
            set { this.Model.OrderNumber = value; this.OnPropertyChanged("OrderNumber"); }
        }
        public int OrderStatus
        {
            get { return this.Model.OrderStatus; }
            set { this.Model.OrderStatus = value; this.OnPropertyChanged("OrderStatus"); }
        }

    }
}
