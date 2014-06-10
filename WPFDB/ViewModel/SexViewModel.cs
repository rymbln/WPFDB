using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class SexViewModel : ViewModelBase
    {
        public SexViewModel(Sex sex)
        {
            if (sex == null)
            {
                throw new ArgumentNullException("sex");
            }
            this.Model = sex;
        }

        public Sex Model { get; private set; }

        public string Name { get { return this.Model.Name; } set { this.Model.Name = value; this.OnPropertyChanged("Name"); } }
        public string Id
        {
            get { return this.Model.Id.ToString(); }
            set {}
        }
    }
}

