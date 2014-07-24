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
    public class PersonConferenceDetailViewModel : ViewModelBase
    {
        private RankViewModel rank;
        private CompanyViewModel company;

        public ObservableCollection<RankViewModel> RankLookup { get; private set; }
        public ObservableCollection<CompanyViewModel> CompanyLookup { get; private set; } 

        public PersonConferences_Detail Model { get; private set; }


        public PersonConferenceDetailViewModel(PersonConferences_Detail obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            this.Model = obj;

            RankLookup = new ObservableCollection<RankViewModel>();
            foreach (var rank in DataManager.Instance.GetAllRanks())
            {
                RankLookup.Add(new RankViewModel(rank));
            }
            RankLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.Rank))
                {
                    this.Rank = new RankViewModel(DefaultManager.Instance.DefaultRank);
                }
            };

            CompanyLookup = new ObservableCollection<CompanyViewModel>();
            foreach (var company in DataManager.Instance.GetAllCompanies())
            {
                CompanyLookup.Add(new CompanyViewModel(company));
            }
            CompanyLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.Company))
                {
                    this.Company = new CompanyViewModel(DefaultManager.Instance.DefaultCompany);
                }
            };


            
        }

        public DateTime? DateArrive
        {
            get { return (this.Model.DateArrive == null ) ? DateTime.Now : this.Model.DateArrive ; }
            set { this.Model.DateArrive = value; this.OnPropertyChanged("DateArrive"); }
        }
        public bool IsBadge
        {
            get { return this.Model.IsBadge; }
            set { this.Model.IsBadge = value; this.OnPropertyChanged("IsBadge"); }
        }
          public bool IsArrive
        {
            get { return this.Model.IsArrive; }
            set { this.Model.IsArrive = value; this.OnPropertyChanged("IsArrive"); }
        }
        public bool IsAbstract
        {
            get { return this.Model.IsAbstract; }
            set { this.Model.IsAbstract = value; this.OnPropertyChanged("IsAbstract"); }
        }
    public bool IsNeedBadge
        {
            get { return this.Model.IsNeedBadge; }
            set { this.Model.IsNeedBadge = value; this.OnPropertyChanged("IsNeedBadge"); }
        }
        public bool IsAutoreg
        {
            get { return this.Model.IsAutoreg; }
            set { this.Model.IsAutoreg = value; this.OnPropertyChanged("IsAutoreg"); }
        }

        public RankViewModel Rank
        {
            get
            {
                if (this.Model.Rank == null)
                {
                    this.Model.Rank = DefaultManager.Instance.DefaultRank;
                }
                this.rank = this.RankLookup.SingleOrDefault(r => r.Model == this.Model.Rank);
                return this.rank;
            }
            set
            {
                this.rank = value;
                this.Model.Rank = (value == null) ? null : value.Model;
                this.OnPropertyChanged("Rank");
            }
        }
        public CompanyViewModel Company
        {
            get
            {
                if (this.Model.Company == null)
                {
                    this.Model.Company = DefaultManager.Instance.DefaultCompany;
                }
                this.company = this.CompanyLookup.SingleOrDefault(r => r.Model == this.Model.Company);
                return this.company;
            }
            set
            {
                this.company = value;
                this.Model.Company = (value == null) ? null : value.Model;
                this.OnPropertyChanged("Company");
            }
        }
    }
}
