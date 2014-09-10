using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Common;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
   public class SettingsViewModel : ViewModelBase
    {
       public SettingsViewModel()
       {
           ConferenceLookup = new ObservableCollection<Conference>(DataManager.Instance.GetAllConferences());
       }

       public ObservableCollection<Conference> ConferenceLookup { get; private set; }

       public Conference ACTIVE_CONFERENCE
       {
           get { return DefaultManager.Instance.DefaultConference; }
           set
           {
               DefaultManager.Instance.DefaultConference = value;
               OnPropertyChanged("ACTIVE_CONFERENCE");
           }
       }
       public bool IsConferenceMode
       {
           get { return DefaultManager.Instance.ConferenceMode; }
           set
           {
               DefaultManager.Instance.ConferenceMode = value;
               OnPropertyChanged("IsConferenceMode");
           }
       }
       public bool IsRegistrationMode
       {
           get { return DefaultManager.Instance.RegistrationMode; }
           set
           {
               DefaultManager.Instance.RegistrationMode = value;
               OnPropertyChanged("IsRegistrationMode");
           }
       }
    }
}
