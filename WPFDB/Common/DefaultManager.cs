using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Common
{
    public class DefaultManager
    {
        private static DefaultManager instance;
        //  private Conference defaultConference;

        public static DefaultManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DefaultManager();
                }
                return instance;
            }
        }

        public Conference DefaultConference
        {
            get
            {
                var list = DataManager.Instance.GetAllConferences();
                var obj = list.FirstOrDefault(o => o.Code == "+");
                if (obj == null)
                {
                    obj = list.FirstOrDefault(o => o.Code == "-");
                }
                return obj;
            }
        }

        public Sex DefaultSex
        {
            get
            {
                var list = DataManager.Instance.GetAllSexes();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public Speciality DefaultSpeciality
        {
            get
            {
                var list = DataManager.Instance.GetAllSpecialities();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public ScienceDegree DefaultScienceDegree
        {
            get
            {
                var list = DataManager.Instance.GetAllScienceDegrees();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public ScienceStatus DefaultScienceStatus
        {
            get
            {
                var list = DataManager.Instance.GetAllScienceStatuses();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public Rank DefaultRank
        {
            get
            {
                var list = DataManager.Instance.GetAllRanks();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public Company DefaultCompany
        {
            get
            {
                var list = DataManager.Instance.GetAllCompanies();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        public OrderStatus DefaultOrderStatus
        {
            get
            {
                var list = DataManager.Instance.GetAllOrderStatuses();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }
        public PaymentType DefaultPaymentType
        {
            get
            {
                var list = DataManager.Instance.GetAllPaymentTypes();
                var obj = list.FirstOrDefault(o => o.Code == "-");
                return obj;
            }
        }

        private PersonConferences_Detail DefaultPersonConferenceDetail(Guid personConferenceId)
        {
            
            if (personConferenceId == null)
            {
                throw new ArgumentNullException("personConferenceId");
            }
            var pcd = DataManager.Instance.CreateObject<PersonConferences_Detail>();
            pcd.PersonConferenceId = personConferenceId;
            pcd.IsAutoreg = false;
            pcd.IsAbstract = false;
            pcd.IsArrive = false;
            pcd.IsBadge = false;
            pcd.IsNeedBadge = true;
            pcd.RankId = DefaultRank.Id;
            pcd.CompanyId = DefaultCompany.Id;
            return pcd;
        }

        private PersonConferences_Payment DefaultPersonConferencePayment(Guid personConferenceId)
        {
            if (personConferenceId == null)
            {
                throw new ArgumentNullException("personConferenceId");
            }
            var pcp = DataManager.Instance.CreateObject<PersonConferences_Payment>();
            pcp.PersonConferenceId = personConferenceId;
            pcp.Company = DefaultCompany;
            pcp.IsComplect = false;
            pcp.Money = 0;
            pcp.OrderNumber = 0;
            pcp.OrderStatus = DefaultOrderStatus;
            pcp.PaymentType = DefaultPaymentType;
            pcp.PaymentDate = DateTime.Now;
            pcp.PaymentDocument = "---";
            return pcp;
        }

        private Iacmac DefaultIacmac(Guid personId)
        {
            if (personId == null)
            {
                throw new ArgumentNullException("personId");
            }
            var iacmac = DataManager.Instance.CreateObject<Iacmac>();
            iacmac.PersonId = personId;
            iacmac.Code = "-";
            iacmac.IsMember = false;
            iacmac.IsCardCreate = false;
            iacmac.IsCardSent = false;
            iacmac.IsForm = false;
            return iacmac;
        }

      


        public Person DefaultPerson
        {
            get
            {
                var p = DataManager.Instance.CreateObject<Person>();
                p.Id = GuidComb.Generate();
                p.BirthDate = Convert.ToDateTime("01/01/1900");
                p.FirstName = "---";
                p.SecondName = "---";
                p.ThirdName = "---";
                p.SexId = DefaultSex.Id;
                p.SpecialityId = DefaultSpeciality.Id;
                p.ScienceDegreeId = DefaultScienceDegree.Id;
                p.ScienceStatusId = DefaultScienceStatus.Id;
                p.Iacmac = DefaultIacmac(p.Id);
                DataManager.Instance.AddPerson(p);
                DataManager.Instance.Save();

                var pc = DataManager.Instance.CreateObject<PersonConference>();
                pc.PersonConferenceId = GuidComb.Generate();
                pc.PersonId = p.Id;
                pc.ConferenceId = DefaultConference.Id;
                
                pc.PersonConferences_Payment = DefaultPersonConferencePayment(pc.PersonConferenceId);
                pc.PersonConferences_Detail = DefaultPersonConferenceDetail(pc.PersonConferenceId);

                DataManager.Instance.AddPersonConference(pc);
                DataManager.Instance.Save();
                return p;
            }
        }

    }
}
