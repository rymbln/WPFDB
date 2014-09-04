using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.View;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class AbstractWorkspaceViewModel : ViewModelBase
    {
        private Abstract currentAbstract { get; set; }
        private AbstractViewModel currentAbstractVM { get; set; }
      

        private string filterText = "";

        public AbstractWorkspaceViewModel()
        {

            AllAbstracts = new ObservableCollection<Abstract>();
            AllAbstractsDB = new ObservableCollection<Abstract>();

            RefreshAbstracts();

            this.OpenAbstractCommand = new DelegateCommand((o) => this.OpenAbstract());
            this.DeleteAbstractCommand = new DelegateCommand((o) => this.DeleteAbstract());
            this.RefreshCommand = new DelegateCommand((o) => this.RefreshAbstracts());


        }
        public ObservableCollection<Abstract> AllAbstracts { get; private set; }
        public ObservableCollection<Abstract> AllAbstractsDB { get; private set; }

        public ICommand RefreshCommand { get; private set; }
        public ICommand ApplyFiltersCommand { get; private set; }


        public ICommand OpenAbstractCommand{ get; private set; }
        public ICommand DeleteAbstractCommand { get; private set; }

        public Abstract CurrentAbstract
        {
            get { return this.currentAbstract; }
            set
            {
                this.currentAbstract = value;
                if (CurrentAbstract != null)
                {
                    this.CurrentAbstractVM = new AbstractViewModel(currentAbstract);
                }
                this.OnPropertyChanged("CurrentAbstract");
            }
        }

        public AbstractViewModel CurrentAbstractVM
        {
            get { return this.currentAbstractVM; }
            set
            {
                currentAbstractVM = value;
                OnPropertyChanged("CurrentAbstractVM");
            }
        }

        private void DeleteAbstract()
        {
            DataManager.Instance.RemoveAbstract(CurrentAbstract);
            AllAbstracts.Remove(CurrentAbstract);
            OnPropertyChanged("AllAbstracts");
        }

        private void RefreshAbstracts()
        {
            filterText = "";
            this.OnPropertyChanged("FilterText");
            AllAbstractsDB = new ObservableCollection<Abstract>(DataManager.Instance.GetAllAbstracts());
            AllAbstracts = AllAbstractsDB;
            this.OnPropertyChanged("AllAbstracts");
            this.CurrentAbstract = AllAbstracts.Count > 0 ? AllAbstracts[0] : null;
        }

        private void FilterAbstracts()
        {
            AllAbstracts = new ObservableCollection<Abstract>(AllAbstractsDB.Where(o => o.ToFilterString.ToUpper().Contains(filterText.ToUpper())));
            this.OnPropertyChanged("AllAbstracts");
            this.CurrentAbstract = AllAbstracts.Count > 0 ? AllAbstracts[0] : null;
        }

        private void OpenAbstract()
        {
            AbstractFormViewModel vm = new AbstractFormViewModel(new AbstractViewModel(currentAbstract));
            AbstractFormView v = new AbstractFormView{DataContext = vm};
            v.Show();
        }

        public string FilterText
        {
            get { return this.filterText; }
            set
            {
                this.filterText = value.Trim();
                this.OnPropertyChanged("FilterText");
                if (filterText.Equals(""))
                {
                    RefreshAbstracts();
                }
                else
                {

                    FilterAbstracts();
                }
            }
        }

        //public AbstractWorkViewModel CurrentAbstractWork
        //{
        //    get
        //    {
        //    //    this.currentAbstractWork = this.AbstractWorks.FirstOrDefault();
        //        return this.currentAbstractWork;
        //    }
        //    set
        //    {
        //        this.currentAbstractWork = value;
        //        OnPropertyChanged("CurrentAbstractWork");
        //    }
        //}


        //private void AddAbstractWork()
        //{
        //    var abstractWork = DefaultManager.Instance.DefaultAbstractWork;
        //    DataManager.Instance.AddAbstractWorkToAbstract(currentAbstract, abstractWork);

        //    var vm = new AbstractWorkViewModel(abstractWork);
        //    this.AbstractWorks.Add(vm);
        //    this.CurrentAbstractWork = vm;
        //}

        //private void DeleteAbstractWork()
        //{
        //    DataManager.Instance.RemoveAbstractWork(CurrentAbstractWork.Model);
        //    AbstractWorks.Remove(CurrentAbstractWork);
        //    CurrentAbstractWork = null;
        //}
    }
}

