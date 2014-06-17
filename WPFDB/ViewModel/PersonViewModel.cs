using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Common;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class PersonViewModel : BasicPersonViewModel
    {
        private ScienceDegreeViewModel scienceDegree;
        private ScienceStatusViewModel scienceStatus;
        private SexViewModel sex;
        private SpecialityViewModel speciality;

        private IUnitOfWork unitOfWork;

        public PersonViewModel(Person person,
            ObservableCollection<ScienceDegreeViewModel> scienceDegreeLookup,
            ObservableCollection<ScienceStatusViewModel> scienceStatusLookup,
            ObservableCollection<SexViewModel> sexLookup,
            ObservableCollection<SpecialityViewModel> specialityLookup,
            IUnitOfWork unitOfWork
            )
            : base(person)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }

            this.unitOfWork = unitOfWork;
            this.ScienceDegreeLookup = scienceDegreeLookup;
            this.ScienceStatusLookup = scienceStatusLookup;
            this.SexLookup = sexLookup;
            this.SpecialityLookup = specialityLookup;

            this.ScienceDegreeLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.ScienceDegree))
                {
                    this.ScienceDegree = null;
                }
            };

            this.ScienceStatusLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.ScienceStatus))
                {
                    this.ScienceStatus = null;
                }
            };
            this.SexLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.Sex))
                {
                    this.Sex = null;
                }
            };
            this.SpecialityLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.Speciality))
                {
                    this.Speciality = null;
                }
            };
        }

        public  ObservableCollection<ScienceDegreeViewModel> ScienceDegreeLookup { get; private set; }
        public    ObservableCollection<ScienceStatusViewModel> ScienceStatusLookup { get; private set; }
        public    ObservableCollection<SexViewModel> SexLookup { get; private set; }
        public ObservableCollection<SpecialityViewModel> SpecialityLookup { get; private set; }

        public ScienceDegreeViewModel ScienceDegree
        {
            get
            {
                if (this.Model.ScienceDegree == null)
                {
                    return null;
                }
                else if (this.scienceDegree == null || this.scienceDegree.Model != this.Model.ScienceDegree)
                {
                    this.scienceDegree =
                        this.ScienceDegreeLookup.Where(s => s.Model == this.Model.ScienceDegree).SingleOrDefault();
                }
                return this.scienceDegree;
            }

            set
            {
                this.scienceDegree = value;
                this.Model.ScienceDegree = (value == null) ? null : value.Model;
                this.OnPropertyChanged("ScienceDegree");
            }
        }
        public ScienceStatusViewModel ScienceStatus
        {
            get
            {
                if (this.Model.ScienceStatus == null)
                {
                    return null;
                }
                else if (this.scienceStatus == null || this.scienceStatus.Model != this.Model.ScienceStatus)
                {
                    this.scienceStatus =
                        this.ScienceStatusLookup.Where(s => s.Model == this.Model.ScienceStatus).SingleOrDefault();
                }
                return this.scienceStatus;
            }

            set
            {
                this.scienceStatus = value;
                this.Model.ScienceStatus = (value == null) ? null : value.Model;
                this.OnPropertyChanged("ScienceStatus");
            }
        }

        public SexViewModel Sex
        {
            get
            {
                if (this.Model.Sex == null)
                {
                    return null;
                }
                else if (this.sex == null || this.sex.Model != this.Model.Sex)
                {
                    this.sex =
                        this.SexLookup.Where(s => s.Model == this.Model.Sex).SingleOrDefault();
                }
                return this.sex;
            }

            set
            {
                this.sex = value;
                this.Model.Sex = (value == null) ? null : value.Model;
                this.OnPropertyChanged("Sex");
            }
        }

        public SpecialityViewModel Speciality
        {
            get
            {
                if (this.Model.Speciality == null)
                {
                    return null;
                }
                else if (this.speciality == null || this.speciality.Model != this.Model.Speciality)
                {
                    this.speciality =
                        this.SpecialityLookup.Where(s => s.Model == this.Model.Speciality).SingleOrDefault();
                }
                return this.speciality;
            }

            set
            {
                this.speciality = value;
                this.Model.Speciality = (value == null) ? null : value.Model;
                this.OnPropertyChanged("Speciality");
            }
        }


    }
}
