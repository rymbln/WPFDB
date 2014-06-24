using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            CurrentUser = new User { Name = "Name", Password = "Password" };
        }

        private bool _IsAuthenticated;
        public bool IsAuthenticated
        {
            get { return _IsAuthenticated; }
            set
            {
                if (value != _IsAuthenticated)
                {
                    _IsAuthenticated = value;
                    OnPropertyChanged("IsAuthenticated");
                    OnPropertyChanged("IsNotAuthenticated");
                }
            }
        }

        public bool IsNotAuthenticated
        {
            get
            {
                return !IsAuthenticated;
            }
        }

        public bool CanDoAuthenticated(object ignore)
        {
            return IsAuthenticated;
        }

        private User _CurrentUser;
        public User CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                if (value != _CurrentUser)
                {
                    _CurrentUser = value;
                    OnPropertyChanged("CurrentUser");
                }
            }
        }

        public void Authenticate()
        {
            IsAuthenticated = true;
        }

        public void LogOff()
        {
            IsAuthenticated = false;
        }
    }
}
