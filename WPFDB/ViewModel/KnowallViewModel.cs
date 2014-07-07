using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Data;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class KnowallViewModel: ViewModelBase
    {
        private DataManager dm = DataManager.Instance;

        public KnowallViewModel()
        {
            this.SaveCommand = new DelegateCommand((o) => this.Save());
          
            this.SpecialityWorkspace = new SpecialityWorkspaceViewModel();
            this.SexWorkspace = new SexWorkspaceViewModel();
            this.ScienceDegreeWorkspace = new ScienceDegreeWorkspaceViewModel();
            this.ScienceStatusWorkspace = new ScienceStatusWorkspaceViewModel();
            this.ConferenceWorkspace = new ConferenceWorkspaceViewModel();
            this.CompanyWorkspace = new CompanyWorkspaceViewModel();
            this.RankWorkspace = new RankWorkspaceViewModel();
            this.UserWorkspace = new UserWorkspaceViewModel();
            this.PaymentTypeWorkspace = new PaymentTypeWorkspaceViewModel();
            this.OrderStatusWorkspace = new OrderStatusWorkspaceViewModel();
            this.ContactTypeWorkspace = new ContactTypeWorkspaceViewModel();

        }

        public SpecialityWorkspaceViewModel SpecialityWorkspace { get; private set; }
        public SexWorkspaceViewModel SexWorkspace { get; private set; }
        public ScienceDegreeWorkspaceViewModel ScienceDegreeWorkspace { get; private set; }
        public ScienceStatusWorkspaceViewModel ScienceStatusWorkspace { get; private set; }
        public ConferenceWorkspaceViewModel ConferenceWorkspace { get; private set; }
        public CompanyWorkspaceViewModel CompanyWorkspace { get; private set; }
        public RankWorkspaceViewModel RankWorkspace { get; private set; }
        public PaymentTypeWorkspaceViewModel PaymentTypeWorkspace { get; private set; }
        public UserWorkspaceViewModel UserWorkspace { get; private set; }
        public OrderStatusWorkspaceViewModel OrderStatusWorkspace { get; private set; }
        public ContactTypeWorkspaceViewModel ContactTypeWorkspace { get; private set; }

        public ICommand SaveCommand { get; private set; }

        private void Save()
        {
            this.dm.Save();
        }
    }
}
