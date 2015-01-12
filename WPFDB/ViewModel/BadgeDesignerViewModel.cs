using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class BadgeDesignerViewModel : ViewModelBase
    {


        //private BadgeType currentBadge { get; set; }
        private BadgeViewModel currentBadge { get; set; }
        private BadgeElementViewModel currentBadgeElement { get; set; }
        private Canvas canvasView { get; set; }

        public ObservableCollection<BadgeViewModel> BadgeCollection { get; private set; }
        public ObservableCollection<BadgeElementViewModel> BadgeElementCollection { get; private set; }


        public ICommand SaveCommand { get; private set; }
        public ICommand AddBadgeElementCommand { get; private set; }
        public ICommand RemoveBadgeElementCommand { get; private set; }
        public ICommand AddBadgeCommand { get; private set; }
        public ICommand RemoveBadgeCommand { get; private set; }
        public ICommand DrawBadgeCommand { get; private set; }
        public ICommand PrintBadgeCommand { get; private set; }


        public BadgeDesignerViewModel()
        {
            BadgeCollection = new ObservableCollection<BadgeViewModel>(DataManager.Instance.GetAllBadges().Select(o => new BadgeViewModel(o)).ToList());


            SaveCommand = new DelegateCommand(o => Save());
            AddBadgeCommand = new DelegateCommand(o => AddBadge());
            RemoveBadgeCommand = new DelegateCommand(o => RemoveBadge(), o => CurrentBadge != null);
            AddBadgeElementCommand = new DelegateCommand(o => AddBadgeElement(), o => CurrentBadge != null);
            RemoveBadgeElementCommand = new DelegateCommand(o => RemoveBadgeElement(), o => CurrentBadgeElement != null);
            DrawBadgeCommand = new DelegateCommand(o => DrawBadge());
            PrintBadgeCommand = new DelegateCommand(o => PrintBadge(), o => CurrentBadge != null);
            RefreshBadges();

        }

        private void PrintBadge()
        {
           Process.Start( PdfManager.Generate(PdfMode.BADGE, CurrentBadge.Model, null));
        }

        private void AddBadge()
        {
            var element = DefaultManager.Instance.DefaultBadge;
            DataManager.Instance.AddBadge(element);

            var vm = new BadgeViewModel(element);
            BadgeCollection.Add(vm);
            OnPropertyChanged("BadgeCollection");
            CurrentBadge = vm;
        }

        private void RemoveBadge()
        {
            DataManager.Instance.RemoveBadge(CurrentBadge.Model);
            BadgeCollection.Remove(CurrentBadge);
            OnPropertyChanged("CurrentBadge");
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

        private void DrawBadge()
        {
            
       //     var obj = BadgeElementCollection;
      //      BadgeElementCollection = new ObservableCollection<BadgeElementViewModel>(obj);
            OnPropertyChanged("BadgeElementCollection");
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

        public Canvas CanvasView
        {
            get { return canvasView; }
            set
            {
                canvasView = value;
                OnPropertyChanged("CanvasView");
            }
        }



    }
}
