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
   public class BadgeSettingsViewModel: ViewModelBase
    {
       private ListBoxBadgeDefault badgeDefaultSelected;

       public BadgeSettingsViewModel()
       {
           Load();
           AddCommand = new DelegateCommand(o => Add());
           DeleteCommand = new DelegateCommand(o => Delete(), o => BadgeDefaultSelected != null);
           SaveCommand = new DelegateCommand(o => Save());

       }

       public ObservableCollection<ListBoxBadgeDefault> BadgesDefaultsCollection { get; set; }
   

       public ICommand AddCommand { get; set; }
       public ICommand DeleteCommand { get; set; }
       public ICommand SaveCommand { get; set; }

       public ListBoxBadgeDefault BadgeDefaultSelected
       {
           get { 
               
               if (badgeDefaultSelected == null)
               {
                   badgeDefaultSelected = BadgesDefaultsCollection.FirstOrDefault();
               }

               return badgeDefaultSelected; }

           set
           {
               badgeDefaultSelected = value;
               OnPropertyChanged("BadgeDefaultSelected");
           }
       }

       private void Load()
       {
           try
           {
               BadgesDefaultsCollection = new ObservableCollection<ListBoxBadgeDefault>(
                   DataManager.Instance.GetAllBadgesDefaults().Select(o=> new ListBoxBadgeDefault(o)));
               OnPropertyChanged("BadgesDefaultsCollection");

             
           }
           catch (Exception ex)
           {
               LogManager.Write(ex);
           }
       }

       private void Save()
       {
           DataManager.Instance.Save();
       }

       private void Add()
       {
           var obj = DataManager.Instance.CreateObject<BadgesDefault>();
           obj.Id = GuidComb.Generate();
           obj.RankId = DataManager.Instance.GetAllRanks().FirstOrDefault().Id;
           obj.BadgeTypeId = DataManager.Instance.GetAllBadges().FirstOrDefault().Id;
           DataManager.Instance.AddBadgesDefault(obj);
           BadgesDefaultsCollection.Add(new ListBoxBadgeDefault(obj));
           OnPropertyChanged("BadgesDefaultsCollection");
           BadgeDefaultSelected = new ListBoxBadgeDefault(obj);
       }

       private void Delete()
       {
           DataManager.Instance.DeleteBadgesDefault(BadgeDefaultSelected.Model);
           Load();
           BadgeDefaultSelected = null;
       }

    }
}
