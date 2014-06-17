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
    public class PersonWorkspaceViewModel: ViewModelBase
    {
        private PersonViewModel currentPerson;
        private IUnitOfWork unitOfWork;
        private ObservableCollection<ScienceDegreeViewModel> scienceDegreeLookup;
        private ObservableCollection<ScienceStatusViewModel> scienceStatusLookup;
        private ObservableCollection<SexViewModel> sexLookup;
        private ObservableCollection<SpecialityViewModel> specialityLookup;

        public PersonWorkspaceViewModel(
            ObservableCollection<PersonViewModel> persons,
            ObservableCollection<ScienceDegreeViewModel> scienceDegreeLookup,
            ObservableCollection<ScienceStatusViewModel> scienceStatusLookup,
            ObservableCollection<SexViewModel> sexLookup,
            ObservableCollection<SpecialityViewModel> specialityLookup,
            IUnitOfWork unitOfWork
            )
        {
            if (persons == null)
            {
                throw new ArgumentNullException("persons");
            }
            if (scienceDegreeLookup == null)
            {
                throw new ArgumentNullException("scienceDegreeLookup");
            }
            if (scienceStatusLookup == null)
            {
                throw new ArgumentNullException("scienceStatusLookup");
            }
            if (sexLookup == null)
            {
                throw new ArgumentNullException("sexLookup");
            }
            if (specialityLookup == null)
            {
                throw new ArgumentNullException("specialityLookup");
            }
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            this.unitOfWork = unitOfWork;
            this.AllPersons = persons;
            this.scienceDegreeLookup = scienceDegreeLookup;
            this.scienceStatusLookup = scienceStatusLookup;
            this.sexLookup = sexLookup;
            this.specialityLookup = specialityLookup;
            this.CurrentPerson = persons.Count > 0 ? persons[0] : null;

            this.AllPersons.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentPerson))
                {
                    this.CurrentPerson = null;
                }
            };

            this.AddPersonCommand = new DelegateCommand((o) => this.AddPerson());
            this.DeletePersonCommand = new DelegateCommand((o) => this.DeleteCurrentPerson());

        }

        public ICommand AddPersonCommand { get; private set; }
        public ICommand DeletePersonCommand { get; private set; }

        public ObservableCollection<PersonViewModel> AllPersons { get; private set; }

        public PersonViewModel CurrentPerson
        {
            get { return this.currentPerson; }
            set { this.currentPerson = value; this.OnPropertyChanged("CurrentPerson"); }
        }

        private void AddPerson()
        {
            Person p = this.unitOfWork.CreateObject<Person>();
            p.Id = GuidComb.Generate();
            this.unitOfWork.AddPerson(p);

            PersonViewModel vm = new PersonViewModel(p, this.scienceDegreeLookup, this.scienceStatusLookup, this.sexLookup, this.specialityLookup, this.unitOfWork);
            this.AllPersons.Add(vm);
            this.CurrentPerson = vm;
        }

        private void DeleteCurrentPerson()
        {
            this.unitOfWork.RemovePerson(this.CurrentPerson.Model);
            this.AllPersons.Remove(this.CurrentPerson);
            this.CurrentPerson = null;

        }

    }
}
