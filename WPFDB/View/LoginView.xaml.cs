using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.ViewModel;

namespace WPFDB.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private Authentification auth = Authentification.Instance;

        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if ((txtName.Text != "") & (txtPassword.Text != ""))
            //{
            //    var user = new User();
            //    user.Name = txtName.Text;
            //    user.Password = txtPassword.Text;
            //    if (auth.AuthentificateUser(user))

            //    {
                    MainViewModel main = new MainViewModel();
                    MainView window = new View.MainView { DataContext = main };
                    window.Show();
                    this.Close();
            //    }

            //}
        }
    }
}
