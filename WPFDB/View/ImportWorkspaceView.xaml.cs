using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFDB.Common;

namespace WPFDB.View
{
    /// <summary>
    /// Interaction logic for ImportWorkspaceView.xaml
    /// </summary>
    public partial class ImportWorkspaceView : UserControl
    {
        private BackgroundWorker backgroundWorker;

        public ImportWorkspaceView()
        {
            InitializeComponent();
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
          //  var res = ImportManager.Instance.ImportAll(backgroundWorker);
            int res;
 //res = ImportManager.Instance.ImportContactTypes();
            //MessageBox.Show(res + " ContactTypes");
            ImportManager.Instance.ImportSexes();
            res = ImportManager.Instance.ImportSpecialities();
            MessageBox.Show(res + "Specialities");
            res = ImportManager.Instance.ImportRanks();
            MessageBox.Show(res + "Ranks");
            res = ImportManager.Instance.ImportScienceDegrees();
            MessageBox.Show(res + " ScienceDegree");
            res = ImportManager.Instance.ImportScienceStatuses();
            MessageBox.Show(res + "ScienceStatuses");


            res = ImportManager.Instance.ImportPaymentTypes();
            MessageBox.Show(res + " PaymentTypes");

            res = ImportManager.Instance.ImportConferenceTypes();
            MessageBox.Show(res + " ConferenceTypes");

            res = ImportManager.Instance.ImportCompanies();
            MessageBox.Show(res + " Companies");

            res = ImportManager.Instance.ImportAbstractStatuses();
            MessageBox.Show(res + " AbstractStatuses");

            res = ImportManager.Instance.ImportUsers();
            MessageBox.Show(res + " Users");

             res = ImportManager.Instance.ImportPersons();
            MessageBox.Show(res + " persons imported ");

             res = ImportManager.Instance.ImportPersonConferences();
            MessageBox.Show(res + " PersonConference");

             res = ImportManager.Instance.ImportPersonConferenceDetail();
            MessageBox.Show(res + " PersonConferenceDetail");

             res = ImportManager.Instance.ImportPersonConferenceMoney();
            MessageBox.Show(res + " PersonConferenceMoney");

             res = ImportManager.Instance.ImportAbstracts();
            MessageBox.Show(res + " Abstracts");

             res = ImportManager.Instance.ImportAbstractsWorks();
            MessageBox.Show(res + " Abstract Works");
             res = ImportManager.Instance.ImportAdresses();
            MessageBox.Show(res + " Addresses");
            res = ImportManager.Instance.ImportEmails();
            MessageBox.Show(res + " Emails");
         res = ImportManager.Instance.ImportPhones();
            MessageBox.Show(res + " Phones");

            string result = "Finish";
            e.Result = result;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("FUCK!");
            }
            else
            {
                MessageBox.Show("Import Finished - " + e.Result);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        private void btnImportPersons_Click(object sender, RoutedEventArgs e)
        {
            var res = ImportManager.Instance.ImportPersons();
            MessageBox.Show(res + " persons imported ");
        }

        private void btnImportKnowalls_Click(object sender, RoutedEventArgs e)
        {
            int res;

            //res = ImportManager.Instance.ImportContactTypes();
            //MessageBox.Show(res + " ContactTypes");

            res = ImportManager.Instance.ImportSpecialities();
            MessageBox.Show(res + "Specialities");
            res = ImportManager.Instance.ImportRanks();
            MessageBox.Show(res + "Ranks");
            res = ImportManager.Instance.ImportScienceDegrees();
            MessageBox.Show(res + " ScienceDegree");
            res = ImportManager.Instance.ImportScienceStatuses();
            MessageBox.Show(res + "ScienceStatuses");


            res = ImportManager.Instance.ImportPaymentTypes();
            MessageBox.Show(res + " PaymentTypes");

            res = ImportManager.Instance.ImportConferenceTypes();
            MessageBox.Show(res + " ConferenceTypes");

            res = ImportManager.Instance.ImportCompanies();
            MessageBox.Show(res + " Companies");

            res = ImportManager.Instance.ImportAbstractStatuses();
            MessageBox.Show(res + " AbstractStatuses");

            res = ImportManager.Instance.ImportUsers();
            MessageBox.Show(res + " Users");

            ImportManager.Instance.ImportSexes();
        }

        private void btnIMportPersonConferences_Click(object sender, RoutedEventArgs e)
        {
            var res = ImportManager.Instance.ImportPersonConferences();
            MessageBox.Show(res + " PersonConference");
        }

        private void btnImportPersonConferenceDetail_Click(object sender, RoutedEventArgs e)
        {
            var res = ImportManager.Instance.ImportPersonConferenceDetail();
            MessageBox.Show(res + " PersonConferenceDetail");
        }

        private void btnImportPErsonConferenceMoney_Click(object sender, RoutedEventArgs e)
        {
            var res = ImportManager.Instance.ImportPersonConferenceMoney();
            MessageBox.Show(res + " PersonConferenceMoney");
        }

        private void btnImportAbstract_Click(object sender, RoutedEventArgs e)
        {
            var res = ImportManager.Instance.ImportAbstracts();
            MessageBox.Show(res + " Abstracts");
        }

        private void btnImportAbstractWorks_Click(object sender, RoutedEventArgs e)
        {
            var res = ImportManager.Instance.ImportAbstractsWorks();
            MessageBox.Show(res + " Abstract Works");
        }

        private void btnImportAddresses_Click(object sender, RoutedEventArgs e)
        {
            var res = ImportManager.Instance.ImportAdresses();
            MessageBox.Show(res + " Addresses");
        }

        private void btnImportEmails_Click(object sender, RoutedEventArgs e)
        {
            var res = ImportManager.Instance.ImportEmails();
            MessageBox.Show(res + " Emails");
        }

        private void btnImportPhones_Click(object sender, RoutedEventArgs e)
        {
            var res = ImportManager.Instance.ImportPhones();
            MessageBox.Show(res + " Phones");
        }
    }
}
