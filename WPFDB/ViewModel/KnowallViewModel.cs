using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Data;

namespace WPFDB.ViewModel
{
    public class KnowallViewModel: ViewModelBase
    {
        private DataManager dm = DataManager.Instance;

        public KnowallViewModel()
        {


            var allSpecialities = new ObservableCollection<SpecialityViewModel>();
            var allSexes = new ObservableCollection<SexViewModel>();
            var allScienceStatuses = new ObservableCollection<ScienceStatusViewModel>();
            var allScienceDegree = new ObservableCollection<ScienceDegreeViewModel>();
            var allConferences = new ObservableCollection<ConferenceViewModel>();


 
 

          
            this.SpecialityWorkspace = new SpecialityWorkspaceViewModel();
            this.SexWorkspace = new SexWorkspaceViewModel();
            this.ScienceDegreeWorkspace = new ScienceDegreeWorkspaceViewModel();
            this.ScienceStatusWorkspace = new ScienceStatusWorkspaceViewModel();
            this.ConferenceWorkspace = new ConferenceWorkspaceViewModel();

        }

        public SpecialityWorkspaceViewModel SpecialityWorkspace { get; private set; }
        public SexWorkspaceViewModel SexWorkspace { get; private set; }
        public ScienceDegreeWorkspaceViewModel ScienceDegreeWorkspace { get; private set; }
        public ScienceStatusWorkspaceViewModel ScienceStatusWorkspace { get; private set; }
        public ConferenceWorkspaceViewModel ConferenceWorkspace { get; private set; }

        public ICommand SaveCommand { get; private set; }

        private void Save()
        {
            this.dm.Save();
        }
    }
}
