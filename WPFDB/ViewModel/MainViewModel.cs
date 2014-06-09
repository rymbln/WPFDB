using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Data;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        ///         /// <param name="unitOfWork">UnitOfWork for co-ordinating changes</param>
        /// <param name="specialityRepository">Repository for querying department data</param>

        public MainViewModel(IUnitOfWork unitOfWork, ISpecialityRepository specialityRepository)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            if (specialityRepository == null)
            {
                throw new ArgumentNullException("specialityRepository");
            }

            this.unitOfWork = unitOfWork;

            // Build data structures to populate areas of the application surface
            ObservableCollection<SpecialityViewModel> allSpecialities = new ObservableCollection<SpecialityViewModel>();

            foreach (var sp in specialityRepository.GetAllSpecialities())
            {
                allSpecialities.Add(new SpecialityViewModel(sp));
            }

            this.SpecialityWorkspace = new SpecialityWorkspaceViewModel(allSpecialities, unitOfWork);
            this.SaveCommand = new DelegateCommand((o) => this.Save());
        }

        /// <summary>
        /// Gets the command to save all changes made in the current sessions UnitOfWork
        /// </summary>
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// Gets the workspace for managing departments of the company
        /// </summary>
        public SpecialityWorkspaceViewModel SpecialityWorkspace { get; private set; }

        /// <summary>
        /// Saves all changes made in the current sessions UnitOfWork
        /// </summary>
        private void Save()
        {
            this.unitOfWork.Save();
        }
    }
}
