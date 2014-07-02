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
    public class UserWorkspaceViewModel : ViewModelBase
    {
        private UserViewModel currentUser;
        private DataManager dm = DataManager.Instance;

        public UserWorkspaceViewModel()
        {
            AllUsers = new ObservableCollection<UserViewModel>();
            foreach (var item in dm.GetAllUsers())
            {
                AllUsers.Add(new UserViewModel(item));
            }
            this.CurrentUser = this.AllUsers.Count > 0 ? this.AllUsers[0] : null;
            this.AllUsers.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentUser))
                {
                    this.CurrentUser = null;
                }
            };

            this.AddUserCommand = new DelegateCommand((o) => this.AddUser());
            this.DeleteUserCommand = new DelegateCommand((o) => this.DeleteCurrentUser(), (o) => this.CurrentUser != null);


        }

        public ICommand SaveCommand { get; private set; }

        public ObservableCollection<UserViewModel> AllUsers { get; private set; }

        public UserViewModel CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; OnPropertyChanged("CurrentUser"); }
        }

        public ICommand AddUserCommand { get; private set; }
        public ICommand DeleteUserCommand { get; private set; }

        public void AddUser()
        {
            User c = this.dm.CreateObject<User>();
            c.Id = GuidComb.Generate();
            dm.AddUser(c);

            UserViewModel vm = new UserViewModel(c);
            AllUsers.Add(vm);
            CurrentUser = vm;
        }

        private void DeleteCurrentUser()
        {
            dm.RemoveUser(CurrentUser.Model);
            AllUsers.Remove(CurrentUser);
            CurrentUser = null;
        }
    }
}
