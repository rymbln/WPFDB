using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Common;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class AbstractViewModel: ViewModelBase
    {
        public Abstract Model { get; private set; }
         public AbstractViewModel(Abstract obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            this.Model = obj;
        }

         public string OtherAuthors
         {
             get { return this.Model.OtherAuthors; }
             set
             {
                 this.Model.OtherAuthors = value;
                 this.OnPropertyChanged("OtherAuthors");
             }
         }

         public string OtherAuthorsShort
         {
             get
             {
                 if (Model.OtherAuthors.Length > 30)
                 {
                     return Model.OtherAuthors.Substring(1, 30);
                 }
                 else
                 {
                     return Model.OtherAuthors;
                 }
             }
             set { }
         }

         public string Name
         {
             get { return this.Model.Name; }
             set
             {
                 this.Model.Name = value;
                 OnPropertyChanged("Name");
             }
         }

         public string NameShort
         {
             get
             {
                 if (Model.Name.Length > 50)
                 {
                     return this.Model.Name.Substring(1, 50);
                 }
                 else
                 {
                     return this.Model.Name;
                 }
             }
             set { }
         }

         public string Text
         {
             get { return this.Model.Text; }
             set
             {
                 this.Model.Text = value;
                 OnPropertyChanged("Text");
             }
         }
         public string Link
         {
             get { return this.Model.Link; }
             set
             {
                 this.Model.Link = value;
                 OnPropertyChanged("Link");
             }
         }

      

         public string Id
         {
             get { return this.Model.Id.ToString(); }
             set { }
         }

         public string AuthorName
         {
             get
             {
                 var person = DataManager.Instance.GetPersonByPersonConferenceId(this.Model.PersonConferenceId);
                 return person.FirstName + " " + person.SecondName + " " + person.ThirdName;
             }
             set { }
         }

         public string AuthorNameShort
         {
             get
             {
                 var person = DataManager.Instance.GetPersonByPersonConferenceId(this.Model.PersonConferenceId);
                 return person.FirstName + " " + person.SecondName.Substring(0,1) +"." + person.ThirdName.Substring(0,1)+ ".";
             }
             set { }
         }

         public string AuthorEmail
         {
             get
             {
                 var person = DataManager.Instance.GetPersonByPersonConferenceId(this.Model.PersonConferenceId);
                 var emails = DataManager.Instance.GetEmailsByPersonId(person.Id);
                 string email_string = "";
                 if (emails.Count() > 0)
                 {
                     foreach (var email in emails)
                     {
                         if (email_string != "")
                         {
                             email_string = email_string + "; ";
                         }
                         email_string = email_string + email.Name;
                     }
                 }
                 else
                 {
                     email_string = "---";
                 }
                 return email_string;
             }
             set { }
         }

        public List<AbstractWork> Works
        {
            get { return Model.AbstractWorks.OrderByDescending(o => o.DateWork).ToList(); }
        }
    }
}
