using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        private UserViewModel reviewer;
        private AbstractStatusViewModel abstractStatus;

        public AbstractWorkViewModel(AbstractWork abstractWork)
        {
            if (abstractWork == null)
            {
                throw new ArgumentNullException("abstractWork");
            }
            if (abstractWork.Reviewer == null)
            {
                abstractWork.Reviewer = DefaultManager.Instance.DefaultResponsiblePerson;
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
            ReviewersLookup = new ObservableCollection<UserViewModel>();
            foreach (var user in DataManager.Instance.GetAllUsers())
            {
                ReviewersLookup.Add(new UserViewModel(user));
            }
            ReviewersLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.Reviewer))
                {
                    this.reviewer = new UserViewModel(DefaultManager.Instance.DefaultResponsiblePerson);
                }
            };
            this.Model = abstractWork;
            this.SendEmailCommand = new DelegateCommand(o => this.SendEmail());
        }

        public ObservableCollection<UserViewModel> ReviewersLookup { get; private set; }
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

        public UserViewModel Reviewer
        {
            get
            {
                if (this.Model.Reviewer == null)
                {
                    this.Model.Reviewer = DefaultManager.Instance.DefaultResponsiblePerson;
                }
                this.reviewer =
                    this.ReviewersLookup.SingleOrDefault(s => s.Model == this.Model.Reviewer);
                return this.reviewer;
            }
            set
            {
                this.reviewer = value;
                this.Model.Reviewer = (value == null) ? null : value.Model;
                OnPropertyChanged("Reviewer");
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
         
            var lstEmail = new List<string>();
            lstEmail.Add("ivan.trushin@antibiotic.ru");
            EmailManager.Instance.SendMailForAbstract(lstEmail, "Test Email From WPFDB Conference",
                "This email is sent for testing email manager module", @"D:\ELI_Validation_20140612.xls");
        }

        public string ReviewerName
        {
            get { return Reviewer.Name; }
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
