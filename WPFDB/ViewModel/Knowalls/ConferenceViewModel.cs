using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;
using WPFDB.Validation.Helpers;

namespace WPFDB.ViewModel
{
    public class ConferenceViewModel : ViewModelBase
    {
        public static int Errors { get; set; }

        public ConferenceViewModel(Conference conference)
        {
            if (conference == null)
            {
                throw new ArgumentNullException("conference");
            }
            this.Model = conference;
        }

        public Conference Model { get; private set; }


        public string Id
        {
            get { return this.Model.Id.ToString(); }
            set { }
        }

        public string Name
        {
            get { return this.Model.Name; }
            set { this.Model.Name = value; this.OnPropertyChanged("Name"); }
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
