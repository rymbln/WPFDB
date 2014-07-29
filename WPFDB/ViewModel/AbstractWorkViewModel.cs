using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class AbstractWorkViewModel : ViewModelBase
    {
        public AbstractWork Model;
        private UserViewModel responsiblePerson;
        private AbstractStatusViewModel abstractStatus;

        public AbstractWorkViewModel(AbstractWork abstractWork)
        {
            if (abstractWork == null)
            {
                throw new ArgumentNullException("abstractWork");
            }
            if (abstractWork.User == null)
            {
                abstractWork.User = DefaultManager.Instance.DefaultResponsiblePerson;
            }
            if (abstractWork.AbstractStatus == null)
            {
                abstractWork.AbstractStatus = DefaultManager.Instance.DefaultAbstractStatus;
            }
            AbstractStatusLookup = new ObservableCollection<AbstractStatusViewModel>();
            foreach (var abstractStatus in DataManager.Instance.GetAllAbstractStatuses())
            {
                AbstractStatusLookup.Add(new AbstractStatusViewModel(abstractStatus));
            }
            AbstractStatusLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.AbstractStatus))
                {
                    this.abstractStatus = new AbstractStatusViewModel(DefaultManager.Instance.DefaultAbstractStatus);
                }
            };
            ResponsiblePersonsLookup = new ObservableCollection<UserViewModel>();
            foreach (var user in DataManager.Instance.GetAllUsers())
            {
                ResponsiblePersonsLookup.Add(new UserViewModel(user));
            }
            ResponsiblePersonsLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.ResponsiblePerson))
                {
                    this.responsiblePerson = new UserViewModel(DefaultManager.Instance.DefaultResponsiblePerson);
                }
            };
            this.Model = abstractWork;
            this.SendEmailCommand = new DelegateCommand(o => this.SendEmail());
        }

        public ObservableCollection<UserViewModel> ResponsiblePersonsLookup { get; private set; }
        public ObservableCollection<AbstractStatusViewModel> AbstractStatusLookup { get; private set; }

        public ICommand SendEmailCommand { get; private set; }

        public bool IsSentByEmail
        {
            get { return Model.IsSentByEmail; }
            set
            {
                Model.IsSentByEmail = value;
                OnPropertyChanged("IsSentByEmail");
            }
        }

        public DateTime DateWork
        {
            get { return Model.DateWork; }
            set
            {
                Model.DateWork = value;
                OnPropertyChanged("DateWork");
            }
        }

        public UserViewModel ResponsiblePerson
        {
            get
            {
                if (this.Model.User == null)
                {
                    this.Model.User = DefaultManager.Instance.DefaultResponsiblePerson;
                }
                this.responsiblePerson =
                    this.ResponsiblePersonsLookup.SingleOrDefault(s => s.Model == this.Model.User);
                return this.responsiblePerson;
            }
            set
            {
                this.responsiblePerson = value;
                this.Model.User = (value == null) ? null : value.Model;
                OnPropertyChanged("AbstractResponsiblePerson");
            }
        }

        public AbstractStatusViewModel AbstractStatus
        {
            get
            {
                if (this.Model.AbstractStatus == null)
                {
                    this.Model.AbstractStatus = DefaultManager.Instance.DefaultAbstractStatus;
                }
                this.abstractStatus =
                    this.AbstractStatusLookup.SingleOrDefault(s => s.Model == this.Model.AbstractStatus);
                return this.abstractStatus;
            }
            set
            {
                this.abstractStatus = value;
                this.Model.AbstractStatus = (value == null) ? null : value.Model;
                OnPropertyChanged("AbstractStatus");

            }
        }

        public void SendEmail()
        {

        }

        public string ReviewerName
        {
            get { return ResponsiblePerson.Name; }
        }

        public string AbstractStatusName
        {
            get { return AbstractStatus.Name; }
        }

        public string IsSentByEmailName
        {
            get
            {
                if (IsSentByEmail)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                }
            }
        }


    }
}
