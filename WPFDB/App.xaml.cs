

using WPFDB.View;

namespace WPFDB
{

    using System.Windows;
    using WPFDB.Common;
    using WPFDB.Data;
    using WPFDB.Model;
    using WPFDB.ViewModel;
    using WPFDB.Properties;
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Authentification auth = Authentification.Instance;
        /// <summary>
        /// Lauches the entry form on startup
        /// </summary>
        /// <param name="e">Arguments of the startup event</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var currentUser = auth.GetCurrentUser();
            if (auth.Authentificated())
            {
                MainViewModel main = new MainViewModel();
                MainView window = new View.MainView {DataContext = main};
                window.Show();
            }
            else
            {
                LoginViewModel authVm = new LoginViewModel();
                LoginView authV = new LoginView();
                authV.Show();
            }
        }

        /// <summary>
        /// Cleans up any resources on exit
        /// </summary>
        /// <param name="e">Arguments of the exit event</param>
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
