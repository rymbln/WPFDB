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
        private static bool authentificated;
        private static User currentUser;

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

        public static User GetCurrentUser()
        {
            return currentUser;
        }

        public static bool AuthentificateUser(User user)
        {
            var cu = DataManager.Instance.GetUser(user.Name, user.Password);
            if (cu != null)
            {
                currentUser = cu;
                authentificated = true;
                return true;
            }
            else
            {
                currentUser = null;
                authentificated = true;
                return true;
            }
        }

      

    }
}
