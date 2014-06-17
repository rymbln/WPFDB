using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class BasicPersonViewModel: ViewModelBase
    {
        public BasicPersonViewModel(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }
            this.Model = person;
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


    }
}
