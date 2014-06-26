using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Data;
using WPFDB.View;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private DataManager dm = DataManager.Instance;


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        ///         /// <param name="unitOfWork">DataManager for co-ordinating changes</param>
        /// <param name="specialityRepository">Repository for querying department data</param>

        public MainViewModel()
        {
            this.PersonWorkspace = new PersonWorkspaceViewModel();
            this.SaveCommand = new DelegateCommand((o) => this.Save());
            this.OpenKnowallsCommand = new DelegateCommand((o) => this.OpenKnowalls());
            this.FillDataCommand = new DelegateCommand((o) => this.FillDatabase());

        }

        /// <summary>
        /// Gets the command to save all changes made in the current sessions DataManager
        /// </summary>
        public ICommand SaveCommand { get; private set; }
        public ICommand OpenKnowallsCommand { get; private set; }
        public ICommand FillDataCommand { get; private set; }

        /// <summary>
        /// Gets the workspace for managing departments of the company
        /// </summary>
        public SpecialityWorkspaceViewModel SpecialityWorkspace { get; private set; }
        public SexWorkspaceViewModel SexWorkspace { get; private set; }
        public ScienceDegreeWorkspaceViewModel ScienceDegreeWorkspace { get; private set; }
        public ScienceStatusWorkspaceViewModel ScienceStatusWorkspace { get; private set; }
        public ConferenceWorkspaceViewModel ConferenceWorkspace { get; private set; }
        public PersonWorkspaceViewModel PersonWorkspace { get; private set; }

        /// <summary>
        /// Saves all changes made in the current sessions DataManager
        /// </summary>
        private void Save()
        {
            this.dm.Save();
        }

        private void FillDatabase()
        {
            dm.FillData();
        }

        private void OpenKnowalls()
        {
            KnowallViewModel vm = new KnowallViewModel();
            KnowallView v = new KnowallView { DataContext = vm };
            v.Show();
        }
    }
}
