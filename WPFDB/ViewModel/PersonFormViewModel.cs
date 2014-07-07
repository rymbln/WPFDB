using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.View;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class PersonFormViewModel : ViewModelBase
    {
        private PersonViewModel currentPerson { get; set; }
        private DataManager dm = DataManager.Instance;

        public PersonFormViewModel(PersonViewModel person)
        {
            this.CurrentPerson = person;

            this.SaveCommand = new DelegateCommand((o) => this.Save());
            this.CancelCommand = new DelegateCommand((o) => this.Cancel());
            this.PersonFormConferenceWorkspace = new PersonFormConferencesWorkspaceViewModel(CurrentPerson);
            this.PersonFormContactsWorkspace = new PersonFormContactsWorkspaceViewModel(CurrentPerson);
        }

        public PersonViewModel CurrentPerson
        {
            get { return this.currentPerson; }
            set
            {
                this.currentPerson = value; this.OnPropertyChanged("CurrentPerson");
            }
        }
        public PersonFormConferencesWorkspaceViewModel PersonFormConferenceWorkspace { get; private set; }
        public PersonFormContactsWorkspaceViewModel PersonFormContactsWorkspace { get; private set; }


        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private void Save()
        {
            dm.Save();
        }

        private void Cancel()
        {
            dm.Rollback();
        }
    }
}
