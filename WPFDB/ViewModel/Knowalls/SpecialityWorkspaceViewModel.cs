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
    public class SpecialityWorkspaceViewModel: ViewModelBase
    {
        /// <summary>
        /// The speciality currently selected in the workspace
        /// </summary>
        private SpecialityViewModel  currentSpeciality;

        /// <summary>
        /// DataManager for managing changes
        /// </summary>
   private DataManager dm = DataManager.Instance;



        /// <summary>
        /// Initializes a new instance of the SpecialityWorkspaceViewModel class.
        /// </summary>
        /// <param name="specialities">The specialities to be managed</param>
        /// <param name="unitOfWork">DataManager for managing changes</param>
        public SpecialityWorkspaceViewModel(  )
        {
            AllSpecialities = new ObservableCollection<SpecialityViewModel>();
            foreach (var item in dm.GetAllSpecialities())
            {
                AllSpecialities.Add(new SpecialityViewModel(item));
            }
            this.CurrentSpeciality = this.AllSpecialities.Count > 0 ? this.AllSpecialities[0] : null;

            this.AllSpecialities.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentSpeciality))
                {
                    this.CurrentSpeciality = null;
                }
            };

            this.AddSpecialityCommand = new DelegateCommand((o) => this.AddSpeciality());
            this.DeleteSpecialityCommand = new DelegateCommand((o) => this.DeleteCurrentSpeciality(), (o) => this.CurrentSpeciality != null);
        }

        /// <summary>
        /// Gets all specialities whithin the company
        /// </summary>
        public ObservableCollection<SpecialityViewModel> AllSpecialities { get; private set; }
        /// <summary>
        /// Gets or sets the speciality currently selected in the workspace
        /// </summary>
        public SpecialityViewModel CurrentSpeciality
        {
            get
            {
                return this.currentSpeciality;
            }

            set
            {
                this.currentSpeciality = value;
                this.OnPropertyChanged("CurrentSpeciality");
            }
        }

        /// <summary>
        /// Gets the command for adding a new speciality
        /// </summary>
        public ICommand AddSpecialityCommand { get; private set; }

        /// <summary>
        /// Gets the command for deleting the current speciality
        /// </summary>
        public ICommand DeleteSpecialityCommand { get; private set; }

        /// <summary>
        /// Handles addition a new department to the workspace and model
        /// </summary>
        private void AddSpeciality()
        {
            Speciality sp = this.dm.CreateObject<Speciality>();
            sp.Id = GuidComb.Generate();
            this.dm.AddSpeciality(sp);

            SpecialityViewModel vm = new SpecialityViewModel(sp);
            this.AllSpecialities.Add(vm);
            this.CurrentSpeciality = vm;
        }

        /// <summary>
        /// Handles deletion of the current department
        /// </summary>
        private void DeleteCurrentSpeciality()
        {
            this.dm.RemoveSpeciality(this.CurrentSpeciality.Model);
            this.AllSpecialities.Remove(this.CurrentSpeciality);
            this.CurrentSpeciality = null;
        }
    }
}
