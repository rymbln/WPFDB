using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class CompanyWorkspaceViewModel: ViewModelBase
    {
        private CompanyViewModel currentCompany;
        private DataManager dm = DataManager.Instance;

      public CompanyWorkspaceViewModel()
        {
            AllCompanys = new ObservableCollection<CompanyViewModel>();
            foreach (var item in dm.GetAllCompanies())
            {
                AllCompanys.Add(new CompanyViewModel(item));
            }
            this.CurrentCompany = this.AllCompanys.Count > 0 ? this.AllCompanys[0] : null;
            this.AllCompanys.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentCompany))
                {
                    this.CurrentCompany = null;
                }
            };

            this.AddCompanyCommand = new DelegateCommand((o) => this.AddCompany());
            this.DeleteCompanyCommand = new DelegateCommand((o) => this.DeleteCurrentCompany(), (o) => this.CurrentCompany != null);
     

        }

        public ICommand SaveCommand { get; private set; }

        public ObservableCollection<CompanyViewModel> AllCompanys { get; private set; }

        public CompanyViewModel CurrentCompany
        {
            get { return currentCompany; }
            set { currentCompany = value; OnPropertyChanged("CurrentCompany"); }
        }

        public ICommand AddCompanyCommand { get; private set; }
        public ICommand DeleteCompanyCommand { get; private set; }

        public void AddCompany()
        {
            Company c = this.dm.CreateObject<Company>();
            c.Id = GuidComb.Generate();
            dm.AddCompany(c);

            CompanyViewModel vm = new CompanyViewModel(c);
            AllCompanys.Add(vm);
            CurrentCompany = vm;
        }

        private void DeleteCurrentCompany()
        {
            dm.RemoveCompany(CurrentCompany.Model);
            AllCompanys.Remove(CurrentCompany);
            CurrentCompany = null;
        }
    }
}
