using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Common;
using WPFDB.ViewModel;

namespace WPFDB.Model
{
    public class ListBoxBadgeDefault : ViewModelBase
    {
        public BadgesDefault Model;
        private bool? isSelected;
        public ListBoxBadgeDefault(BadgesDefault obj)
        {
            Model = obj;
            isSelected = false;
        }

        public ObservableCollection<Rank> RanksLookup
        {
            get
            {
                return new ObservableCollection<Rank>(
                    DataManager.Instance.GetAllRanks());
            }
        }
        public ObservableCollection<BadgeType> BadgeTypesLookup
        {
            get
            {
              return   new ObservableCollection<BadgeType>(
                    DataManager.Instance.GetAllBadges());
            }
        }


        public bool? IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public Rank RankValue
        {
            get { return Model.Rank; }
            set { Model.Rank = (value != null) ? value : null;
            OnPropertyChanged("RankValue");
            }
        }

        public BadgeType BadgeTypeValue
        {
            get { return Model.BadgeType; }
            set
            {
                Model.BadgeType = (value != null) ? value : null;
                OnPropertyChanged("BadgeTypeValue");
            }
        }
    }
}
