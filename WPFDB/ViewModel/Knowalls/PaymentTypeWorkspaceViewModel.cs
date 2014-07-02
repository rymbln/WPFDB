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
    public class PaymentTypeWorkspaceViewModel : ViewModelBase
    {
        private PaymentTypeViewModel currentPaymentType;
        private DataManager dm = DataManager.Instance;

        public PaymentTypeWorkspaceViewModel()
        {
            AllPaymentTypes = new ObservableCollection<PaymentTypeViewModel>();
            foreach (var item in dm.GetAllPaymentTypes())
            {
                AllPaymentTypes.Add(new PaymentTypeViewModel(item));
            }
            this.CurrentPaymentType = this.AllPaymentTypes.Count > 0 ? this.AllPaymentTypes[0] : null;
            this.AllPaymentTypes.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentPaymentType))
                {
                    this.CurrentPaymentType = null;
                }
            };

            this.AddPaymentTypeCommand = new DelegateCommand((o) => this.AddPaymentType());
            this.DeletePaymentTypeCommand = new DelegateCommand((o) => this.DeleteCurrentPaymentType(),
                (o) => this.CurrentPaymentType != null);


        }

        public ICommand SaveCommand { get; private set; }

        public ObservableCollection<PaymentTypeViewModel> AllPaymentTypes { get; private set; }

        public PaymentTypeViewModel CurrentPaymentType
        {
            get { return currentPaymentType; }
            set
            {
                currentPaymentType = value;
                OnPropertyChanged("CurrentPaymentType");
            }
        }

        public ICommand AddPaymentTypeCommand { get; private set; }
        public ICommand DeletePaymentTypeCommand { get; private set; }

        public void AddPaymentType()
        {
            PaymentType c = this.dm.CreateObject<PaymentType>();
            c.Id = GuidComb.Generate();
            dm.AddPaymentType(c);

            PaymentTypeViewModel vm = new PaymentTypeViewModel(c);
            AllPaymentTypes.Add(vm);
            CurrentPaymentType = vm;
        }

        private void DeleteCurrentPaymentType()
        {
            dm.RemovePaymentType(CurrentPaymentType.Model);
            AllPaymentTypes.Remove(CurrentPaymentType);
            CurrentPaymentType = null;
        }
    }
}
