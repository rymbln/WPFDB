using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WPFDB.Common;
using WPFDB.Model;
using WPFDB.ViewModel.Helpers;

namespace WPFDB.ViewModel
{
   public class SettingsViewModel : ViewModelBase
    {
       public SettingsViewModel()
       {
           ConferenceLookup = new ObservableCollection<Conference>(DataManager.Instance.GetAllConferences());
           SelectFolderCommand = new DelegateCommand(o => SelectFolder());
       }

       public ObservableCollection<Conference> ConferenceLookup { get; private set; }

       public Conference ACTIVE_CONFERENCE
       {
           get { return DefaultManager.Instance.DefaultConference; }
           set
           {
               DefaultManager.Instance.DefaultConference = value;
               OnPropertyChanged("ACTIVE_CONFERENCE");
           }
       }
       public bool IsConferenceMode
       {
           get { return DefaultManager.Instance.ConferenceMode; }
           set
           {
               DefaultManager.Instance.ConferenceMode = value;
               OnPropertyChanged("IsConferenceMode");
           }
       }
       public bool IsRegistrationMode
       {
           get { return DefaultManager.Instance.RegistrationMode; }
           set
           {
               DefaultManager.Instance.RegistrationMode = value;
               OnPropertyChanged("IsRegistrationMode");
           }
       }

       public string MailServer
       {
           get { return DefaultManager.Instance.MailServer; }
           set
           {
               DefaultManager.Instance.MailServer = value;
               OnPropertyChanged("MailServer");
           }
       }

       public string MailPort
       {
           get { return DefaultManager.Instance.MailPort; }
           set
           {
               DefaultManager.Instance.MailPort = value;
               OnPropertyChanged("MailPort");
           }
       }
       public string MailLogin
       {
           get { return DefaultManager.Instance.MailLogin; }
           set
           {
               DefaultManager.Instance.MailLogin = value;
               OnPropertyChanged("MailLogin");
           }
       }

       public string MailPassword
       {
           get { return DefaultManager.Instance.MailPassword; }
           set
           {
               DefaultManager.Instance.MailPassword = value;
               OnPropertyChanged("MailPassword");
           }
       }

       public string MailHeaderAbstract
       {
           get { return DefaultManager.Instance.MailHeaderAbstract; }
           set
           {
               DefaultManager.Instance.MailHeaderAbstract = value;
               OnPropertyChanged("MailHeaderAbstract");
           }
       }

       public string MailHeaderPoster
       {
           get { return DefaultManager.Instance.MailHeaderPoster; }
           set
           {
               DefaultManager.Instance.MailHeaderPoster = value;
               OnPropertyChanged("MailHeaderPoster");
           }
       }

       public string MailMessageWork
       {
           get { return DefaultManager.Instance.MailMessageWork; }
           set
           {
               DefaultManager.Instance.MailMessageWork = value;
               OnPropertyChanged("MailMessageWork");
           }
       }
       public string MailMessagePositive
       {
           get { return DefaultManager.Instance.MailMessagePositive; }
           set
           {
               DefaultManager.Instance.MailMessagePositive = value;
               OnPropertyChanged("MailMessagePositive");
           }
       }

       public string MailMessageNegative
       {
           get { return DefaultManager.Instance.MailMessageNegative; }
           set
           {
               DefaultManager.Instance.MailMessageNegative = value;
               OnPropertyChanged("MailMessageNegative");
           }
       }

       public string MailMessageNegativeSecond
       {
           get { return DefaultManager.Instance.MailMessageNegativeSecond; }
           set
           {
               DefaultManager.Instance.MailMessageNegativeSecond = value;
               OnPropertyChanged("MailMessageNegativeSecond");
           }
       }

       public string AbstractFilePath
       {
           get { return DefaultManager.Instance.AbstractFilePath; }
           set
           {
               DefaultManager.Instance.AbstractFilePath = value;
               OnPropertyChanged("AbstractFilePath");
           }
       }

       public string MailMessagePosterSession
       {
           get { return DefaultManager.Instance.MailMessagePosterSession; }
           set
           {
               DefaultManager.Instance.MailMessagePosterSession = value;
               OnPropertyChanged("MailMessagePosterSession");
           }
       }

       public ICommand SelectFolderCommand { get; private set; }

       private void SelectFolder()
       {
           FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

           folderBrowserDialog.SelectedPath = @"C:\Users\rymbln\Desktop\Output\";
           //  openFileDialogCSV.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
           //  openFileDialogCSV.FilterIndex = 1;

           if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
           {
              AbstractFilePath = folderBrowserDialog.SelectedPath;
           }
       }


    }
}
