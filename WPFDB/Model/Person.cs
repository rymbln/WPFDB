using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDB.Model
{
    public partial class Person: EntityObject
    {
        public string FullName
        {
            get { return this.FirstName + " " + SecondName + " " + ThirdName; }
        }
        public string FullNameInitials
        {
            get { return this.FirstName + " " + SecondName.Substring(0, 1) + "." + ThirdName.Substring(0, 1) + "."; }
        }
        public string CityName {
            get
            {
                var lst = this.Addresses.Select(a => new { Name = a.CityName, Date = a.DateUpdate }).ToList();
                if (lst.Count >0)
                {
                    var str =  lst.OrderByDescending(a => a.Date).FirstOrDefault();
                    return str == null ? "" : str.Name; 
                }
                else
                {
                    return null;
                }
            }
        }
        public string CountryName
        {
            get
            {
                var lst = this.Addresses.Select(a => new { Name = a.CountryName, Date = a.DateUpdate }).ToList();
                if (lst.Count > 0)
                {
                    var str = lst.OrderByDescending(a => a.Date).FirstOrDefault();
                    return str == null ? "" : str.Name;
                }
                else
                {
                    return null;
                }
            }
        }
        public string CompanyName { get; set; }
        public string ToString
        {
            get
            {
                var str = new StringBuilder();
                str.Append(this.FirstName);
                str.Append(" ");
                str.Append(this.SecondName);
                str.Append(" ");
                str.Append(this.ThirdName);
                str.Append(";");
                str.Append(this.Sex.Name);
                str.Append(";");
                str.Append(this.BirthDate);
                str.Append(";");
                str.Append(this.Post);
                str.Append(";");
                str.Append(this.WorkPlace);
                str.Append(";");
                str.Append(this.Speciality.Name);
                str.Append(";");
                str.Append(this.ScienceDegree.Name);
                str.Append(";");
                str.Append(this.ScienceStatus.Name);
                str.Append(";");
                str.Append(this.Iacmac.Code);
                return str.ToString();
            }
        }
        public string ToFilterString
        {
            get
            {
                var str = new StringBuilder();
                str.Append(this.FirstName);
                str.Append(this.SecondName);
                str.Append(this.ThirdName);
                str.Append(this.Sex.Name);
                str.Append(this.BirthDate);
                str.Append(this.Post);
                str.Append(this.WorkPlace);
                str.Append(this.Speciality.Name);
                str.Append(this.ScienceDegree.Name);
                str.Append(this.ScienceStatus.Name);
                str.Append(this.Iacmac.Code);
                return str.ToString();
            }
        }
    }
}
