using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization.Advanced;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
    public class ScienceDegreeWorkspaceViewModel : ViewModelBase
    {
        private ScienceDegreeViewModel currentScienceDegree;
        private IUnitOfWork unitOfWork;

        public ScienceDegreeWorkspaceViewModel(ObservableCollection<ScienceDegreeViewModel> scienceDegrees,
            IUnitOfWork unitOfWork)
        {
            if (scienceDegrees == null)
            {
                throw new ArgumentNullException("scienceDegrees");
            }
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            this.unitOfWork = unitOfWork;
            this.AllScienceDegrees = scienceDegrees;
            this.CurrentScienceDegree = this.AllScienceDegrees.Count > 0 ? this.AllScienceDegrees[0] : null;

            this.AllScienceDegrees.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains((this.CurrentScienceDegree)))
                {
                    this.CurrentScienceDegree = null;
                }
            };
            this.AddScienceDegreeCommand = new DelegateCommand((o) => this.AddScienceDegree());
            this.DeleteScienceDegreeCommand = new DelegateCommand((o) => this.DeleteCurrentScienceDegree(), (o) => this.CurrentScienceDegree != null);
        }

        public ObservableCollection<ScienceDegreeViewModel> AllScienceDegrees { get; private set; }

        public ScienceDegreeViewModel CurrentScienceDegree
        {
            get { return this.currentScienceDegree; }
            set { this.currentScienceDegree = value; this.OnPropertyChanged("CurrentScienceDegree"); }
        }

        public ICommand AddScienceDegreeCommand { get; private set; }
        public ICommand DeleteScienceDegreeCommand { get; private set; }

        private void AddScienceDegree()
        {
            ScienceDegree sd = this.unitOfWork.CreateObject<ScienceDegree>();
            sd.Id = GuidComb.Generate();
            this.unitOfWork.AddScienceDegree(sd);

            ScienceDegreeViewModel vm = new ScienceDegreeViewModel(sd);
            this.AllScienceDegrees.Add(vm);
            this.CurrentScienceDegree = vm;

        }

        private void DeleteCurrentScienceDegree()
        {
            this.unitOfWork.RemoveScienceDegree((this.CurrentScienceDegree.Model));
            this.AllScienceDegrees.Remove(this.CurrentScienceDegree);
            this.CurrentScienceDegree = null;
        }
    }
}
