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
            var res = ImportManager.Instance.ImportAll(backgroundWorker);

            e.Result = res;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("FUCK!");
            }
            else
            {
                MessageBox.Show("Import Finished");
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
    }
}
