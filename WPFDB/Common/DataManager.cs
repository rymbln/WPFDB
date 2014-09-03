using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.Specialities.AddObject(obj);
            Save();
        }
        public void AddSex(Sex obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("sex");
            }
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.Sexes.AddObject(obj);
            Save();
        }
        public void AddScienceStatus(ScienceStatus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("scienceStatus");
            }
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.ScienceStatuses.AddObject(obj);
            Save();
        }
        public void AddScienceDegree(ScienceDegree obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("scienceStatus");
            }
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.ScienceDegrees.AddObject(obj);
            Save();
        }

        public void AddOrderStatus(OrderStatus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("conference");
            }
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.OrderStatuses.AddObject(obj);
            Save();
        }

        public void AddConference(Conference obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("conference");
            }
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.Conferences.AddObject(obj);
            Save();
        }

        public void AddContactType(ContactType obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("contactType");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.ContactTypes.AddObject(obj);
            Save();
        }

        public void AddUser(User obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("User");
            }
            this.underlyingContext.Users.AddObject(obj);
            Save();
        }

        public void AddCompany(Company obj)
        {

            if (obj == null)
            {
                throw new ArgumentNullException("Company");
            }
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            this.underlyingContext.Companies.AddObject(obj);
            Save();
        }

        public void AddPaymentType(PaymentType obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("PaymentType");
            }
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            this.underlyingContext.PaymentTypes.AddObject(obj);
            Save();
        }

        public void AddRank(Rank obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Rank");
            }
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            this.underlyingContext.Ranks.AddObject(obj);
            Save();
        }
        public void AddPerson(Person obj)
        {

            if (obj == null)
            {
                throw new ArgumentNullException("person");
            }
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            this.underlyingContext.Persons.AddObject(obj);
            Save();
        }

        public void AddAbstractStatus(AbstractStatus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("abstractStatus");
            }
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            underlyingContext.AbstractStatuses.AddObject(obj);
            Save();
        }

        public void AddAbstractWork(AbstractWork obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("abstractWork");
            }
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            underlyingContext.AbstractWorks.AddObject(obj);
            Save();
        }


        public void AddAbstract(Abstract obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("abstract");
            }
            obj.DateAdd = DateTime.Now;
            obj.DateUpdate = DateTime.Now;
            var currentUser = Authentification.GetCurrentUser();
            obj.User = currentUser == null ? "-" : currentUser.Name;
            underlyingContext.Abstracts.AddObject(obj);
            Save();
        }

        public PersonConference AddPersonConference(Person person, Conference conference)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }
            if (conference == null)
            {
                throw new ArgumentNullException("conference");
            }
            var personConference = CreateObject<PersonConference>();
            personConference.PersonConferenceId = GuidComb.Generate();
            personConference.PersonId = person.Id;
            personConference.ConferenceId = conference.Id;
            underlyingContext.PersonConferences.AddObject(personConference);
            Save();
            personConference.PersonConferences_Detail = DefaultManager.Instance.DefaultPersonConferenceDetail(personConference.PersonConferenceId);
            personConference.PersonConferences_Payment = DefaultManager.Instance.DefaultPersonConferencePayment(personConference.PersonConferenceId);
            Save();
            return personConference;
        }

        public void AddPersonConference(PersonConference obj)
        {

            underlyingContext.PersonConferences.AddObject(obj);
            Save();

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
            Save();
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
            Save();
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
            Save();
        }

        public void RemoveOrderStatus(OrderStatus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("scienceDegree");
            }

            this.CheckEntityBelongsToUnitOfWork(obj);
            this.underlyingContext.OrderStatuses.DeleteObject(obj);
            Save();
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
            Save();
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
            Save();
        }
        public void RemovePerson(Person obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("person");
            }
            var lst = obj.PersonConferences.ToList();
            foreach (var item in lst)
            {
                RemovePersonConference(item);
            }
            var addr = obj.Addresses.ToList();
            foreach (var item in addr)
            {
                RemoveAddress(obj, item);
            }
            var phones = obj.Phones.ToList();
            foreach (var item in phones)
            {
                RemovePhone(obj, item);
            }
            var emails = obj.Emails.ToList();
            foreach (var email in emails)
            {
                RemoveEmail(obj, email);
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
            var lst = obj.Abstracts.ToList();
            foreach (var item in lst)
            {
                RemoveAbstract(item);
            }
            this.underlyingContext.PersonConferences.DeleteObject(obj);
            Save();
        }

        //private void RemoveAbstract(Abstract obj)
        //{
        //    if (obj == null)
        //    {
        //        throw new ArgumentNullException("abstract");
        //    }

        //    this.underlyingContext.Abstracts.DeleteObject(obj);
        //    Save();
        //}

        public void RemoveRank(Rank obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Rank");
            }
            this.underlyingContext.Ranks.DeleteObject(obj);
            Save();
        }

        public void RemoveCompany(Company obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Company");
            }
            this.underlyingContext.Companies.DeleteObject(obj);
            Save();
        }

        public void RemoveUser(User obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("User");
            }
            this.underlyingContext.Users.DeleteObject(obj);
            this.CheckEntityBelongsToUnitOfWork(obj);
            Save();
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
            Save();
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
            foreach (var obj in underlyingContext.AbstractWorks.ToList())
            {
                underlyingContext.AbstractWorks.DeleteObject(obj);
            }
            Save();
            foreach (var obj in underlyingContext.Abstracts.ToList())
            {
                underlyingContext.Abstracts.DeleteObject(obj);
            }
            Save();
            foreach (var obj in underlyingContext.PersonConferences.ToList())
            {
                underlyingContext.PersonConferences.DeleteObject(obj);
            }
            Save();
            IList<Person> persons = underlyingContext.Persons.ToList();
            foreach (Person obj in persons)
            {
                //       foreach (var email in obj.Emails.ToList())
                //       {
                //           obj.Emails.Remove(email);
                //       }
                //       Save();
                //       var phones = new List<Phone>();
                //       foreach (var phone in obj.Phones.ToList())
                //       {
                //           obj.Phones.Remove(phone);
                //       }
                //       Save();
                //       foreach (var adr in obj.Addresses.ToList())
                //       {
                //           obj.Addresses.Remove(adr);
                //       }
                //       Save();
                ////       obj.Iacmac = null;
                RemovePerson(obj);
                //       underlyingContext.Persons.DeleteObject(obj);
                //    Save();
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
            AddUser(new User { Id = GuidComb.Generate(), Name = "user", FullName = "user", Password = "user", Email = "user@example.com", Role = "user" });
            AddUser(new User { Id = GuidComb.Generate(), Name = "admin", FullName = "user", Password = "admin", Email = "admin@example.com", Role = "admin" });
            AddUser(new User { Id = GuidComb.Generate(), Name = "111", FullName = "user",  Password = "111", Email = "111@example.com", Role = "user" });
            Save();

            AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0});
            //AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "I", Name = "In work" });
            //AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "RV", Name = "Revision" });
            //AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "A", Name = "Accepted" });
            //AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "RJ", Name = "Rejected" });
            //AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "R2", Name = "Rejected Twice" });
            Save();

            AddContactType(new ContactType { Id = GuidComb.Generate(), Code = "-", Name = "-" , SourceId = 0});
            AddContactType(new ContactType { Id = GuidComb.Generate(), Code = "H", Name = "Домашний", SourceId = 1});
            AddContactType(new ContactType { Id = GuidComb.Generate(), Code = "W", Name = "Рабочий", SourceId = 2 });
            AddContactType(new ContactType { Id = GuidComb.Generate(), Code = "O", Name = "Другой", SourceId = 3});
            Save();

            AddOrderStatus(new OrderStatus { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0});
            AddOrderStatus(new OrderStatus { Id = GuidComb.Generate(), Code = "О", Name = "Оплачен", SourceId = 1});
            AddOrderStatus(new OrderStatus { Id = GuidComb.Generate(), Code = "В", Name = "Возврат" , SourceId = 2});
            Save();

            AddConference(new Conference { Id = GuidComb.Generate(), Code = "-", Name = "-" , SourceId = 0});
            //AddConference(new Conference { Id = GuidComb.Generate(), Code = "", Name = "Conference 1" });
            //AddConference(new Conference { Id = GuidComb.Generate(), Code = "", Name = "Conference 2" });
            Save();
            AddScienceDegree(new ScienceDegree { Id = GuidComb.Generate(), Code = "-", Name = "-" , SourceId = 0});
            //AddScienceDegree(new ScienceDegree { Id = GuidComb.Generate(), Code = "", Name = "Science Degree 1" });
            //AddScienceDegree(new ScienceDegree { Id = GuidComb.Generate(), Code = "", Name = "Science Degree 2" });
            Save();
            AddScienceStatus(new ScienceStatus { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0 });
            //AddScienceStatus(new ScienceStatus { Id = GuidComb.Generate(), Code = "", Name = "Science Status 1" });
            //AddScienceStatus(new ScienceStatus { Id = GuidComb.Generate(), Code = "", Name = "Science Status 2" });
            Save();
            AddSex(new Sex { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0});
            AddSex(new Sex { Id = GuidComb.Generate(), Code = "М", Name = "Мужской" , SourceId = 1});
            AddSex(new Sex { Id = GuidComb.Generate(), Code = "Ж", Name = "Женский", SourceId = 2});
            Save();
            AddSpeciality(new Speciality { Id = GuidComb.Generate(), Code = "-", Name = "-" , SourceId = 0});
            //AddSpeciality(new Speciality { Id = GuidComb.Generate(), Code = "", Name = "Speciality 1" });
            //AddSpeciality(new Speciality { Id = GuidComb.Generate(), Code = "", Name = "Speciality 2" });
            Save();

            AddRank(new Rank { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0});
            ////AddRank(new Rank { Id = GuidComb.Generate(), Code = "", Name = "Rank 1" });
            //AddRank(new Rank { Id = GuidComb.Generate(), Code = "", Name = "Rank 2" });
            Save();
            AddCompany(new Company { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0});
            //AddCompany(new Company { Id = GuidComb.Generate(), Code = "", Name = "Company 1" });
            //AddCompany(new Company { Id = GuidComb.Generate(), Code = "", Name = "Company 2" });
            Save();
            AddPaymentType(new PaymentType { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0});
            //AddPaymentType(new PaymentType { Id = GuidComb.Generate(), Code = "", Name = "Payment type 1" });
            //AddPaymentType(new PaymentType { Id = GuidComb.Generate(), Code = "", Name = "Payment type 2" });
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
            Save();
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

            email.PersonId = person.Id;

    //        this.CheckEntityBelongsToUnitOfWork(email);
    //        this.CheckEntityBelongsToUnitOfWork(person);

            this.underlyingContext.Emails.AddObject(email);
            person.Emails.Add(email);
            Save();
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
            Save();
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
            address.PersonId = person.Id;
    //        this.CheckEntityBelongsToUnitOfWork(address);
    //        this.CheckEntityBelongsToUnitOfWork(person);

            this.underlyingContext.Addresses.AddObject(address);
            person.Addresses.Add(address);
            Save();
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
            Save();
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
            phone.PersonId = person.Id;
    //        this.CheckEntityBelongsToUnitOfWork(phone);
      //      this.CheckEntityBelongsToUnitOfWork(person);

            this.underlyingContext.Phones.AddObject(phone);
            person.Phones.Add(phone);
            Save();
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
            Save();
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

        public void AddAbstractToPerson(Person person, Abstract abs)
        {
            var personConference =
                this.underlyingContext.PersonConferences.FirstOrDefault(
                    o => o.PersonId == person.Id && o.ConferenceId == DefaultManager.Instance.DefaultConference.Id);
            if (personConference == null)
            {
                personConference = new PersonConference
                {
                    PersonConferenceId = GuidComb.Generate(),
                    PersonId = person.Id,
                    ConferenceId = DefaultManager.Instance.DefaultConference.Id
                };
                this.underlyingContext.PersonConferences.AddObject(personConference);
                Save();
            }

            abs.PersonConferenceId = personConference.PersonConferenceId;
            this.underlyingContext.Abstracts.AddObject(abs);
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

        public Person GetPersonByPersonConferenceId(Guid id)
        {
            return this.underlyingContext.PersonConferences.FirstOrDefault(o => o.PersonConferenceId == id).Person;

        }

        public IEnumerable<Email> GetEmailsByPersonId(Guid id)
        {
            return this.underlyingContext.Emails.Where(o => o.PersonId == id).ToList();
        }

        public void AddAbstractWorkToAbstract(ViewModel.AbstractViewModel currentAbstract, AbstractWork abstractWork)
        {
            abstractWork.AbstractId = Guid.Parse(currentAbstract.Id);
            this.underlyingContext.AbstractWorks.AddObject(abstractWork);
            Save();

        }

        public void RemoveAbstractWork(AbstractWork abstractWork)
        {
            if (abstractWork == null)
            {
                throw new ArgumentNullException("abstractWork");
            }
            this.underlyingContext.AbstractWorks.DeleteObject(abstractWork);
        }

        public void RemoveAbstract(Abstract p)
        {
            if (p.AbstractWorks != null)
            {
                foreach (var item in p.AbstractWorks.ToList())
                {
                    this.underlyingContext.AbstractWorks.DeleteObject(item);
                }
            }
            this.underlyingContext.Abstracts.DeleteObject(p);
            Save();
        }

        public Sex GetSexBySourceId(int id)
        {
            var obj = underlyingContext.Sexes.FirstOrDefault(o => o.SourceId == id);
            return (obj == null) ? DefaultManager.Instance.DefaultSex : obj;
        }

        public Speciality GetSpecialityBySourceId(int id)
        {
            var obj = underlyingContext.Specialities.FirstOrDefault(o => o.SourceId == id);
            return (obj == null) ? DefaultManager.Instance.DefaultSpeciality : obj;
        }

        public ScienceDegree GetScienceDegreeBySourceID(int id)
        {
            var obj = underlyingContext.ScienceDegrees.FirstOrDefault(o => o.SourceId == id);
            return (obj == null) ? DefaultManager.Instance.DefaultScienceDegree : obj;
        }

        public ScienceStatus GetScienceStatusBySourceId(int id)
        {
            var obj = underlyingContext.ScienceStatuses.FirstOrDefault(o => o.SourceId == id);
            return (obj == null) ? DefaultManager.Instance.DefaultScienceStatus : obj;
        }

        public bool GetBoolLogicBySourceId(int id)
        {
            switch (id)
            {
                case 1:
                    return false;
                    break;
                case 2:
                    return true;
                    break;
                default:
                    return false;
                    break;
            }
        }


        public void AddIacmacToPerson(Person obj, Iacmac iacmac)
        {
            iacmac.PersonId = obj.Id;
            obj.Iacmac = iacmac;
            Save();
        }

        public Person GetPersonBySourceId(int id)
        {
            var obj = underlyingContext.Persons.FirstOrDefault(o => o.SourceId == id);
            return obj;
        }

        public Conference GetConferenceBySourceId(int id)
        {
            var obj = underlyingContext.Conferences.FirstOrDefault(o => o.SourceId == id);
            return obj;
        }

        public PersonConference GetPersonConferenceBySourceID(int p)
        {
            var obj = underlyingContext.PersonConferences.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public Rank GetRankBySourceId(int p)
        {
            var obj = underlyingContext.Ranks.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public Company GetCompanyBySourceId(int p)
        {
            var obj = underlyingContext.Companies.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public void AddPersonConferenceDetail(PersonConferences_Detail obj)
        {
            var personConference =
                underlyingContext.PersonConferences.FirstOrDefault(o => o.PersonConferenceId == obj.PersonConferenceId);
            personConference.PersonConferences_Detail = obj;
            Save();
        }

        public void AddPersonConferenceMoney(PersonConferences_Payment obj)
        {
            var personConference =
                underlyingContext.PersonConferences.FirstOrDefault(o => o.PersonConferenceId == obj.PersonConferenceId);
            personConference.PersonConferences_Payment = obj;
            Save();
        }

        public PaymentType GetPaymentTypeBySourceId(int p)
        {
            var obj = underlyingContext.PaymentTypes.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public OrderStatus GetOrderStatusBySourceId(int p)
        {
            var obj = underlyingContext.OrderStatuses.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public AbstractStatus GetAbstractStatusBySourceId(int p)
        {
            var obj = underlyingContext.AbstractStatuses.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }



        public Abstract GetAbstractBySourceId(int p)
        {
            var obj = underlyingContext.Abstracts.FirstOrDefault((o => o.SourceId == p));
            return obj;

        }

        public User GetUserBySourceId(int p)
        {
            var obj = underlyingContext.Users.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public ContactType GetContactTypeBySourceId(int p)
        {
            var obj = underlyingContext.ContactTypes.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public List<PersonConference> GetConferenceForPersonRegistered(Guid guid)
        {
            return underlyingContext.PersonConferences
                .Where(o => o.PersonId == guid)
         //       .Include(o => o.PersonConferences_Detail)
                
                .ToList();
        }

        public List<PersonConference> GetConferenceForPersonArrived(Guid guid)
        {
            return underlyingContext.PersonConferences
                .Where(o => o.PersonId == guid && o.PersonConferences_Detail.IsArrive == true)
           //     .Include(o => o.PersonConferences_Detail)
                .ToList();
        }
    }
}
