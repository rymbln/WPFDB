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
    public class ContactTypeWorkspaceViewModel : ViewModelBase
    {
        private ContactTypeViewModel currentContactType;
        private DataManager dm = DataManager.Instance;

        public ContactTypeWorkspaceViewModel()
        {
            AllContactTypes = new ObservableCollection<ContactTypeViewModel>();
            foreach (var item in dm.GetAllContactTypes())
            {
                AllContactTypes.Add(new ContactTypeViewModel(item));
            }
            this.CurrentContactType = this.AllContactTypes.Count > 0 ? this.AllContactTypes[0] : null;

            this.AllContactTypes.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains((this.CurrentContactType)))
                {
                    this.CurrentContactType = null;
                }
            };
            this.AddContactTypeCommand = new DelegateCommand((o) => this.AddContactType());
            this.DeleteContactTypeCommand = new DelegateCommand((o) => this.DeleteCurrentContactType(), (o) => this.CurrentContactType != null);
        }

        public ObservableCollection<ContactTypeViewModel> AllContactTypes { get; private set; }

        public ContactTypeViewModel CurrentContactType
        {
            get { return this.currentContactType; }
            set { this.currentContactType = value; this.OnPropertyChanged("CurrentContactType"); }
        }

        public ICommand AddContactTypeCommand { get; private set; }
        public ICommand DeleteContactTypeCommand { get; private set; }

        private void AddContactType()
        {
            ContactType ct = this.dm.CreateObject<ContactType>();
            ct.Id = GuidComb.Generate();
            this.dm.AddContactType(ct);

            ContactTypeViewModel vm = new ContactTypeViewModel(ct);
            this.AllContactTypes.Add(vm);
            this.CurrentContactType = vm;

        }

        private void DeleteCurrentContactType()
        {
            this.dm.RemoveContactType(this.CurrentContactType.Model);
        }
    }
}
