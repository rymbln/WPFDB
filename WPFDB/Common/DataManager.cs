using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Annotations;
using System.Windows.Documents;
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
       }




        #region Getting Lists

        public IEnumerable<Person> GetAllPersons()
        {
            return this.underlyingContext.Persons.ToList();
        }
        public IEnumerable<Conference> GetAllConferences()
        {
            return this.underlyingContext.Conferences.ToList();
        }
        public IEnumerable<Sex> GetAllSexes()
        {
            return this.underlyingContext.Sexes.ToList();
        }
        public IEnumerable<Speciality> GetAllSpecialities()
        {
            return this.underlyingContext.Specialities.ToList();
        }
        public IEnumerable<ScienceDegree> GetAllScienceDegrees()
        {
            return this.underlyingContext.ScienceDegrees.ToList();
        }
        public IEnumerable<ScienceStatus> GetAllScienceStatuses()
        {
            return this.underlyingContext.ScienceStatuses.ToList();
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return this.underlyingContext.Companies.ToList();
        }

        public IEnumerable<OrderStatus> GetAllOrderStatuses()
        {
            return this.underlyingContext.OrderStatuses.ToList();
        }

        public IEnumerable<Rank> GetAllRanks()
        {
            return this.underlyingContext.Ranks.ToList();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this.underlyingContext.Users.ToList();
        }

        public IEnumerable<PaymentType> GetAllPaymentTypes()
        {
            return this.underlyingContext.PaymentTypes.ToList();
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

        public void AddContactType(ContactType obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("contactType");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.ContactTypes.AddObject(obj);
        }

        public void AddUser(User obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("User");
            }
            this.underlyingContext.Users.AddObject(obj);
        }

        public void AddCompany(Company obj)
        {

            if (obj == null)
            {
                throw new ArgumentNullException("Company");
            }
            this.underlyingContext.Companies.AddObject(obj);
        }

        public void AddPaymentType(PaymentType obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("PaymentType");
            }
            this.underlyingContext.PaymentTypes.AddObject(obj);
        }

        public void AddRank(Rank obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Rank");
            }
            this.underlyingContext.Ranks.AddObject(obj);
        }
        public void AddPerson(Person obj)
        {

            if (obj == null)
            {
                throw new ArgumentNullException("person");
            }
            this.underlyingContext.Persons.AddObject(obj);
           Save();
        }

        public void AddAbstractStatus(AbstractStatus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("abstractStatus");
            }
            underlyingContext.AbstractStatuses.AddObject(obj);
        }

        public void AddAbstractWork(AbstractWork obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("abstractWork");
            }
            underlyingContext.AbstractWorks.AddObject(obj);
        }

     
        public void AddAbstract(Abstract obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("abstract");
            }
            underlyingContext.Abstracts.AddObject(obj);
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
            if (obj == null)
            {
                throw new ArgumentNullException("person");
            }
            var lst = obj.PersonConferences;
            foreach (var item in lst)
            {
                RemovePersonConference(item);
            }
            this.underlyingContext.Persons.DeleteObject(obj);
            Save();
        }

        private void RemovePersonConference(PersonConference obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("personConference");
            }
            var lst = obj.Abstracts;
            foreach (var item in lst)
            {
                RemoveAbstract(item);
            }
            this.underlyingContext.PersonConferences.DeleteObject(obj);
            Save();
        }

        private void RemoveAbstract(Abstract obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("abstract");
            }

            this.underlyingContext.Abstracts.DeleteObject(obj);
            Save();
        }

        public void RemoveRank(Rank obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Rank");
            }
            this.underlyingContext.Ranks.DeleteObject(obj);
        }

        public void RemoveCompany(Company obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Company");
            }
            this.underlyingContext.Companies.DeleteObject(obj);
        }

        public void RemoveUser(User obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("User");
            }
            this.underlyingContext.Users.DeleteObject(obj);
            this.CheckEntityBelongsToUnitOfWork(obj);
        }

        public User GetUser(string name)
        {
            return this.underlyingContext.Users.FirstOrDefault(o => o.Name == name);
        }

       
        public IEnumerable<User> GetUsers()
        {
            return this.underlyingContext.Users.ToList();
        }

        public void RemovePaymentType(PaymentType obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("PaymentType");
            }
            this.underlyingContext.PaymentTypes.DeleteObject(obj);
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

        public ContactType GetDefaultContactType()
        {
            return this.underlyingContext.ContactTypes.FirstOrDefault(o => o.Code == "-");
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

        public ContactType GetContactTypeByName(string name)
        {
            return this.underlyingContext.ContactTypes.FirstOrDefault(o => o.Name == name);
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
            foreach (var obj in underlyingContext.Abstracts)
            {
                underlyingContext.Abstracts.DeleteObject(obj);
            }
            Save();
            foreach (var obj in underlyingContext.PersonConferences)
            {
                underlyingContext.PersonConferences.DeleteObject(obj);
            }
            Save();

            foreach (var obj in underlyingContext.Persons)
            {
                foreach (var email in obj.Emails.ToList())
                {
                    underlyingContext.Emails.DeleteObject(email);
                }
                Save();
                var phones = new List<Phone>();
                foreach (var phone in obj.Phones.ToList())
                {
                    underlyingContext.Phones.DeleteObject(phone);
                }
                Save();
                foreach (var adr in obj.Addresses.ToList())
                {
                    underlyingContext.Addresses.DeleteObject(adr);
                }
                underlyingContext.Persons.DeleteObject(obj);
                Save();
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
            foreach (var obj in underlyingContext.OrderStatuses)
            {
                underlyingContext.OrderStatuses.DeleteObject(obj);
            }
            foreach (var obj in underlyingContext.OrderStatuses)
            {
                underlyingContext.OrderStatuses.DeleteObject(obj);
            }
            foreach (var obj in underlyingContext.ContactTypes)
            {
                underlyingContext.ContactTypes.DeleteObject(obj);
            }
            foreach (var obj in underlyingContext.AbstractStatuses)
            {
                underlyingContext.AbstractStatuses.DeleteObject(obj);
            }
            foreach (var obj in underlyingContext.Users)
            {
                underlyingContext.Users.DeleteObject(obj);
            }
            Save();
        }

        public void FillData()
        {      
            AddUser(new User { Id = GuidComb.Generate(), Name = "user", Password = "user", Email = "user@example.com",Role = "user" });
            AddUser(new User { Id = GuidComb.Generate(), Name = "admin", Password = "admin", Email = "admin@example.com",  Role = "admin" });
            AddUser(new User { Id = GuidComb.Generate(), Name = "111", Password = "111", Email = "111@example.com",  Role = "user" });
            Save();
    
            AddAbstractStatus(new AbstractStatus {Id=GuidComb.Generate(), Code = "-", Name = "-"});
            AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "I", Name = "In work" });
            AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "RV", Name = "Revision" });
            AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "A", Name = "Accepted" });
            AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "RJ", Name = "Rejected" });
            AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "R2", Name = "Rejected Twice" });
            Save();

            AddContactType(new ContactType{Id=GuidComb.Generate(), Code = "-", Name="-"});
            AddContactType(new ContactType { Id = GuidComb.Generate(), Code = "H", Name = "Home" });
            AddContactType(new ContactType { Id = GuidComb.Generate(), Code = "W", Name = "Work" });
            AddContactType(new ContactType { Id = GuidComb.Generate(), Code = "O", Name = "Other" });
            Save();

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
            //  var person = new Person
            //{
            //    Id = GuidComb.Generate(),
            //    BirthDate = Convert.ToDateTime("01.01.1990"),
            //    FirstName = "Иванов",
            //    SecondName = "Иван",
            //    ThirdName = "Иванович",
            //    Post = "Man",
            //    WorkPlace = "WorkPlace",
            //    ScienceDegree = scienceDegreeRepository.GetByName("-"),
            //    ScienceStatus = scienceStatusRepository.GetByName("-"),
            //    Sex = sexRepository.GetByName("-"),
            //    Speciality = specialityRepository.GetByName("-"),
            //    Iacmac = new Iacmac
            //    {
            //        Code = "42000001",
            //        DateRegistration = Convert.ToDateTime("01.01.1990"),
            //        Number = 1,
            //        IsCardCreate = true,
            //        IsCardSent = true,
            //        IsForm = true,
            //        IsMember = true
            //    }


            //};

            //AddPerson(person);

            //Save();

            //person.Emails.Add(new Email{ Id=GuidComb.Generate(), Name = "test@mail.ru", ContactType = GetDefaultContactType()});
            //person.Emails.Add(new Email { Id = GuidComb.Generate(), Name = "work@mail.ru", ContactType = GetContactTypeByName("Work") });
            //person.Emails.Add(new Email { Id = GuidComb.Generate(), Name = "Home@mail.ru", ContactType = GetContactTypeByName("Home") });
            //person.Emails.Add(new Email { Id = GuidComb.Generate(), Name = "other@mail.ru", ContactType = GetContactTypeByName("Other") });
            //Save();
            //person.Phones.Add(new Phone{Id = GuidComb.Generate(), ContactType = GetDefaultContactType(), Number = "+71234567890"});
            //person.Phones.Add(new Phone { Id = GuidComb.Generate(), ContactType = GetContactTypeByName("Home"), Number = "+71234567890" });
            //person.Phones.Add(new Phone { Id = GuidComb.Generate(), ContactType = GetContactTypeByName("Other"), Number = "+71234567890" });
            //person.Phones.Add(new Phone { Id = GuidComb.Generate(), ContactType = GetContactTypeByName("Work"), Number = "+71234567890" });
            //Save();
            //person.Addresses.Add(new Address{Id=GuidComb.Generate(),ZipCode = "214000",ContactType = GetContactTypeByName("Home"), CountryName = "Russia", RegionName = "Smolensk region", CityName = "Smolensk", StreetHouseName = "Petr Alekseev st. 19"});
            //person.Addresses.Add(new Address { Id = GuidComb.Generate(), ZipCode = "214000",ContactType = GetContactTypeByName("Work"), CountryName = "Russia", RegionName = "Smolensk region", CityName = "Smolensk", StreetHouseName = "Kirov st. 46" });
            //Save();
            //var details = new PersonConferences_Detail
            //{
            //    Company = companyRepository.GetByName("-"),
            //    DateArrive = Convert.ToDateTime("12.12.2014"),
            //    IsAbstract = false,
            //    IsAutoreg = true,
            //    IsBadge = true,
            //    IsNeedBadge = true,
            //    IsArrive = true,
            //    Rank = rankRepository.GetByName("-")
            //};
            //var payment = new PersonConferences_Payment
            //{
            //    Company = companyRepository.GetByName("-"),
            //    PaymentType = paymentTypeRepository.GetByName("-"),
            //    PaymentDocument = "Order",
            //    PaymentDate = Convert.ToDateTime("12.11.2014"),
            //    Money = 1200,
            //    IsComplect = true,
            //    OrderNumber = 5,
            //    OrderStatus = GetDefaultOrderStatus()
            //};
            //personRepository.AddConferenceInfoToPerson(person, conferenceRepository.GetByName("-"), details, payment);
            //Save();
            //details = new PersonConferences_Detail
            //{
            //    Company = companyRepository.GetByName("Company 1"),
            //    DateArrive = Convert.ToDateTime("12.12.2014"),
            //    IsAbstract = false,
            //    IsAutoreg = true,
            //    IsBadge = true,
            //    IsNeedBadge = true,
            //    IsArrive = true,
            //    Rank = rankRepository.GetByName("-")
            //};
            //payment = new PersonConferences_Payment
            //{
            //    Company = companyRepository.GetByName("Company 1"),
            //    PaymentType = paymentTypeRepository.GetByName("-"),
            //    PaymentDocument = "Order",
            //    PaymentDate = Convert.ToDateTime("12.11.2014"),
            //    Money = 1200,
            //    IsComplect = true,
            //    OrderNumber = 5,
            //    OrderStatus = GetDefaultOrderStatus()
            //};
            //personRepository.AddConferenceInfoToPerson(person, conferenceRepository.GetByName("Conference 1"), details, payment);
            //Save();
            //details = new PersonConferences_Detail
            //{
            //    Company = companyRepository.GetByName("Company 2"),
            //    DateArrive = Convert.ToDateTime("12.12.2014"),
            //    IsAbstract = false,
            //    IsAutoreg = true,
            //    IsBadge = true,
            //    IsNeedBadge = true,
            //    IsArrive = true,
            //    Rank = rankRepository.GetByName("-")
            //};
            //payment = new PersonConferences_Payment
            //{
            //    Company = companyRepository.GetByName("Company 2"),
            //    PaymentType = paymentTypeRepository.GetByName("-"),
            //    PaymentDocument = "Order",
            //    PaymentDate = Convert.ToDateTime("12.11.2014"),
            //    Money = 1200,
            //    IsComplect = true,
            //    OrderNumber = 5,
            //    OrderStatus = GetDefaultOrderStatus()
            //};
            //personRepository.AddConferenceInfoToPerson(person, conferenceRepository.GetByName("Conference 2"), details, payment);
            Save();
        }


        public User GetUser(string name, string pass)
        {
            return this.underlyingContext.Users.FirstOrDefault(o => o.Name == name && o.Password == pass);
        }

        public PersonConference GetPersonConference(Person person, Conference conference)
        {
            return
                this.underlyingContext.PersonConferences.SingleOrDefault(
                    o => o.PersonId == person.Id & o.ConferenceId == conference.Id);
        }

        public PersonConference GetPersonConferenceByID(string id)
        {
            return this.underlyingContext.PersonConferences.SingleOrDefault(o => o.PersonConferenceId == new Guid(id));
        }

        public IEnumerable<PersonConference> GetPersonConferencesForPerson(Person person)
        {
            return this.underlyingContext.PersonConferences.Where(o => o.PersonId == person.Id).ToList();
        }

        public IEnumerable<ContactType> GetAllContactTypes()
        {
            return this.underlyingContext.ContactTypes.ToList();
        }

        public void RemoveContactType(ContactType obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("contactType");
            }

            this.CheckEntityBelongsToUnitOfWork(obj);
            this.underlyingContext.ContactTypes.DeleteObject(obj);
        }

        public void AddEmailToPerson(Person person, Email email)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }

            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(email);
            this.CheckEntityBelongsToUnitOfWork(person);

            this.underlyingContext.Emails.AddObject(email);
            person.Emails.Add(email);
        }

        public void RemoveEmail(Person person, Email email)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }
            this.CheckEntityBelongsToUnitOfWork(email);
            this.CheckEntityBelongsToUnitOfWork(person);
            if (!person.Emails.Contains(email))
            {
                throw new InvalidOperationException("This email doesnot belong to person");
            }
            person.Emails.Remove(email);
            this.underlyingContext.Emails.DeleteObject(email);
        }

        public void AddAddressToPerson(Person person, Address address)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }

            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(address);
            this.CheckEntityBelongsToUnitOfWork(person);

            this.underlyingContext.Addresses.AddObject(address);
            person.Addresses.Add(address);
        }

        public void RemoveAddress(Person person, Address address)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }
            this.CheckEntityBelongsToUnitOfWork(address);
            this.CheckEntityBelongsToUnitOfWork(person);
            if (!person.Addresses.Contains(address))
            {
                throw new InvalidOperationException("This email doesnot belong to person");
            }
            person.Addresses.Remove(address);
            this.underlyingContext.Addresses.DeleteObject(address);
        }

        public void AddPhoneToPerson(Person person, Phone phone)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }

            if (phone == null)
            {
                throw new ArgumentNullException("phone");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(phone);
            this.CheckEntityBelongsToUnitOfWork(person);

            this.underlyingContext.Phones.AddObject(phone);
            person.Phones.Add(phone);
        }

        public void RemovePhone(Person person, Phone phone)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }
            if (phone == null)
            {
                throw new ArgumentNullException("phone");
            }
            this.CheckEntityBelongsToUnitOfWork(phone);
            this.CheckEntityBelongsToUnitOfWork(person);
            if (!person.Phones.Contains(phone))
            {
                throw new InvalidOperationException("This email doesnot belong to person");
            }
            person.Phones.Remove(phone);
            this.underlyingContext.Phones.DeleteObject(phone);
        }

        public IEnumerable<AbstractStatus> GetAllAbstractStatuses()
        {
            return this.underlyingContext.AbstractStatuses.ToList();
        }

        public IEnumerable<Abstract> GetAllAbstracts()
        {
            return this.underlyingContext.Abstracts.ToList();
        }



        public Conference GetDefaultConference()
        {
            return this.underlyingContext.Conferences.SingleOrDefault(o => o.Code == "-");
        }

        public void AddAbstractToPerson(Person person, Conference conference, Abstract abs)
        {
            var personConference =
                this.underlyingContext.PersonConferences.FirstOrDefault(
                    o => o.PersonId == person.Id && o.ConferenceId == conference.Id);
            if (personConference == null)
            {
                personConference = new PersonConference
                {
                    PersonConferenceId = GuidComb.Generate(),
                    PersonId = person.Id,
                    ConferenceId = conference.Id
                };
                this.underlyingContext.PersonConferences.AddObject(personConference);
                Save();
            }

            abs.PersonConferenceId = personConference.PersonConferenceId;
            Save();
        }

        public void SetPersonConferenceAbstractOn(PersonConference personConference)
        {
            // TODO - Сделать чтобы в PersonConferenceDetail проставлялся флажок IsAbstract
        }

        public IEnumerable<Abstract> GetAbstractsForPerson(Person person)
        {
            return this.underlyingContext.Abstracts.Where(a => a.PersonConference.PersonId == person.Id).ToList();
        }
    }
}
