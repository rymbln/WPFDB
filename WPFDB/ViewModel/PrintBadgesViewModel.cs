using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.ViewModel.Helpers;


namespace WPFDB.ViewModel
{
    public class PrintBadgesViewModel : ViewModelBase
    {
        public ObservableCollection<ItemGuidName> ConferenceLookup { get; set; }
        public ObservableCollection<ItemGuidName> RankLookup { get; set; }
        public ObservableCollection<ItemGuidName> BadgeLookup { get; set; }
        public ObservableCollection<ItemIntName> PrintedLookup { get; set; }
        public ObservableCollection<ItemIntName> PaidLookup { get; set; }
        public ObservableCollection<ListBoxPrintBadge> PrintCollection { get; set; }

        public ICommand PrintCommand { get; set; }
        public ICommand SelectAllCommand { get; set; }
        public ICommand SelectNoneCommand { get; set; }

        public PrintBadgesViewModel()
        {
            ConferenceLookup = new ObservableCollection<ItemGuidName>(DataManager.Instance.GetAllConferencesItem());
            RankLookup = new ObservableCollection<ItemGuidName>(DataManager.Instance.GetAllRanksItem());
            BadgeLookup = new ObservableCollection<ItemGuidName>(DataManager.Instance.GetAllBadgesItem());
            PrintedLookup = new ObservableCollection<ItemIntName>(DefaultManager.Instance.GetLogicLookup());
            PaidLookup = new ObservableCollection<ItemIntName>(DefaultManager.Instance.GetLogicLookup());
            RefreshLookups();
            SelectAllCommand = new DelegateCommand(o => SelectAll());
            SelectNoneCommand = new DelegateCommand(o => SelectNone());
            PrintCommand = new DelegateCommand(o => Print());
        }
        private void SelectAll()
        {
            foreach (var item in PrintCollection)
            {
                item.IsSelected = true;
            }
            OnPropertyChanged("PrintCollection");
        }

        private void SelectNone()
        {
            foreach (var item in PrintCollection)
            {
                item.IsSelected = false;
            }
            OnPropertyChanged("PrintCollection");
        }
        private void Print()
        {
            foreach (var item in PrintCollection.Where(o=>o.IsSelected == true).ToList())
            {
                var badge = DataManager.Instance.GetBadgeForRank(item.Model.PersonConferences_Detail.Rank);
            PrintManager.Print(DocumentManager.Generate(DocumentType.BADGE, badge, item), DocumentType.BADGE);
            DataManager.Instance.SetPersonConferencePrinted(item.Model);
            }
            MessageBox.Show("Печать окончена");
            RefreshPrintCollection();

        }
        private void RefreshLookups()
        {
            OnPropertyChanged("ConferenceLookup");
            OnPropertyChanged("RankLookup");
            OnPropertyChanged("BadgeLookup");
            OnPropertyChanged("PrintedLookup");
            OnPropertyChanged("PaidLookup");
        }

        private ItemGuidName conferenceSelected;
        private ItemGuidName rankSelected;
        private ItemGuidName badgeSelected;
        private ItemIntName printedSelected;
        private ItemIntName paidSelected;

        public ItemGuidName ConferenceSelected
        {
            get { return conferenceSelected; }
            set
            {
                conferenceSelected = value;
                OnPropertyChanged("ConferenceSelected");
                RefreshPrintCollection();
            }
        }
        public ItemGuidName RankSelected
        {
            get { return rankSelected; }
            set
            {
                rankSelected = value;
                OnPropertyChanged("RankSelected");
                RefreshPrintCollection();
            }
        }
        public ItemGuidName BadgeSelected
        {
            get { return badgeSelected; }
            set
            {
                badgeSelected = value;
                OnPropertyChanged("BadgeSelected");
                RefreshPrintCollection();
            }
        }
        public ItemIntName PrintedSelected
        {
            get { return printedSelected; }
            set
            {
                printedSelected = value;
                OnPropertyChanged("PrintedSelected");
                RefreshPrintCollection();
            }
        }
        public ItemIntName PaidSelected
        {
            get { return paidSelected; }
            set
            {
                paidSelected = value;
                OnPropertyChanged("PaidSelected");
                RefreshPrintCollection();
            }
        }

        private void RefreshPrintCollection()
        {
            PrintCollection = new ObservableCollection<ListBoxPrintBadge>(
                DataManager.Instance.GetPersonConferenceForPrint(ConferenceSelected, RankSelected, PrintedSelected, PaidSelected)
                .Select(o => new ListBoxPrintBadge(o)));
            OnPropertyChanged("PrintCollection");
        }
    }
}
