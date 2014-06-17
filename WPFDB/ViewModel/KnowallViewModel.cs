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
        private IUnitOfWork unitOfWork;

        public KnowallViewModel(IUnitOfWork unitOfWork,
            ISpecialityRepository specialityRepository,
            ISexRepository sexRepository,
            IScienceDegreeRepository scienceDegreeRepository,
            IScienceStatusRepository scienceStatusRepository,
            IConferenceRepository conferenceRepository)
        {
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

            ObservableCollection<SpecialityViewModel> allSpecialities = new ObservableCollection<SpecialityViewModel>();
            ObservableCollection<SexViewModel> allSexes = new ObservableCollection<SexViewModel>();
            ObservableCollection<ScienceStatusViewModel> allScienceStatuses = new ObservableCollection<ScienceStatusViewModel>();
            ObservableCollection<ScienceDegreeViewModel> allScienceDegree = new ObservableCollection<ScienceDegreeViewModel>();
            ObservableCollection<ConferenceViewModel> allConferences = new ObservableCollection<ConferenceViewModel>();

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
            this.SpecialityWorkspace = new SpecialityWorkspaceViewModel(allSpecialities, unitOfWork);
            this.SexWorkspace = new SexWorkspaceViewModel(allSexes, unitOfWork);
            this.ScienceDegreeWorkspace = new ScienceDegreeWorkspaceViewModel(allScienceDegree, unitOfWork);
            this.ScienceStatusWorkspace = new ScienceStatusWorkspaceViewModel(allScienceStatuses, unitOfWork);
            this.ConferenceWorkspace = new ConferenceWorkspaceViewModel(allConferences, unitOfWork);

        }

        public SpecialityWorkspaceViewModel SpecialityWorkspace { get; private set; }
        public SexWorkspaceViewModel SexWorkspace { get; private set; }
        public ScienceDegreeWorkspaceViewModel ScienceDegreeWorkspace { get; private set; }
        public ScienceStatusWorkspaceViewModel ScienceStatusWorkspace { get; private set; }
        public ConferenceWorkspaceViewModel ConferenceWorkspace { get; private set; }

        public ICommand SaveCommand { get; private set; }

        private void Save()
        {
            this.unitOfWork.Save();
        }
    }
}
