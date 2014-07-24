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
        private string filterMainAuthor;
        private string filterOtherAuthors;
        private string filterName;

        public AbstractWorkspaceViewModel()
        {
            AllAbstracts = new ObservableCollection<AbstractViewModel>();
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
            this.ApplyFiltersCommand = new DelegateCommand((o) => this.ApplyFilters());
        }
        public ObservableCollection<AbstractViewModel> AllAbstracts { get; private set; }

        public ICommand ApplyFiltersCommand { get; private set; }

        public AbstractViewModel CurrentAbstract
        {
            get { return this.currentAbstract; }
            set
            {
                this.currentAbstract = value;
                this.OnPropertyChanged("CurrentAbstract");
            }
        }

        private void ApplyFilters()
        {
            // TODO ApplyFilters
        }

        public string FilterMainAuthor
        {
            get { return filterMainAuthor; }
            set
            {
                this.filterMainAuthor = value;
                OnPropertyChanged("FilterMainAuthor");
            }
        }

        public string FilterOtherAuthors
        {
            get { return this.filterOtherAuthors; }
            set
            {
                this.filterOtherAuthors = value;
                OnPropertyChanged("FilterOtherAuthors");
            }
        }

        public string FilterName
        {
            get { return this.filterName; }
            set
            {
                this.filterName = value;
                OnPropertyChanged("FilterName");
            }
        }
    }
}
