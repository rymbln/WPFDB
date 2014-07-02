using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class RankViewModel:ViewModelBase
    {
        public static int Errors { get; set; }
        public Rank Model { get; private set; }

        public RankViewModel(Rank rank)
        {
            if (rank == null)
            {
                throw new ArgumentNullException("rank");
            }
            this.Model = rank;
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

    }
}
