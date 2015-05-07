using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Common;
using WPFDB.Model;

namespace WPFDB.ViewModel
{
    public class CurrentPersonViewModel : ViewModelBase
    {

        public Person Model { get; private set; }
        public PersonConference ActualConference { get; private set; }

        public CurrentPersonViewModel(Person person)
        {
            this.Model = person;
            ActualConference = Model.PersonConferences.Where(p => p.ConferenceId == DefaultManager.Instance.DefaultConference.Id).FirstOrDefault();

        }
        public string Rank
        {
            get { return ActualConference == null ? "" : ActualConference.PersonConferences_Detail.Rank.Name; }
        }
        public string IsArrive
        {
            get
            {
                if (ActualConference == null)
                {
                    return "";
                }
                else
                {
                    return ActualConference.PersonConferences_Detail.IsArrive ? "Да" : "Нет";
                }
            }
        }
        public DateTime? DateArrive
        {
            get { return ActualConference == null ? null : ActualConference.PersonConferences_Detail.DateArrive; }
        }

        public string IsBadge
        {
            get
            {
                if (ActualConference == null)
                {
                    return "";
                }
                else
                {
                    return ActualConference.PersonConferences_Detail.IsBadge ? "Да" : "Нет";
                }
            }
        }

        public string ArriveCompany
        {
            get
            {
                return ActualConference == null ? "" : ActualConference.PersonConferences_Detail.Company.Name;
            }
        }

        public string PaymentType
        {
            get { return ActualConference == null ? "" : ActualConference.PersonConferences_Payment.PaymentType.Name; }
        }
        public DateTime? PaymentDate
        {
            get { return ActualConference == null ? null : ActualConference.PersonConferences_Payment.PaymentDate; }
        }
        public string PaymentDocument
        {
            get
            {

                if (ActualConference == null)
                {
                    return "";

                }
                else
                {
                    var str = ActualConference.PersonConferences_Payment.PaymentDocument;

                    if (ActualConference.PersonConferences_Payment.OrderNumber > 0)
                    {
                        str = str + string.Format(" (Ордер № {0})", ActualConference.PersonConferences_Payment.OrderNumber);
                    }
                    return str;
                }
            }
        }

        public string PaymentCompany
        {
            get { return ActualConference == null ? "" : ActualConference.PersonConferences_Payment.Company.Name; }
        }

        public string Money
        {
            get { return ActualConference == null ? "" : ActualConference.PersonConferences_Payment.Money.ToString(); }
        }

        public ScienceDegree ScienceDegree
        {
            get
            {
                if (this.Model.ScienceDegree == null)
                {
                    this.Model.ScienceDegree = DefaultManager.Instance.DefaultScienceDegree;

                }
                return this.Model.ScienceDegree;
            }

            set
            {
                this.Model.ScienceDegree = value;
                this.OnPropertyChanged("ScienceDegree");
            }
        }

        public ScienceStatus ScienceStatus
        {
            get
            {
                if (this.Model.ScienceStatus == null)
                {
                    this.Model.ScienceStatus = DefaultManager.Instance.DefaultScienceStatus;
                }
                return this.Model.ScienceStatus;
            }

            set
            {
                this.Model.ScienceStatus = value;
                this.OnPropertyChanged("ScienceStatus");
            }
        }

        public Sex Sex
        {
            get
            {
                if (this.Model.Sex == null)
                {
                    this.Model.Sex = DefaultManager.Instance.DefaultSex;

                }
                return this.Model.Sex;
            }

            set
            {
                this.Model.Sex = value;
                this.OnPropertyChanged("Sex");
            }
        }
        public Speciality Speciality
        {
            get
            {
                if (this.Model.Speciality == null)
                {
                    this.Model.Speciality = DefaultManager.Instance.DefaultSpeciality;
                }
                return this.Model.Speciality;
            }

            set
            {
                this.Model.Speciality = value;
                this.OnPropertyChanged("Speciality");
            }
        }


        public DateTime? BirthDate
        {
            get
            {
                return this.Model.BirthDate;
            }

            set
            {
                this.Model.BirthDate = value;
                this.OnPropertyChanged("BirthDate");
            }
        }

        public string IsMemberName
        {
            get
            {
                if (this.Model.Iacmac.IsMember)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                };
            }
        }

        public string IsCardCreateName
        {
            get
            {
                if (this.Model.Iacmac.IsCardCreate)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                };
            }
        }
        public string FullName
        {
            get { return this.Model.FullName; }
        }
        public string FullNameInitials
        {
            get { return this.Model.FullNameInitials; }
        }

        public string WorkplacePostName
        {
            get { return this.Model.WorkPlace + " (" + this.Model.Post + ")"; }
        }

        public string IsCardSentName
        {
            get
            {
                if (this.Model.Iacmac.IsCardSent)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                };
            }


        }

        public string IsFormName
        {
            get
            {
                if (this.Model.Iacmac.IsForm)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                };
            }
        }
        public DateTime? DateRegistration
        {
            get
            {
                return this.Model.Iacmac.DateRegistration;
            }

            set
            {
                this.Model.Iacmac.DateRegistration = value;
                this.OnPropertyChanged("DateRegistration");
            }
        }
        public int Number
        {
            get
            {
                return this.Model.Iacmac.Number;
            }

            set
            {
                this.Model.Iacmac.Number = value;
                this.OnPropertyChanged("Number");
            }
        }
        public string Code
        {
            get
            {
                return this.Model.Iacmac.Code;
            }

            set
            {
                this.Model.Iacmac.Code = value;
                this.OnPropertyChanged("Code");
            }
        }


        public List<string> PhonesList
        {
            get
            {
                return Model.Phones.Select(phone => phone.ContactType.Name + ": " + phone.Number).ToList();
            }
        }

        public List<string> EmailsList
        {
            get
            {
                return Model.Emails.Select(email => email.Name).ToList();
            }
        }

        public List<string> AddressesList
        {
            get
            {
                return Model.Addresses.Select(address => address.ContactType.Name + ": " + address.CountryName + ", " + address.RegionName + "," + address.ZipCode + ", " + address.CityName + ", " + address.StreetHouseName).ToList();
            }
        }

        public List<string> ConferencesRegisteredList
        {
            get
            {
                var conferences = DataManager.Instance.GetConferenceForPersonRegistered(Model.Id);
                return conferences.Select(conference => conference.Conference.Name + " (" + conference.PersonConferences_Detail.Rank.Name + ")").ToList();
            }
        }

        public List<string> ConferencesArrivedList
        {
            get
            {
                var conferences = DataManager.Instance.GetConferenceForPersonArrived(Model.Id);
                return conferences.Select(conference => conference.Conference.Name + " (" + conference.PersonConferences_Detail.Rank.Name + ")").ToList();
            }
        }


    }
}
