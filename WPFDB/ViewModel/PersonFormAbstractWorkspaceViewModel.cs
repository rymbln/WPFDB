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
    public class PersonFormAbstractWorkspaceViewModel: ViewModelBase
    {
        private AbstractDetailsViewModel currentAbstract;
        private PersonViewModel currentPerson;
        private DataManager dm = DataManager.Instance;

        public Abstract Model { get; private set; }

        public ObservableCollection<AbstractDetailsViewModel> AllAbstracts { get; private set; }

        public ICommand AddPersonAbstractCommand { get; private set; }
        public ICommand RemovePersonAbstractCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public PersonFormAbstractWorkspaceViewModel(PersonViewModel person)
        {
            AllAbstracts = new ObservableCollection<AbstractDetailsViewModel>();
            foreach (var a in dm.GetAbstractsForPerson(person.Model))
            {
                AllAbstracts.Add(new AbstractDetailsViewModel(a,person.Model));
            }
            this.currentPerson = person;
            this.AddPersonAbstractCommand = new DelegateCommand((o) => this.AddPersonAbstract());
            this.RemovePersonAbstractCommand = new DelegateCommand((o) => this.RemoveCurrentPersonAbstract(), (o) => this.currentAbstract != null);
            this.SaveCommand = new DelegateCommand((o) => this.Save());
        }

        public AbstractDetailsViewModel CurrentAbstract
        {
            get
            {
                //this.currentAbstract =
                //    this.AllAbstracts.FirstOrDefault(o => o.Id == this.CurrentAbstract.Model.Id.ToString());
                return this.currentAbstract;
            }
            set
            {
                this.currentAbstract = value;
                this.OnPropertyChanged("CurrentAbstract");
            }
        }

        public void AddPersonAbstract()
        {
            var abs = DefaultManager.Instance.DefaulAbstract;
            dm.AddAbstractToPerson(this.currentPerson.Model,abs);
            var vm = new AbstractDetailsViewModel(abs, currentPerson.Model);
            AllAbstracts.Add(vm);
            this.currentAbstract = vm;
        }

        public void RemoveCurrentPersonAbstract()
        {
            AllAbstracts.Remove(this.CurrentAbstract);
            dm.RemoveObject(CurrentAbstract.Model);
            this.currentAbstract = AllAbstracts.Count > 0 ? AllAbstracts[0] : null;
        }

        private void Save()
        {
            this.dm.Save();
        }
    }
}
