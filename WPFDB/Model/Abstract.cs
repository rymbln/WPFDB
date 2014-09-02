using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Common;

namespace WPFDB.Model
{
    public partial class Abstract : EntityObject
    {
        public string OtherAuthorsShort
        {
            get
            {
                if (OtherAuthors.Length > 30)
                {
                    return OtherAuthors.Substring(0, 30)+"...";
                }
                else
                {
                    return OtherAuthors;
                }
            }
            set { }
        }

        public string NameShort
        {
            get
            {
                if (Name.Length > 50)
                {
                    return this.Name.Substring(0, 50)+"...";
                }
                else
                {
                    return this.Name;
                }
            }
            set { }
        }

        public string AuthorNameShort
        {
            get
            {
                var person = DataManager.Instance.GetPersonByPersonConferenceId(this.PersonConferenceId);
                return person.FirstName + " " + person.SecondName.Substring(0, 1) + "." + person.ThirdName.Substring(0, 1) + ".";
            }
            set { }
        }

        public string AuthorEmail
        {
            get
            {
                var person = DataManager.Instance.GetPersonByPersonConferenceId(this.PersonConferenceId);
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

        public string ReviewerName
        {
            //todo Добавить отображение рецензента
            get { return ""; }
            set { }
        }

        public string LastState
        {
            // todo Добавить отображение последнего состояния
            get { return ""; }
            set { }
        }

        public DateTime? LastStateDate
        {
            //todo Добавить отображение даты последнего состояния
            get { return null; }
            set { }
        }
    }
}
