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
    public class RankWorkspaceViewModel : ViewModelBase
    {
        private RankViewModel currentRank;
        private DataManager dm = DataManager.Instance;

        public RankWorkspaceViewModel()
        {
            AllRanks = new ObservableCollection<RankViewModel>();
            foreach (var item in dm.GetAllRanks())
            {
                AllRanks.Add(new RankViewModel(item));
            }
            this.CurrentRank = this.AllRanks.Count > 0 ? this.AllRanks[0] : null;
            this.AllRanks.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentRank))
                {
                    this.CurrentRank = null;
                }
            };

            this.AddRankCommand = new DelegateCommand((o) => this.AddRank());
            this.DeleteRankCommand = new DelegateCommand((o) => this.DeleteCurrentRank(),
                (o) => this.CurrentRank != null);


        }

        public ICommand SaveCommand { get; private set; }

        public ObservableCollection<RankViewModel> AllRanks { get; private set; }

        public RankViewModel CurrentRank
        {
            get { return currentRank; }
            set
            {
                currentRank = value;
                OnPropertyChanged("CurrentRank");
            }
        }

        public ICommand AddRankCommand { get; private set; }
        public ICommand DeleteRankCommand { get; private set; }

        public void AddRank()
        {
            var c = this.dm.CreateObject<Rank>();
            c.Id = GuidComb.Generate();
            dm.AddRank(c);

            var vm = new RankViewModel(c);
            AllRanks.Add(vm);
            CurrentRank = vm;
        }

        private void DeleteCurrentRank()
        {
            dm.RemoveRank(CurrentRank.Model);
            AllRanks.Remove(CurrentRank);
            CurrentRank = null;
        }
    }
}
