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
    public class ScienceStatusWorkspaceViewModel : ViewModelBase
    {
        private ScienceStatusViewModel currentScienceStatus;

   private DataManager dm = DataManager.Instance;

        public ScienceStatusWorkspaceViewModel()
        {
            AllScienceStatuses = new ObservableCollection<ScienceStatusViewModel>();
            foreach (var item in dm.GetAllScienceStatuses())
            {
                AllScienceStatuses.Add(new ScienceStatusViewModel(item));
            }
            this.CurrentScienceStatus = this.AllScienceStatuses.Count > 0 ? this.AllScienceStatuses[0] : null;

            this.AllScienceStatuses.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentScienceStatus))
                {
                    this.CurrentScienceStatus = null;
                }
            };

            this.AddScienceStatusCommand = new DelegateCommand((o) => this.AddScienceStatus());
            this.DeleteScienceStatusCommand = new DelegateCommand((o)=>this.DeleteCurrentScienceStatus(), (o) => this.CurrentScienceStatus != null);
        }

        public ObservableCollection<ScienceStatusViewModel> AllScienceStatuses { get; private set; }

        public ScienceStatusViewModel CurrentScienceStatus
        {
            get { return this.currentScienceStatus; }
            set
            {
                this.currentScienceStatus = value;
                this.OnPropertyChanged("CurrentScienceStatus");
            }
        }

        public ICommand AddScienceStatusCommand { get; private set; }
        public ICommand DeleteScienceStatusCommand { get; private set; }

        private void AddScienceStatus()
        {
            ScienceStatus ss = this.dm.CreateObject<ScienceStatus>();
            ss.Id = GuidComb.Generate();
            this.dm.AddScienceStatus(ss);

            ScienceStatusViewModel vm = new ScienceStatusViewModel(ss);
            this.AllScienceStatuses.Add(vm);
            this.CurrentScienceStatus = vm;
        }

        private void DeleteCurrentScienceStatus()
        {
            this.dm.RemoveScienceStatus(this.CurrentScienceStatus.Model);
            this.AllScienceStatuses.Remove(this.CurrentScienceStatus);
            this.CurrentScienceStatus = null;
        }
    }
}
