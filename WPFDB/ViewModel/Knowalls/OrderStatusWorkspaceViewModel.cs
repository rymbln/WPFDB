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
    public class OrderStatusWorkspaceViewModel: ViewModelBase
    {
        private OrderStatusViewModel currentOrderStatus;
        private DataManager dm = DataManager.Instance;

      public OrderStatusWorkspaceViewModel()
        {
            AllOrderStatuss = new ObservableCollection<OrderStatusViewModel>();
            foreach (var item in dm.GetAllOrderStatuses())
            {
                AllOrderStatuss.Add(new OrderStatusViewModel(item));
            }
            this.CurrentOrderStatus = this.AllOrderStatuss.Count > 0 ? this.AllOrderStatuss[0] : null;
            this.AllOrderStatuss.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentOrderStatus))
                {
                    this.CurrentOrderStatus = null;
                }
            };

            this.AddOrderStatusCommand = new DelegateCommand((o) => this.AddOrderStatus());
            this.DeleteOrderStatusCommand = new DelegateCommand((o) => this.DeleteCurrentOrderStatus(), (o) => this.CurrentOrderStatus != null);
     

        }

        public ICommand SaveCommand { get; private set; }

        public ObservableCollection<OrderStatusViewModel> AllOrderStatuss { get; private set; }

        public OrderStatusViewModel CurrentOrderStatus
        {
            get { return currentOrderStatus; }
            set { currentOrderStatus = value; OnPropertyChanged("CurrentOrderStatus"); }
        }

        public ICommand AddOrderStatusCommand { get; private set; }
        public ICommand DeleteOrderStatusCommand { get; private set; }

        public void AddOrderStatus()
        {
            OrderStatus c = this.dm.CreateObject<OrderStatus>();
            c.Id = GuidComb.Generate();
            dm.AddOrderStatus(c);

            OrderStatusViewModel vm = new OrderStatusViewModel(c);
            AllOrderStatuss.Add(vm);
            CurrentOrderStatus = vm;
        }

        private void DeleteCurrentOrderStatus()
        {
            dm.RemoveOrderStatus(CurrentOrderStatus.Model);
            AllOrderStatuss.Remove(CurrentOrderStatus);
            CurrentOrderStatus = null;
        }
    }
}
