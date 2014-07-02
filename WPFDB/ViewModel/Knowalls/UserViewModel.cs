using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
   public  class UserViewModel:ViewModelBase
    {
       public static int Errors { get; set; }

       public User Model { get; private set; }

       public UserViewModel(User user)
       {
           if (user == null)
           {
               throw new ArgumentNullException("conference");
           }
           this.Model = user;
       }

       public string Id
       {
           get { return this.Model.Id.ToString(); }
           set { }
       }

       public string Name
       {
           get { return this.Model.Name; }
           set { this.Model.Name = value; this.OnPropertyChanged("Name"); }
       }

       public string Password
       {
           get { return this.Model.Password; }
           set { this.Model.Password = value; this.OnPropertyChanged("Password"); }
       }

       public string Role
       {
           get { return this.Model.Role; }
           set { this.Model.Role = value; this.OnPropertyChanged("Role"); }
       }
    }
}
