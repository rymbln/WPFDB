using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Data;
using WPFDB.Model;
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
            PersonWorkspace = new PersonWorkspaceViewModel();
            AbstractWorkspace = new AbstractWorkspaceViewModel();
            ImportWorkspace = new ImportWorkspaceViewModel();

            SaveCommand = new DelegateCommand((o) => this.Save());
            OpenKnowallsCommand = new DelegateCommand((o) => this.OpenKnowalls());
            FillDataCommand = new DelegateCommand((o) => this.FillDatabase());
            EraseDataCommand = new DelegateCommand((o) => this.EraseData());
            OpenSettingsCommand = new DelegateCommand(o => OpenSettings());
        }

        /// <summary>
        /// Gets the command to save all changes made in the current sessions DataManager
        /// </summary>
        public ICommand SaveCommand { get; private set; }
        public ICommand OpenKnowallsCommand { get; private set; }
        public ICommand OpenSettingsCommand { get; private set; }
        public ICommand FillDataCommand { get; private set; }
        public ICommand EraseDataCommand { get; private set; }

        /// <summary>
        /// Gets the workspace for managing departments of the company
        /// </summary>
        //public SpecialityWorkspaceViewModel SpecialityWorkspace { get; private set; }
        //public SexWorkspaceViewModel SexWorkspace { get; private set; }
        //public ScienceDegreeWorkspaceViewModel ScienceDegreeWorkspace { get; private set; }
        //public ScienceStatusWorkspaceViewModel ScienceStatusWorkspace { get; private set; }
        //public ConferenceWorkspaceViewModel ConferenceWorkspace { get; private set; }
        public PersonWorkspaceViewModel PersonWorkspace { get; private set; }
        public AbstractWorkspaceViewModel AbstractWorkspace { get; private set; }
        public ImportWorkspaceViewModel ImportWorkspace { get; private set; }
        /// <summary>
        /// Saves all changes made in the current sessions DataManager
        /// </summary>
        /// 
        /// 
        private void Save()
        {
            this.dm.Save();
        }

        private void FillDatabase()
        {
            dm.FillData();
            MessageBox.Show("Data Filled!");
        }

        private void EraseData()
        {
            dm.EraseData();
            MessageBox.Show("Data Erased!");
        }

        private void OpenKnowalls()
        {
            KnowallViewModel vm = new KnowallViewModel();
            KnowallView v = new KnowallView { DataContext = vm };
            v.Show();
        }

        private void OpenSettings()
        {
            SettingsViewModel vm = new SettingsViewModel();
            SettingsView v = new SettingsView { DataContext = vm };
            v.Show();
        }
        public string ACTIVE_CONFERENCE
        {
            get { return "Текущая конференция: " + DefaultManager.Instance.DefaultConference.Name; }

        }
        public string IsConferenceMode
        {
            get
            {
                if (DefaultManager.Instance.ConferenceMode)
                {
                    return "Режим конференции: ВКЛ";
                }
                else
                {
                    return "Режим конференции: ВЫКЛ";
                }
                ;
            }

        }
        public string IsRegistrationMode
        {
            get
            {
                if (DefaultManager.Instance.RegistrationMode)
                {
                    return "Режим регистрации: ВКЛ";
                }
                else
                {
                    return "Режим регистрации: ВЫКЛ";
                }
                ;
            }

        }


    }
}
