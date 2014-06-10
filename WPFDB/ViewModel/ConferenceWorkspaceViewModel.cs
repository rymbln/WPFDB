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
    public class ConferenceWorkspaceViewModel: ViewModelBase
    {
        private ConferenceViewModel currentConference;
        private IUnitOfWork unitOfWork;

        public ConferenceWorkspaceViewModel(ObservableCollection<ConferenceViewModel> conferences, IUnitOfWork unitOfWork )
        {
            if (conferences == null)
            {
                throw new ArgumentNullException("conference");
            }
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfwork");
            }
            this.unitOfWork = unitOfWork;
            this.AllConferences = conferences;
            this.CurrentConference = this.AllConferences.Count > 0 ? this.AllConferences[0] : null;
            this.AllConferences.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentConference))
                {
                    this.CurrentConference = null;
                }
            };

            this.AddConferenceCommand = new DelegateCommand((o) => this.AddConference());
            this.DeleteConferenceCommand = new DelegateCommand((o) => this.DeleteCurrentConference(), (o) => this.CurrentConference != null);
        }

        public ObservableCollection<ConferenceViewModel> AllConferences { get; private set; }

        public ConferenceViewModel CurrentConference
        {
            get { return currentConference; }
            set { currentConference = value; OnPropertyChanged("CurrentConference"); }
        }

        public ICommand AddConferenceCommand { get; private set; }
        public ICommand DeleteConferenceCommand { get; private set; }

        public void AddConference()
        {
            Conference c = this.unitOfWork.CreateObject<Conference>();
            c.Id = GuidComb.Generate();
            unitOfWork.AddConference(c);

            ConferenceViewModel vm = new ConferenceViewModel(c);
            AllConferences.Add(vm);
            CurrentConference = vm;
        }

        private void DeleteCurrentConference()
        {
            unitOfWork.RemoveConference(CurrentConference.Model);
            AllConferences.Remove(CurrentConference);
            CurrentConference = null;
        }
    }
}
