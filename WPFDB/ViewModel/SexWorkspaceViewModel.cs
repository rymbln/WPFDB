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
    public class SexWorkspaceViewModel : ViewModelBase
    {
        private SexViewModel currentSex;
        private IUnitOfWork unitOfWork;

        public SexWorkspaceViewModel(ObservableCollection<SexViewModel> sexes, IUnitOfWork unitOfWork)
        {
            if (sexes == null)
            {
                throw new ArgumentNullException("sex");
            }
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfwork");
            }
            this.unitOfWork = unitOfWork;
            this.AllSexes = sexes;
            this.CurrentSex = this.AllSexes.Count > 0 ? this.AllSexes[0] : null;
            this.AllSexes.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentSex))
                {
                    this.CurrentSex = null;
                }
            };

            this.AddSexCommand = new DelegateCommand((o) => this.AddSex());
            this.DeleteSexCommand = new DelegateCommand((o) => this.DeleteCurrentSex(), (o) => this.CurrentSex != null );
        }

        public ObservableCollection<SexViewModel> AllSexes { get; private set; }

        public SexViewModel CurrentSex
        {
            get { return this.currentSex; }
            set { this.currentSex = value; this.OnPropertyChanged("CurrentSex"); }
        }

        public ICommand AddSexCommand { get; private set; }
        public ICommand DeleteSexCommand { get; private set; }

        private void AddSex()
        {
            Sex s = this.unitOfWork.CreateObject<Sex>();
            s.Id = GuidComb.Generate();
            this.unitOfWork.AddSex(s);

            SexViewModel vm = new SexViewModel(s);
            this.AllSexes.Add(vm);
            this.CurrentSex = vm;
        }

        private void DeleteCurrentSex()
        {
            this.unitOfWork.RemoveSex(this.CurrentSex.Model);
            this.AllSexes.Remove(this.CurrentSex);
            this.CurrentSex = null;
        }
    }
}
