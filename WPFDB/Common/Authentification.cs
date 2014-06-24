using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFDB.Model;

namespace WPFDB.Common
{
    public class Authentification
    {
        private static Authentification instance;
        private bool authentificated;
        private User currentUser;
        private DataManager dm = DataManager.Instance;

        public static Authentification Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Authentification();
                }
                return instance;
            }
        }

        private Authentification()
        {
            authentificated = false;
        }

        public bool Authentificated()
        {
            return authentificated;
        }

        public User GetCurrentUser()
        {
            return currentUser;
        }

        public bool AuthentificateUser(User user)
        {
            var cu = dm.GetUser(user.Name, user.Password);
            if (cu != null)
            {
                this.currentUser = cu;
                this.authentificated = true;
                return true;
            }
            else
            {
                this.currentUser = null;
                this.authentificated = false;
                return false;
            }
        }

      

    }
}
