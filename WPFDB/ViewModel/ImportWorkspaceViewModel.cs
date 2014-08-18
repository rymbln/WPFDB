using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class ImportWorkspaceViewModel:ViewModelBase
    {
        private ImportManager im = ImportManager.Instance;
        public ImportWorkspaceViewModel()
        {
          //  ImportAllCommand = new DelegateCommand(o=>this.ImportAll());
        }

        //public ICommand ImportAllCommand { get; set; }

        //private void ImportAll()
        //{
        //    im.ImportAll();
        //}
    }
}
