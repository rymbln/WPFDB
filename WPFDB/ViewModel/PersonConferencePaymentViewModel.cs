using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Common;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class PersonConferencePaymentViewModel: ViewModelBase
    {
       // private 
        private CompanyViewModel company;
        private PaymentTypeViewModel paymentType;
        private OrderStatusViewModel orderStatus;
        public ObservableCollection<CompanyViewModel> CompanyLookup { get; private set; }
        public ObservableCollection<PaymentTypeViewModel> PaymentTypeLookup { get; private set; }
        public ObservableCollection<OrderStatusViewModel> OrderStatusLookup { get; private set; } 
        public PersonConferences_Payment Model { get; private set; }

        public PersonConferencePaymentViewModel(PersonConferences_Payment obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            this.Model = obj;
            OrderStatusLookup = new ObservableCollection<OrderStatusViewModel>();
            foreach (var orderStatuse in DataManager.Instance.GetAllOrderStatuses())
            {
                OrderStatusLookup.Add(new OrderStatusViewModel(orderStatuse));
            }
            OrderStatusLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.PaymentType))
                {
                    this.OrderStatus = new OrderStatusViewModel(DataManager.Instance.GetDefaultOrderStatus());
                }
            };
            PaymentTypeLookup = new ObservableCollection<PaymentTypeViewModel>();
            foreach (var pt in DataManager.Instance.GetAllPaymentTypes())
            {
                PaymentTypeLookup.Add(new PaymentTypeViewModel(pt));
            }
            PaymentTypeLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.PaymentType))
                {
                    this.PaymentType = new PaymentTypeViewModel(DataManager.Instance.GetDefaultPaymentType());
                }
            };
            CompanyLookup = new ObservableCollection<CompanyViewModel>();
            foreach (var company in DataManager.Instance.GetAllCompanies())
            {
                CompanyLookup.Add(new CompanyViewModel(company));
            }
            CompanyLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.Company))
                {
                    this.Company = new CompanyViewModel(DataManager.Instance.GetDefaultCompany());
                }
            };
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
        
        public CompanyViewModel Company
        {
            get
            {
                if (this.Model.Company == null)
                {
                    this.Model.Company = DataManager.Instance.GetDefaultCompany();
                }
                this.company = this.CompanyLookup.SingleOrDefault(r => r.Model == this.Model.Company);
                return this.company;
            }
            set
            {
                this.company = value;
                this.Model.Company = (value == null) ? null : value.Model;
                this.OnPropertyChanged("Company");
            }
        }
        public OrderStatusViewModel OrderStatus
        {
            get
            {
                if (this.Model.OrderStatus == null)
                {
                    this.Model.OrderStatus = DataManager.Instance.GetDefaultOrderStatus();
                }
                this.orderStatus = this.OrderStatusLookup.SingleOrDefault(r => r.Model == this.Model.OrderStatus);
                return this.orderStatus;
            }
            set
            {
                this.orderStatus = value;
                this.Model.OrderStatus = (value == null) ? null : value.Model;
                this.OnPropertyChanged("OrderStatus");
            }
        }
           public PaymentTypeViewModel PaymentType
           {
               get
               {
                   if (this.Model.PaymentType == null)
                   {
                       this.Model.PaymentType = DataManager.Instance.GetDefaultPaymentType();
                   }
                   this.paymentType = this.PaymentTypeLookup.SingleOrDefault(r => r.Model == this.Model.PaymentType);
                   return this.paymentType;
               }
               set
               {
                   this.paymentType = value;
                   this.Model.PaymentType = (value == null) ? null : value.Model;
                   this.OnPropertyChanged("PaymentType");
               }
           }
    }
    
}
