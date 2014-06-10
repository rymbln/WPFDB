using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class ScienceDegreeViewModel: ViewModelBase
    {
        public ScienceDegreeViewModel(ScienceDegree obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("scienceDegree");
            }

            this.Model = obj;
        }

        /// <summary>
        /// Gets the underlying Department this ViewModel is based on
        /// </summary>
        public ScienceDegree Model { get; private set; }

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
            set {}
      
        }
    
    }
}
