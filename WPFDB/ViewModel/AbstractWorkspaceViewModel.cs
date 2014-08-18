using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class AbstractWorkspaceViewModel : ViewModelBase
    {
        private AbstractViewModel currentAbstract;
        private AbstractWorkViewModel currentAbstractWork;
        private string filterMainAuthor;
        private string filterOtherAuthors;
        private string filterName;

        public AbstractWorkspaceViewModel()
        {
            AllAbstracts = new ObservableCollection<AbstractViewModel>();
            AbstractWorks = new ObservableCollection<AbstractWorkViewModel>();
            foreach (var item in DataManager.Instance.GetAllAbstracts())
            {
                AllAbstracts.Add(new AbstractViewModel(item));
            }
            this.CurrentAbstract = AllAbstracts.Count > 0 ? AllAbstracts[0] : null;
            this.AllAbstracts.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentAbstract))
                {
                    this.CurrentAbstract = null;
                }
            };

            this.AbstractWorks.CollectionChanged += (sender, e) =>
            {
                    this.CurrentAbstractWork = AbstractWorks.FirstOrDefault();
            };

            //    this.ApplyFiltersCommand = new DelegateCommand((o) => this.ApplyFilters());
            this.AddAbstractWorkCommand = new DelegateCommand((o) => this.AddAbstractWork());
            this.DeleteAbstractWorkCommand = new DelegateCommand((o) => this.DeleteAbstractWork(), (o) => this.CurrentAbstractWork != null);

        }
        public ObservableCollection<AbstractViewModel> AllAbstracts { get; private set; }
        public ObservableCollection<AbstractWorkViewModel> AbstractWorks { get; private set; }

        public ICommand ApplyFiltersCommand { get; private set; }


        public ICommand AddAbstractWorkCommand { get; private set; }
        public ICommand DeleteAbstractWorkCommand { get; private set; }
        public AbstractViewModel CurrentAbstract
        {
            get { return this.currentAbstract; }
            set
            {
                this.currentAbstract = value;
                this.OnPropertyChanged("CurrentAbstract");

                if (currentAbstract != null && currentAbstract.Model.AbstractWorks != null)
                {
                    if (this.currentAbstract.Model.AbstractWorks.Count > 0)
                    {
                        AbstractWorks = new ObservableCollection<AbstractWorkViewModel>();
                        foreach (var abstractWork in this.currentAbstract.Model.AbstractWorks)
                        {
                            AbstractWorks.Add(new AbstractWorkViewModel(abstractWork));
                        }
                    }
                }
                this.OnPropertyChanged("AbstractWorks");
            }
        }
        public AbstractWorkViewModel CurrentAbstractWork
        {
            get
            {
            //    this.currentAbstractWork = this.AbstractWorks.FirstOrDefault();
                return this.currentAbstractWork;
            }
            set
            {
                this.currentAbstractWork = value;
                OnPropertyChanged("CurrentAbstractWork");
            }
        }


        private void AddAbstractWork()
        {
            var abstractWork = DefaultManager.Instance.DefaultAbstractWork;
            DataManager.Instance.AddAbstractWorkToAbstract(currentAbstract, abstractWork);

            var vm = new AbstractWorkViewModel(abstractWork);
            this.AbstractWorks.Add(vm);
            this.CurrentAbstractWork = vm;
        }

        private void DeleteAbstractWork()
        {
            DataManager.Instance.RemoveAbstractWork(CurrentAbstractWork.Model);
            AbstractWorks.Remove(CurrentAbstractWork);
            CurrentAbstractWork = null;
        }
    }
}

