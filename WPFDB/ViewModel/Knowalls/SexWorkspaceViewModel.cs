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
    public class SexWorkspaceViewModel : ViewModelBase
    {
        private SexViewModel currentSex;
   private DataManager dm = DataManager.Instance;

        public SexWorkspaceViewModel( )
        {
            AllSexes = new ObservableCollection<SexViewModel>();
            foreach (var item in dm.GetAllSexes())
            {
                AllSexes.Add(new SexViewModel(item));
            }
            this.CurrentSex = this.AllSexes.Count > 0 ? this.AllSexes[0] : null;
            this.AllSexes.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentSex))
                {
                    this.CurrentSex = null;
                }
            };

            this.AddSexCommand = new DelegateCommand((o) => this.AddSex());
            this.DeleteSexCommand = new DelegateCommand((o) => this.DeleteCurrentSex(), (o) => this.CurrentSex != null );
        }

        public ObservableCollection<SexViewModel> AllSexes { get; private set; }

        public SexViewModel CurrentSex
        {
            get { return this.currentSex; }
            set { this.currentSex = value; this.OnPropertyChanged("CurrentSex"); }
        }

        public ICommand AddSexCommand { get; private set; }
        public ICommand DeleteSexCommand { get; private set; }

        private void AddSex()
        {
            Sex s = this.dm.CreateObject<Sex>();
            s.Id = GuidComb.Generate();
            this.dm.AddSex(s);

            SexViewModel vm = new SexViewModel(s);
            this.AllSexes.Add(vm);
            this.CurrentSex = vm;
        }

        private void DeleteCurrentSex()
        {
            this.dm.RemoveSex(this.CurrentSex.Model);
            this.AllSexes.Remove(this.CurrentSex);
            this.CurrentSex = null;
        }
    }
}
