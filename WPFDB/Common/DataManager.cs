using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Annotations;
using WPFDB.Data;
using WPFDB.Model;

namespace WPFDB.Common
{
    /// <summary>
    /// Encapsulates changes to underlying data stored in an ConferenceContext
    /// </summary>
    public class DataManager
    {
        private static DataManager instance;


        public static DataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataManager(new ConferenceEntities());
                }
                return instance;
            }
        }

        /// <summary>
        /// The underlying context tracking changes
        /// </summary>
        private IConferenceContext underlyingContext;

        private PersonRepository personRepository;
        private ConferenceRepository conferenceRepository;
        private SexRepository sexRepository;
        private SpecialityRepository specialityRepository;
        private ScienceDegreeRepository scienceDegreeRepository;
        private ScienceStatusRepository scienceStatusRepository;
        private RankRepository rankRepository;
        private CompanyRepository companyRepository;
        private UserRepository userRepository;
        private PaymentTypeRepository paymentTypeRepository;


        /// <summary>
        /// Initializes a new instance of the DataManager class.
        /// Changes registered in the DataManager are recorded in the supplied context
        /// </summary>
        /// <param name="context">The underlying context for this DataManager</param>
        ///   public DataManager(IEmployeeContext context)
        private DataManager(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            underlyingContext = context;
            personRepository = new PersonRepository(underlyingContext);
            conferenceRepository = new ConferenceRepository(underlyingContext);
            sexRepository = new SexRepository(underlyingContext);
            specialityRepository = new SpecialityRepository(underlyingContext);
            scienceDegreeRepository = new ScienceDegreeRepository(underlyingContext);
            scienceStatusRepository = new ScienceStatusRepository(underlyingContext);
            rankRepository = new RankRepository(underlyingContext);
            companyRepository = new CompanyRepository(underlyingContext);
            paymentTypeRepository = new PaymentTypeRepository(underlyingContext);
            userRepository = new UserRepository(underlyingContext);
        }




        #region Getting Lists

        public IEnumerable<Person> GetAllPersons()
        {
            return personRepository.All();
        }
        public IEnumerable<Conference> GetAllConferences()
        {
            return conferenceRepository.All();
        }
        public IEnumerable<Sex> GetAllSexes()
        {
            return sexRepository.All();
        }
        public IEnumerable<Speciality> GetAllSpecialities()
        {
            return specialityRepository.All();
        }
        public IEnumerable<ScienceDegree> GetAllScienceDegrees()
        {
            return scienceDegreeRepository.All();
        }
        public IEnumerable<ScienceStatus> GetAllScienceStatuses()
        {
            return scienceStatusRepository.All();
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return companyRepository.All();
        }

        public IEnumerable<OrderStatus> GetAllOrderStatuses()
        {
            return this.underlyingContext.OrderStatuses.ToList();
        }

        public IEnumerable<Rank> GetAllRanks()
        {
            return rankRepository.All();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return userRepository.All();
        }

        public IEnumerable<PaymentType> GetAllPaymentTypes()
        {
            return paymentTypeRepository.All();
        }
        #endregion

        /// <summary>
        /// Save all pending changes in this DataManager
        /// </summary>
        public void Save()
        {
            this.underlyingContext.Save();
        }

        public void Rollback()
        {
            IEnumerable<object> collection = from e in underlyingContext.GetObjectStateManager().GetObjectStateEntries
                                     (System.Data.EntityState.Modified | System.Data.EntityState.Deleted)
                                             select e.Entity;
            underlyingContext.Refresh(RefreshMode.StoreWins, collection);


            IEnumerable<object> AddedCollection = from e in underlyingContext.GetObjectStateManager().GetObjectStateEntries
                                                      (System.Data.EntityState.Added)
                                                  select e.Entity;
            foreach (object addedEntity in AddedCollection)
            {
                underlyingContext.Detach(addedEntity);
            } 
 
        }



        /// <summary>
        /// Creates a new instance of the specified object type
        /// NOTE: This pattern is used to allow the use of change tracking proxies
        ///       when running against the Entity Framework.
        /// </summary>
        /// <typeparam name="T">The type to create</typeparam>
        /// <returns>The newly created object</returns>
        public T CreateObject<T>() where T : class
        {
            return this.underlyingContext.CreateObject<T>();
        }

        public void RemoveObject<T>(T obj) where T : class
        {
            this.underlyingContext.RemoveObject(obj);
        }

        #region Adding

        public void AddSpeciality(Speciality obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("speciality");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.Specialities.AddObject(obj);
        }
        public void AddSex(Sex obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("sex");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.Sexes.AddObject(obj);
        }
        public void AddScienceStatus(ScienceStatus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("scienceStatus");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.ScienceStatuses.AddObject(obj);
        }
        public void AddScienceDegree(ScienceDegree obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("scienceStatus");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.ScienceDegrees.AddObject(obj);
        }

        public void AddOrderStatus(OrderStatus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("conference");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.OrderStatuses.AddObject(obj);
        }

        public void AddConference(Conference obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("conference");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.Conferences.AddObject(obj);
        }

        public void AddUser(User obj)
        {
            userRepository.Add(obj);
        }

        public void AddCompany(Company obj)
        {
            companyRepository.Add(obj);
        }

        public void AddPaymentType(PaymentType obj)
        {
            paymentTypeRepository.Add(obj);
        }

        public void AddRank(Rank obj)
        {
            rankRepository.Add(obj);
        }
        public void AddPerson(Person obj)
        {
            personRepository.Add(obj);

        }

        public void AddPersonConference(PersonConference obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("personConference");
            }
            underlyingContext.PersonConferences.AddObject(obj);
        }

        #endregion

        #region Removing

        public void RemoveSpeciality(Speciality obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("speciality");
            }

            this.CheckEntityBelongsToUnitOfWork(obj);
            foreach (var person in obj.Persons.ToList())
            {
                person.Speciality = null;
            }

            this.underlyingContext.Specialities.DeleteObject(obj);
        }
        public void RemoveSex(Sex obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("sex");
            }

            this.CheckEntityBelongsToUnitOfWork(obj);
            foreach (var person in obj.Persons.ToList())
            {
                person.Sex = null;
            }

            this.underlyingContext.Sexes.DeleteObject(obj);
        }
        public void RemoveScienceDegree(ScienceDegree obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("scienceDegree");
            }

            this.CheckEntityBelongsToUnitOfWork(obj);
            foreach (var person in obj.Persons.ToList())
            {
                person.ScienceDegree = null;
            }

            this.underlyingContext.ScienceDegrees.DeleteObject(obj);
        }

        public void RemoveOrderStatus(OrderStatus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("scienceDegree");
            }

            this.CheckEntityBelongsToUnitOfWork(obj);
            this.underlyingContext.OrderStatuses.DeleteObject(obj);
        }

        public void RemoveScienceStatus(ScienceStatus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("scienceStatus");
            }

            this.CheckEntityBelongsToUnitOfWork(obj);
            foreach (var person in obj.Persons.ToList())
            {
                person.ScienceStatus = null;
            }

            this.underlyingContext.ScienceStatuses.DeleteObject(obj);
        }
        public void RemoveConference(Conference obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("conference");
            }

            this.CheckEntityBelongsToUnitOfWork(obj);
            //foreach (var person in obj.Conferences.ToList())
            //{
            //    person.ScienceStatus = null;
            //}

            this.underlyingContext.Conferences.DeleteObject(obj);
        }
        public void RemovePerson(Person obj)
        {
            personRepository.Remove(obj);
        }

        public void RemoveRank(Rank obj)
        {
            rankRepository.Remove(obj);
        }

        public void RemoveCompany(Company obj)
        {
            companyRepository.Remove(obj);
        }

        public void RemoveUser(User obj)
        {
            userRepository.Remove(obj);
        }

        public void RemovePaymentType(PaymentType obj)
        {
            paymentTypeRepository.Remove(obj);
        }
        #endregion

        #region Getting Default Values

        public Sex GetDefaultSex()
        {
            return this.underlyingContext.Sexes.FirstOrDefault(o => o.Code == "-");
        }

        public OrderStatus GetDefaultOrderStatus()
        {
            return this.underlyingContext.OrderStatuses.FirstOrDefault(o => o.Code == "-");
        }

        public Speciality GetDefaultSpeciality()
        {
            return this.underlyingContext.Specialities.FirstOrDefault(o => o.Code == "-");
        }
        public ScienceDegree GetDefaultScienceDegree()
        {
            return this.underlyingContext.ScienceDegrees.FirstOrDefault(o => o.Code == "-");
        }
        public ScienceStatus GetDefaultScienceStatus()
        {
            return this.underlyingContext.ScienceStatuses.FirstOrDefault(o => o.Code == "-");
        }

        public Rank GetDefaultRank()
        {
            return this.underlyingContext.Ranks.FirstOrDefault(o => o.Code == "-");
        }

        public Company GetDefaultCompany()
        {
            return this.underlyingContext.Companies.FirstOrDefault(o => o.Code == "-");
        }

        public PaymentType GetDefaultPaymentType()
        {
            return this.underlyingContext.PaymentTypes.FirstOrDefault(o => o.Code == "-");
        }




        #endregion

        #region Getting Users



        #endregion

        private void CheckEntityBelongsToUnitOfWork(object entity)
        {
            if (!this.underlyingContext.IsObjectTracked(entity))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The supplied {0} is not part of this Unit of Work.", entity.GetType().Name));
            }
        }

        /// <summary>
        /// Verifies that the specified entity is not tracked in this DataManager
        /// </summary>
        /// <param name="entity">The object to search for</param>
        /// <exception cref="InvalidOperationException">Thrown if object is tracked by this DataManager</exception>
        private void CheckEntityDoesNotBelongToUnitOfWork(object entity)
        {
            if (this.underlyingContext.IsObjectTracked(entity))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The supplied {0} is already part of this Unit of Work.", entity.GetType().Name));
            }
        }

        public void EraseData()
        {
            foreach (var obj in underlyingContext.PersonConferences)
            {
                underlyingContext.PersonConferences.DeleteObject(obj);
            }
            Save();

            foreach (var obj in underlyingContext.Persons)
            {
                underlyingContext.Persons.DeleteObject(obj);
            }
            Save();

            foreach (var obj in underlyingContext.Conferences)
            {
                underlyingContext.Conferences.DeleteObject(obj);
            }
            Save();
            foreach (var obj in underlyingContext.ScienceDegrees)
            {
                underlyingContext.ScienceDegrees.DeleteObject(obj);
            }
            Save();
            foreach (var obj in underlyingContext.ScienceStatuses)
            {
                underlyingContext.ScienceStatuses.DeleteObject(obj);
            }
            Save();
            foreach (var obj in underlyingContext.Sexes)
            {
                underlyingContext.Sexes.DeleteObject(obj);
            }
            Save();
            foreach (var obj in underlyingContext.Specialities)
            {
                underlyingContext.Specialities.DeleteObject(obj);
            }
            Save();
            foreach (var obj in underlyingContext.Ranks)
            {
                underlyingContext.Ranks.DeleteObject(obj);
            }
            Save();
            foreach (var obj in underlyingContext.Companies)
            {
                underlyingContext.Companies.DeleteObject(obj);
            }
            Save();
            foreach (var obj in underlyingContext.PaymentTypes)
            {
                underlyingContext.PaymentTypes.DeleteObject(obj);
            }
            Save();
        }

        public void FillData()
        {
            AddOrderStatus(new OrderStatus {Id = GuidComb.Generate(), Code = "-", Name = "-"});
            AddOrderStatus(new OrderStatus { Id = GuidComb.Generate(), Code = "P", Name = "Оплачено" });
            AddOrderStatus(new OrderStatus { Id = GuidComb.Generate(), Code = "R", Name = "Возврат" });
            Save();

            AddConference(new Conference { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddConference(new Conference { Id = GuidComb.Generate(), Code = "", Name = "Conference 1" });
            AddConference(new Conference { Id = GuidComb.Generate(), Code = "", Name = "Conference 2" });
            Save();
            AddScienceDegree(new ScienceDegree { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddScienceDegree(new ScienceDegree { Id = GuidComb.Generate(), Code = "", Name = "Science Degree 1" });
            AddScienceDegree(new ScienceDegree { Id = GuidComb.Generate(), Code = "", Name = "Science Degree 2" });
            Save();
            AddScienceStatus(new ScienceStatus { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddScienceStatus(new ScienceStatus { Id = GuidComb.Generate(), Code = "", Name = "Science Status 1" });
            AddScienceStatus(new ScienceStatus { Id = GuidComb.Generate(), Code = "", Name = "Science Status 2" });
            Save();
            AddSex(new Sex { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddSex(new Sex { Id = GuidComb.Generate(), Code = "", Name = "Sex 1" });
            AddSex(new Sex { Id = GuidComb.Generate(), Code = "", Name = "Sex 2" });
            Save();
            AddSpeciality(new Speciality { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddSpeciality(new Speciality { Id = GuidComb.Generate(), Code = "", Name = "Speciality 1" });
            AddSpeciality(new Speciality { Id = GuidComb.Generate(), Code = "", Name = "Speciality 2" });
            Save();

            AddRank(new Rank { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddRank(new Rank { Id = GuidComb.Generate(), Code = "", Name = "Rank 1" });
            AddRank(new Rank { Id = GuidComb.Generate(), Code = "", Name = "Rank 2" });
            Save();
            AddCompany(new Company { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddCompany(new Company { Id = GuidComb.Generate(), Code = "", Name = "Company 1" });
            AddCompany(new Company { Id = GuidComb.Generate(), Code = "", Name = "Company 2" });
            Save();
            AddPaymentType(new PaymentType { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddPaymentType(new PaymentType { Id = GuidComb.Generate(), Code = "", Name = "Payment type 1" });
            AddPaymentType(new PaymentType { Id = GuidComb.Generate(), Code = "", Name = "Payment type 2" });
            Save();
            AddUser(new User { Id = GuidComb.Generate(), Name = "user", Password = "user", Role = "user" });
            AddUser(new User { Id = GuidComb.Generate(), Name = "admin", Password = "admin", Role = "admin" });
            AddUser(new User { Id = GuidComb.Generate(), Name = "111", Password = "111", Role = "user" });
            Save();
            var person = new Person
            {
                Id = GuidComb.Generate(),
                BirthDate = Convert.ToDateTime("01.01.1990"),
                FirstName = "Иванов",
                SecondName = "Иван",
                ThirdName = "Иванович",
                Post = "Man",
                WorkPlace = "WorkPlace",
                ScienceDegree = scienceDegreeRepository.GetByName("-"),
                ScienceStatus = scienceStatusRepository.GetByName("-"),
                Sex = sexRepository.GetByName("-"),
                Speciality = specialityRepository.GetByName("-"),
                Iacmac = new Iacmac
                {
                    Code = "42000001",
                    DateRegistration = Convert.ToDateTime("01.01.1990"),
                    Number = 1,
                    IsCardCreate = true,
                    IsCardSent = true,
                    IsForm = true,
                    IsMember = true
                }


            };

            AddPerson(person);

            Save();
            var details = new PersonConferences_Detail
            {
                Company = companyRepository.GetByName("-"),
                IsAbstract = true,
                DateArrive = Convert.ToDateTime("12.12.2014"),
                IsAdditionalMaterial = true,
                IsAutoreg = true,
                IsBadge = true,
                IsNeedBadge = true,
                IsArrive = true,
                IsComplect = true,
                Rank = rankRepository.GetByName("-")
            };
            var payment = new PersonConferences_Payment
            {
                Company = companyRepository.GetByName("-"),
                PaymentType = paymentTypeRepository.GetByName("-"),
                PaymentDocument = "Order",
                PaymentDate = Convert.ToDateTime("12.11.2014"),
                Money = 1200,
                IsComplect = true,
                OrderNumber = 5,
                OrderStatus = GetDefaultOrderStatus()
            };
            personRepository.AddConferenceInfoToPerson(person, conferenceRepository.GetByName("-"), details, payment);
            Save();
            details = new PersonConferences_Detail
            {
                Company = companyRepository.GetByName("Company 1"),
                IsAbstract = true,
                DateArrive = Convert.ToDateTime("12.12.2014"),
                IsAdditionalMaterial = true,
                IsAutoreg = true,
                IsBadge = true,
                IsNeedBadge = true,
                IsArrive = true,
                IsComplect = true,
                Rank = rankRepository.GetByName("-")
            };
            payment = new PersonConferences_Payment
            {
                Company = companyRepository.GetByName("Company 1"),
                PaymentType = paymentTypeRepository.GetByName("-"),
                PaymentDocument = "Order",
                PaymentDate = Convert.ToDateTime("12.11.2014"),
                Money = 1200,
                IsComplect = true,
                OrderNumber = 5,
                OrderStatus = GetDefaultOrderStatus()
            };
            personRepository.AddConferenceInfoToPerson(person, conferenceRepository.GetByName("Conference 1"), details, payment);
            Save();
            details = new PersonConferences_Detail
            {
                Company = companyRepository.GetByName("Company 2"),
                IsAbstract = true,
                DateArrive = Convert.ToDateTime("12.12.2014"),
                IsAdditionalMaterial = true,
                IsAutoreg = true,
                IsBadge = true,
                IsNeedBadge = true,
                IsArrive = true,
                IsComplect = true,
                Rank = rankRepository.GetByName("-")
            };
            payment = new PersonConferences_Payment
            {
                Company = companyRepository.GetByName("Company 2"),
                PaymentType = paymentTypeRepository.GetByName("-"),
                PaymentDocument = "Order",
                PaymentDate = Convert.ToDateTime("12.11.2014"),
                Money = 1200,
                IsComplect = true,
                OrderNumber = 5,
                OrderStatus = GetDefaultOrderStatus()
            };
            personRepository.AddConferenceInfoToPerson(person, conferenceRepository.GetByName("Conference 2"), details, payment);
            Save();
       
            Save();
        }

        public User GetUser(string name, string password)
        {
            return userRepository.GetUser(name, password);
        }

        public PersonConference GetPersonConference(Person person, Conference conference)
        {
            return
                this.underlyingContext.PersonConferences.SingleOrDefault(
                    o => o.PersonId == person.Id & o.ConferenceId == conference.Id);
        }

        public IEnumerable<PersonConference> GetPersonConferencesForPerson(Person person)
        {
            return this.underlyingContext.PersonConferences.Where(o => o.PersonId == person.Id).ToList();
        }
    }
}
