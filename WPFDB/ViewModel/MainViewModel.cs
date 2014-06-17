﻿using System;
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
        private IUnitOfWork unitOfWork;
        private ISpecialityRepository specialityRepository;
        private ISexRepository sexRepository;
        private IScienceDegreeRepository scienceDegreeRepository;
        private IScienceStatusRepository scienceStatusRepository;
        private IConferenceRepository conferenceRepository;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        ///         /// <param name="unitOfWork">UnitOfWork for co-ordinating changes</param>
        /// <param name="specialityRepository">Repository for querying department data</param>

        public MainViewModel(IUnitOfWork unitOfWork,
            ISpecialityRepository specialityRepository,
            ISexRepository sexRepository,
            IScienceDegreeRepository scienceDegreeRepository,
            IScienceStatusRepository scienceStatusRepository,
            IConferenceRepository conferenceRepository,
            IPersonRepository personRepository
            )
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            if (specialityRepository == null)
            {
                throw new ArgumentNullException("specialityRepository");
            }
            if (sexRepository == null)
            {
                throw new ArgumentNullException("specialityRepository");
            }
            if (scienceStatusRepository == null)
            {
                throw new ArgumentNullException("specialityRepository");
            }
            if (scienceDegreeRepository == null)
            {
                throw new ArgumentNullException("specialityRepository");
            }
            if (conferenceRepository == null)
            {
                throw new ArgumentNullException("conferenceRepository");
            }
            if (personRepository == null)
            {
                throw new ArgumentNullException("personRepository");
            }


            this.unitOfWork = unitOfWork;
            this.scienceDegreeRepository = scienceDegreeRepository;
            this.scienceStatusRepository = scienceStatusRepository;
            this.sexRepository = sexRepository;
            this.specialityRepository = specialityRepository;
            this.conferenceRepository = conferenceRepository;
            // Build data structures to populate areas of the application surface
            ObservableCollection<SpecialityViewModel> allSpecialities = new ObservableCollection<SpecialityViewModel>();
            ObservableCollection<SexViewModel> allSexes = new ObservableCollection<SexViewModel>();
            ObservableCollection<ScienceStatusViewModel> allScienceStatuses = new ObservableCollection<ScienceStatusViewModel>();
            ObservableCollection<ScienceDegreeViewModel> allScienceDegree = new ObservableCollection<ScienceDegreeViewModel>();
            ObservableCollection<ConferenceViewModel> allConferences = new ObservableCollection<ConferenceViewModel>();
            ObservableCollection<PersonViewModel> allPersons = new ObservableCollection<PersonViewModel>();

            foreach (var item in specialityRepository.GetAllSpecialities())
            {
                allSpecialities.Add(new SpecialityViewModel(item));
            }
            foreach (var item in sexRepository.GetAllSexes())
            {
                allSexes.Add(new SexViewModel(item));
            }
            foreach (var item in scienceStatusRepository.GetAllScienceStatuses())
            {
                allScienceStatuses.Add(new ScienceStatusViewModel(item));
            }
            foreach (var item in scienceDegreeRepository.GetAllScienceDegrees())
            {
                allScienceDegree.Add(new ScienceDegreeViewModel(item));
            }
            foreach (var item in conferenceRepository.GetAllConferences())
            {
                allConferences.Add(new ConferenceViewModel(item));
            }
            foreach (var item in personRepository.GetAllPersons())
            {
                allPersons.Add(new PersonViewModel(item, allScienceDegree, allScienceStatuses, allSexes, allSpecialities, unitOfWork));
            }

            this.SpecialityWorkspace = new SpecialityWorkspaceViewModel(allSpecialities, unitOfWork);
            this.SexWorkspace = new SexWorkspaceViewModel(allSexes, unitOfWork);
            this.ScienceDegreeWorkspace = new ScienceDegreeWorkspaceViewModel(allScienceDegree, unitOfWork);
            this.ScienceStatusWorkspace = new ScienceStatusWorkspaceViewModel(allScienceStatuses, unitOfWork);
            this.ConferenceWorkspace = new ConferenceWorkspaceViewModel(allConferences, unitOfWork);
            this.PersonWorkspace = new PersonWorkspaceViewModel(allPersons, allScienceDegree, allScienceStatuses, allSexes, allSpecialities, unitOfWork);
            this.SaveCommand = new DelegateCommand((o) => this.Save());
            this.OpenKnowallsCommand = new DelegateCommand((o) => this.OpenKnowalls());

        }

        /// <summary>
        /// Gets the command to save all changes made in the current sessions UnitOfWork
        /// </summary>
        public ICommand SaveCommand { get; private set; }
        public ICommand OpenKnowallsCommand { get; private set; }

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
        /// Saves all changes made in the current sessions UnitOfWork
        /// </summary>
        private void Save()
        {
            this.unitOfWork.Save();
        }

        private void OpenKnowalls()
        {
            KnowallViewModel vm = new KnowallViewModel(this.unitOfWork, this.specialityRepository, this.sexRepository, this.scienceDegreeRepository, this.scienceStatusRepository, this.conferenceRepository);
            KnowallView v = new KnowallView { DataContext = vm };
            v.Show();
        }
    }
}
