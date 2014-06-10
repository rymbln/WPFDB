using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;
using WPFDB.ViewModel;

namespace WPFDB.ViewModel
{
    public class ScienceStatusViewModel: ViewModelBase
    {
        public ScienceStatusViewModel(ScienceStatus scienceStatus)
        {
            if (scienceStatus == null)
            {
                throw new ArgumentNullException("scienceStatus");
            }

            this.Model = scienceStatus;
        }

        public ScienceStatus Model { get; private set; }

        public string Name
        {
            get { return this.Model.Name; }
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
