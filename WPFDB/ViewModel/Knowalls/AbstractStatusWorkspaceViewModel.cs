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
using WPFDB.ViewModel;

namespace WPFDB.ViewModel
{
   public  class AbstractStatusWorkspaceViewModel: ViewModelBase
   {
       private AbstractStatusViewModel currentAbstractStatus;
       private DataManager dm = DataManager.Instance;

       public AbstractStatusWorkspaceViewModel()
       {
           AllAbstractStatuses = new ObservableCollection<AbstractStatusViewModel>();
           foreach (var item in dm.GetAllAbstractStatuses())
           {
               AllAbstractStatuses.Add(new AbstractStatusViewModel(item));

           }
           this.currentAbstractStatus = this.AllAbstractStatuses.Count > 0 ? this.AllAbstractStatuses[0] : null;
           AllAbstractStatuses.CollectionChanged += (sender, e) =>
           {
               if (e.OldItems != null && e.OldItems.Contains(this.CurrentAbstractStatus))
               {
                   this.CurrentAbstractStatus = null;
               }
           };

           this.AddAbstractStatusCommand = new DelegateCommand((o) => this.AddAbstractStatus());
           this.DeleteAbstractStatusCommand = new DelegateCommand((o) => this.DeleteAbstractStatus(),
               (o) => this.CurrentAbstractStatus != null);
       }

       public ICommand AddAbstractStatusCommand { get; private set; }
       public ICommand DeleteAbstractStatusCommand { get; private set; }
       public ICommand SaveCommand { get; private set; }

       public ObservableCollection<AbstractStatusViewModel> AllAbstractStatuses { get; private set; } 

       public AbstractStatusViewModel CurrentAbstractStatus
       {
           get { return currentAbstractStatus; }
           set
           {
               currentAbstractStatus = value;
               OnPropertyChanged("CurrentAbstractStatus");
           }
       }

       public void AddAbstractStatus()
       {
           var a  = dm.CreateObject<AbstractStatus>();
           a.Id = GuidComb.Generate();
           dm.AddAbstractStatus(a);

           var vm = new AbstractStatusViewModel(a);
           AllAbstractStatuses.Add(vm);
           CurrentAbstractStatus = vm;

       }

       public void DeleteAbstractStatus()
       {
           dm.RemoveObject(CurrentAbstractStatus.Model);
           AllAbstractStatuses.Remove(CurrentAbstractStatus);
           CurrentAbstractStatus = null;
       }
   }
}
