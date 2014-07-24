using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFDB.Common;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class PersonViewModel : ViewModelBase
    {
        private ScienceDegreeViewModel scienceDegree;
        private ScienceStatusViewModel scienceStatus;
        private SexViewModel sex;
        private SpecialityViewModel speciality;

        private DataManager dm = DataManager.Instance;

        public PersonViewModel(Person person)
            
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }
            if (person.Iacmac == null)
            {
                person.Iacmac = new Iacmac{PersonId = person.Id, IsMember = false};
            }
            this.Model = person;
            ScienceDegreeLookup = new ObservableCollection<ScienceDegreeViewModel>();
            ScienceStatusLookup = new ObservableCollection<ScienceStatusViewModel>();
            SexLookup = new ObservableCollection<SexViewModel>();
            SpecialityLookup = new ObservableCollection<SpecialityViewModel>();


            foreach (var item in dm.GetAllSpecialities())
            {
                SpecialityLookup.Add(new SpecialityViewModel(item));
            }
            foreach (var item in dm.GetAllSexes())
            {
                SexLookup.Add(new SexViewModel(item));
            }
            foreach (var item in dm.GetAllScienceStatuses())
            {
                ScienceStatusLookup.Add(new ScienceStatusViewModel(item));
            }
            foreach (var item in dm.GetAllScienceDegrees())
            {
                ScienceDegreeLookup.Add(new ScienceDegreeViewModel(item));
            }
            //this.ScienceDegreeLookup = ScienceDegreeLookup;
            //this.ScienceStatusLookup = ScienceStatusLookup;
            //this.SexLookup = SexLookup;
            //this.SpecialityLookup = SpecialityLookup;

            this.ScienceDegreeLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.ScienceDegree))
                {
                    this.ScienceDegree = new ScienceDegreeViewModel(DefaultManager.Instance.DefaultScienceDegree);
                }
            };

            this.ScienceStatusLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.ScienceStatus))
                {
                    this.ScienceStatus = new ScienceStatusViewModel(DefaultManager.Instance.DefaultScienceStatus);
                }
            };
            this.SexLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.Sex))
                {
                    this.Sex = new SexViewModel(DefaultManager.Instance.DefaultSex);
                }
            };
            this.SpecialityLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.Speciality))
                {
                    this.Speciality = new SpecialityViewModel(DefaultManager.Instance.DefaultSpeciality);
                }
            };
        }

        public ObservableCollection<ScienceDegreeViewModel> ScienceDegreeLookup { get; private set; }
        public ObservableCollection<ScienceStatusViewModel> ScienceStatusLookup { get; private set; }
        public ObservableCollection<SexViewModel> SexLookup { get; private set; }
        public ObservableCollection<SpecialityViewModel> SpecialityLookup { get; private set; }

        public ScienceDegreeViewModel ScienceDegree
        {
            get
            {
                if (this.Model.ScienceDegree == null)
                {
                    this.Model.ScienceDegree = DefaultManager.Instance.DefaultScienceDegree;
     
                }
                    this.scienceDegree =
                        this.ScienceDegreeLookup.SingleOrDefault(s => s.Model == this.Model.ScienceDegree);
              
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
                    this.Model.ScienceStatus = DefaultManager.Instance.DefaultScienceStatus;
                }
                this.scienceStatus =
                        this.ScienceStatusLookup.SingleOrDefault(s => s.Model == this.Model.ScienceStatus);

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
                    this.Model.Sex = DefaultManager.Instance.DefaultSex;

                }
                this.sex =
                        this.SexLookup.SingleOrDefault(s => s.Model == this.Model.Sex);

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
                    this.Model.Speciality = DefaultManager.Instance.DefaultSpeciality;
                }
                this.speciality =
                        this.SpecialityLookup.SingleOrDefault(s => s.Model == this.Model.Speciality);

                return this.speciality;
            }

            set
            {
                this.speciality = value;
                this.Model.Speciality = (value == null) ? null : value.Model;
                this.OnPropertyChanged("Speciality");
            }
        }
        public Person Model { get; private set; }

        public string FirstName
        {
            get
            {
                return this.Model.FirstName;
            }

            set
            {
                this.Model.FirstName = value;
                this.OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get
            {
                return this.Model.SecondName;
            }

            set
            {
                this.Model.SecondName = value;
                this.OnPropertyChanged("SecondName");
            }
        }
        public string ThirdName
        {
            get
            {
                return this.Model.ThirdName;
            }

            set
            {
                this.Model.ThirdName = value;
                this.OnPropertyChanged("ThirdName");
            }
        }
        public DateTime? BirthDate
        {
            get
            {
                return this.Model.BirthDate;
            }

            set
            {
                this.Model.BirthDate = value;
                this.OnPropertyChanged("BirthDate");
            }
        }
        public string WorkPlace
        {
            get
            {
                return this.Model.WorkPlace;
            }

            set
            {
                this.Model.WorkPlace = value;
                this.OnPropertyChanged("WorkPlace");
            }
        }
        public string Post
        {
            get
            {
                return this.Model.Post;
            }

            set
            {
                this.Model.Post = value;
                this.OnPropertyChanged("Post");
            }
        }
        public string Id
        {
            get { return this.Model.Id.ToString(); }
            set { }
        }
        public int SourceId
        {
            get
            {
                return this.Model.SourceId;
            }

            set
            {
                this.Model.SourceId = value;
                this.OnPropertyChanged("SourceId");
            }
        }
        public bool IsMember
        {
            get
            {
                return this.Model.Iacmac.IsMember;
            }

            set
            {
                this.Model.Iacmac.IsMember = value;
                this.OnPropertyChanged("IsMember");
            }
        }
        public bool IsCardCreate
        {
            get
            {
                return this.Model.Iacmac.IsCardCreate;
            }

            set
            {
                this.Model.Iacmac.IsCardCreate = value;
                this.OnPropertyChanged("IsCardCreate");
            }
        }

        public string FullName
        {
            get { return this.Model.FirstName + " " + Model.SecondName + " " + Model.ThirdName; }
        }

        public string FullNameInitials
        {
            get { return this.Model.FirstName + " " + Model.SecondName.Substring(0,1) + "." + Model.ThirdName.Substring(0,1) + "."; }
        }

        public string Initials
        {
            get
            {
                return this.Model.FirstName.Substring(1, 1) + "." + this.Model.SecondName.Substring(1, 1) + "." +
                       this.Model.ThirdName.Substring(1, 1) + ".";
            }
        }
        public bool IsCardSent
        {
            get
            {
                return this.Model.Iacmac.IsCardSent;
            }

            set
            {
                this.Model.Iacmac.IsCardSent = value;
                this.OnPropertyChanged("IsCardSent");
            }
        }
        public bool IsForm
        {
            get
            {
                return this.Model.Iacmac.IsForm;
            }

            set
            {
                this.Model.Iacmac.IsForm = value;
                this.OnPropertyChanged("IsForm");
            }
        }
        public DateTime? DateRegistration
        {
            get
            {
                return (this.Model.Iacmac.DateRegistration == null) ? DateTime.Now : this.Model.Iacmac.DateRegistration;
            }

            set
            {
                this.Model.Iacmac.DateRegistration = value;
                this.OnPropertyChanged("DateRegistration");
            }
        }
        public int Number
        {
            get
            {
                return this.Model.Iacmac.Number;
            }

            set
            {
                this.Model.Iacmac.Number = value;
                this.OnPropertyChanged("Number");
            }
        }
        public string Code
        {
            get
            {
                return this.Model.Iacmac.Code;
            }

            set
            {
                this.Model.Iacmac.Code = value;
                this.OnPropertyChanged("Code");
            }
        }
    }
}
