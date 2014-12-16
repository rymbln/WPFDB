using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class BadgeDesignerViewModel: ViewModelBase
    {


        //private BadgeType currentBadge { get; set; }
        private BadgeViewModel currentBadge { get; set; }
        private BadgeElementViewModel currentBadgeElement { get; set; }


        public ObservableCollection<BadgeViewModel> BadgeCollection { get; private set; }
        public ObservableCollection<BadgeElementViewModel> BadgeElementCollection { get; private set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand AddBadgeElementCommand { get; private set; }
        public ICommand RemoveBadgeElementCommand { get; private set; }


        public BadgeDesignerViewModel()
        {
            BadgeCollection = new ObservableCollection<BadgeViewModel>(DataManager.Instance.GetAllBadges().Select(o => new BadgeViewModel(o)).ToList());


            SaveCommand = new DelegateCommand(o => Save());
            AddBadgeElementCommand = new DelegateCommand(o => AddBadgeElement(), o => CurrentBadge != null);
            RemoveBadgeElementCommand = new DelegateCommand(o => RemoveBadgeElement(), o => CurrentBadgeElement != null);

            RefreshBadges();

        }

        private void AddBadgeElement()
        {
            var element = DefaultManager.Instance.DefaultBadgeElement;
            DataManager.Instance.AddElementToBadge(CurrentBadge.Model, element);

            var vm = new BadgeElementViewModel(element);
            BadgeElementCollection.Add(vm);
            OnPropertyChanged("BadgeElementCollection");
            CurrentBadgeElement = vm;
        }

        private void RemoveBadgeElement()
        {
            DataManager.Instance.RemoveBadgeElement(CurrentBadgeElement.Model);
            BadgeElementCollection.Remove(CurrentBadgeElement);
            OnPropertyChanged("CurrentBadgeCollection");
        }

        public List<Rank> RankLookup
        {
            get
            {
                return DataManager.Instance.GetAllRanks().ToList();
            }
        }


        private void Save()
        {
            DataManager.Instance.Save();
        }

        public void RefreshBadges()
        {
            OnPropertyChanged("BadgeCollection");
           // CurrentBadge = BadgeCollection.Count > 0 ? BadgeCollection[0] : null;
        }

        public BadgeViewModel CurrentBadge
        {
            get
            {
                return currentBadge;
            }
            set
            {
                currentBadge = value;
                BadgeElementCollection = new ObservableCollection<BadgeElementViewModel>(currentBadge.Model.Badges.Select(o => new BadgeElementViewModel(o)).ToList());
                OnPropertyChanged("CurrentBadge");
                OnPropertyChanged("BadgeElementCollection");
             
            }
        }

        public BadgeElementViewModel CurrentBadgeElement
        {
            get { return currentBadgeElement; }
            set
            {
                currentBadgeElement = value;
                OnPropertyChanged("CurrentBadgeElement");
            }
        }
     
      
      
    }
}
