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
    public class PrintersViewModel : ViewModelBase
    {
        public PrintersViewModel()
        {
            Load();
            AddCommand = new DelegateCommand(o => Add());
            DeleteCommand = new DelegateCommand(o => Delete(), o => PrinterSelected != null);
            SaveCommand = new DelegateCommand(o => Save());

        }

        private void Load()
        {
            PrinterCollection = new ObservableCollection<Printer>(DataManager.Instance.GetPrinters());
            CurrentComputer = DefaultManager.Instance.ComputerName;
            PrinterLookup = new ObservableCollection<string>(DefaultManager.Instance.GetPrinterNames());
            DocumentLookup = new ObservableCollection<string>(DefaultManager.Instance.GetDocumentNames());

        }
        public string CurrentDocumentName { get; set; }
        public string CurrentPrinterName { get; set; }
        public ObservableCollection<string> PrinterLookup { get; set; }
        public ObservableCollection<string> DocumentLookup { get; set; }
        public string CurrentComputer { get; set; }
        public ObservableCollection<Printer> PrinterCollection { get; set; }
        public Printer PrinterSelected { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }

        private void Save()
        {
            try
            {
                DataManager.Instance.Save();
            }
            catch (Exception ex)
            {
                LogManager.Write(ex);
            }
        }
        private void Add()
        {
            try
            {
                var obj = DataManager.Instance.CreateObject<Printer>();
                obj.Id = GuidComb.Generate();
                obj.PrinterName = CurrentPrinterName;
                obj.ComputerName = CurrentComputer;
                obj.DocumentName = CurrentDocumentName;

                PrinterCollection.Add(DataManager.Instance.AddPrinter(obj));
            }
            catch (Exception ex)
            {
                LogManager.Write(ex);
            }
        }
        private void Delete()
        {
            try
            {
                DataManager.Instance.RemovePrinter(PrinterSelected);
                PrinterCollection.Remove(PrinterSelected);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex);
            }
        }


    }
}
