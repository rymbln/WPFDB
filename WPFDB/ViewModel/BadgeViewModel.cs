using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class BadgeViewModel:ViewModelBase
    {
        public static int Errors { get; set; }
        public BadgeType Model { get; private set; }

        public BadgeViewModel(BadgeType obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("BadgeType");
            }
            this.Model = obj;
        }

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
        public int Width
        {
            get { return this.Model.Width != null ? this.Model.Width : 0; }
            set { this.Model.Width = value; this.OnPropertyChanged("Width"); }
        }
        public int Height
        {
            get { return this.Model.Height != null ? this.Model.Height : 0; }
            set { this.Model.Height = value; this.OnPropertyChanged("Height"); }
        }
      
        
    }
}
