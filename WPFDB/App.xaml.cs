﻿

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
        /// <summary>
        /// The unit of work co-ordinating changes for the application
        /// </summary>
        private IConferenceContext context;

        /// <summary>
        /// Lauches the entry form on startup
        /// </summary>
        /// <param name="e">Arguments of the startup event</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

   

            this.context = new ConferenceEntities();

            ISpecialityRepository specialityRepository = new SpecialityRepository(context);
            ISexRepository sexRepository = new SexRepository(context);
            IScienceDegreeRepository  scienceDegreeRepository = new ScienceDegreeRepository(context);
            IScienceStatusRepository scienceStatusRepository = new ScienceStatusRepository(context);
            IConferenceRepository conferenceRepository = new ConferenceRepository(context);
            IPersonRepository personRepository = new PersonRepository(context);
            IUnitOfWork unit = new UnitOfWork(context);

            MainViewModel main = new MainViewModel(unit, specialityRepository, sexRepository, scienceDegreeRepository, scienceStatusRepository , conferenceRepository, personRepository);
            MainView window = new View.MainView {DataContext = main};
            window.Show();


        }

        /// <summary>
        /// Cleans up any resources on exit
        /// </summary>
        /// <param name="e">Arguments of the exit event</param>
        protected override void OnExit(ExitEventArgs e)
        {
            this.context.Dispose();

            base.OnExit(e);
        }
    }
}
