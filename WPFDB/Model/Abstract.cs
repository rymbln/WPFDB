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
     

        public string ToFilterString
        {
            get
            {
                var str = new StringBuilder();
                str.Append(this.Name);
                str.Append(this.PersonConference.Person.FullNameInitials);
                
                return str.ToString();
            }
        }
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

        public string AuthorName
        {
            get { return this.PersonConference.Person.FullNameInitials; }
          
        }

        public string AuthorEmail
        {
            get
            {
                var emails = this.PersonConference.Person.Emails.ToList();
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

            get
            {
                try
                {
                    return this.AbstractWorks.OrderByDescending(o => o.DateWork).FirstOrDefault().Reviewer.Name;

                }
                catch (Exception)
                {

                    return "---";
                }
            }
         
        }

        public string LastState
        {
            get
            {
                try
                {
                    return this.AbstractWorks.OrderByDescending(o => o.DateWork).FirstOrDefault().AbstractStatus.Name;

                }
                catch (Exception)
                {

                    return "---";
                }
            }
        }

        public DateTime? LastStateDate
        {
            get
            {
                try
                {
                    return this.AbstractWorks.OrderByDescending(o => o.DateWork).FirstOrDefault().DateWork;
                }
                catch (Exception)
                {
                    return null;
                }
              
            }
        }
    }
}
