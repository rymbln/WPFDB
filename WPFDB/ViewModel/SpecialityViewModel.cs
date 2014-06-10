using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDB.ViewModel
{
    using System;
    using WPFDB.Model;

    public class SpecialityViewModel : ViewModelBase
    {
        public SpecialityViewModel(Speciality speciality)
        {
            if (speciality == null)
            {
                throw new ArgumentNullException("speciality");
            }

            this.Model = speciality;
        }

        /// <summary>
        /// Gets the underlying Department this ViewModel is based on
        /// </summary>
        public Speciality Model { get; private set; }

        /// <summary>
        /// Gets or sets the name of this department
        /// </summary>
        public string Name
        {
            get
            {
                return this.Model.Name;
            }

            set
            {
                this.Model.Name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public string Id
        {

            get
            {
                return this.Model.Id.ToString();
            }
            set
            {
                
            }

        }

        public string Code
        {
            get
            {
                return this.Model.Code;
            }

            set
            {
                this.Model.Code = value;
                this.OnPropertyChanged("Code");
            }
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
