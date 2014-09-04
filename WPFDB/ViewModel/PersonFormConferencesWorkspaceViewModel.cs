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
    public class PersonFormConferencesWorkspaceViewModel : ViewModelBase
    {

        private DataManager dm = DataManager.Instance;
        
    //    public PersonConference Model { get; private set; }

             
              public ICommand SaveCommand { get; private set; }

        public PersonFormConferencesWorkspaceViewModel(PersonViewModel person)
        {
                     this.SaveCommand = new DelegateCommand((o) => this.Save());
        }

     

        private void Save()
        {
            this.dm.Save();
        }


    }
}
