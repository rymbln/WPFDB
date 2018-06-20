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
using Xceed.Wpf.Toolkit.Primitives;

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
            get { return instance ?? (instance = new DataManager(new ConferenceEntities())); }
        }

        /// <summary>
        /// The underlying context tracking changes
        /// </summary>
        private readonly IConferenceContext context;




        /// <summary>
        /// Initializes a new instance of the DataManager class.
        /// Changes registered in the DataManager are recorded in the supplied context
        /// </summary>
        /// <param name="context">The underlying context for this DataManager</param>
        ///   public DataManager(IEmployeeContext context)
        private DataManager(IConferenceContext cntxt)
        {
            if (cntxt == null)
            {
                throw new ArgumentNullException("context");
            }

            context = cntxt;
        }




        #region Getting Lists

        public IEnumerable<Person> GetAllPersonForConference(Guid conf)
        {
            return context.PersonConferences.Where(o => o.Conference.Id == conf).Include(o => o.Person).Select(o => o.Person);

        }

        public IEnumerable<Person> GetAllPersons()
        {
            return this.context.Persons.Include(o => o.PersonConferences).ToList();
        }
        public IEnumerable<Conference> GetAllConferences()
        {
            return this.context.Conferences.OrderBy(r => r.DateAdd).ToList();
        }
        public List<ItemGuidName> GetAllConferencesItem()
        {
            return context.Conferences.OrderBy(r => r.DateAdd).Select(o => new ItemGuidName { Id = o.Id, Name = o.Name }).ToList(); ;
        }
        public IEnumerable<Sex> GetAllSexes()
        {
            return this.context.Sexes.ToList();
        }
        public IEnumerable<Speciality> GetAllSpecialities()
        {
            return this.context.Specialities.ToList();
        }
        public IEnumerable<ScienceDegree> GetAllScienceDegrees()
        {
            return this.context.ScienceDegrees.ToList();
        }
        public IEnumerable<ScienceStatus> GetAllScienceStatuses()
        {
            return this.context.ScienceStatuses.ToList();
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return this.context.Companies.ToList();
        }

        public IEnumerable<OrderStatus> GetAllOrderStatuses()
        {
            return this.context.OrderStatuses.ToList();
        }

        public IEnumerable<Rank> GetAllRanks()
        {
            return this.context.Ranks.ToList();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this.context.Users.ToList();
        }

        public IEnumerable<PaymentType> GetAllPaymentTypes()
        {
            return this.context.PaymentTypes.ToList();
        }
        #endregion

        /// <summary>
        /// Save all pending changes in this DataManager
        /// </summary>
        public void Save()
        {
            this.context.Save();
        }

        public void Rollback()
        {
            IEnumerable<object> collection = from e in context.GetObjectStateManager().GetObjectStateEntries
                                     (System.Data.EntityState.Modified | System.Data.EntityState.Deleted)
                                             select e.Entity;
            context.Refresh(RefreshMode.StoreWins, collection);


            IEnumerable<object> AddedCollection = from e in context.GetObjectStateManager().GetObjectStateEntries
                                                      (System.Data.EntityState.Added)
                                                  select e.Entity;
            foreach (object addedEntity in AddedCollection)
            {
                context.Detach(addedEntity);
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
            return this.context.CreateObject<T>();
        }

        public void RemoveObject<T>(T obj) where T : class
        {
            this.context.RemoveObject(obj);
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
            this.context.Specialities.AddObject(obj);
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
            this.context.Sexes.AddObject(obj);
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
            this.context.ScienceStatuses.AddObject(obj);
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
            this.context.ScienceDegrees.AddObject(obj);
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
            this.context.OrderStatuses.AddObject(obj);
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
            this.context.Conferences.AddObject(obj);
            Save();
        }

        public void AddContactType(ContactType obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("contactType");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.context.ContactTypes.AddObject(obj);
            Save();
        }

        public void AddUser(User obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("User");
            }
            this.context.Users.AddObject(obj);
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
            this.context.Companies.AddObject(obj);
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
            this.context.PaymentTypes.AddObject(obj);
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
            this.context.Ranks.AddObject(obj);
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
            this.context.Persons.AddObject(obj);
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
            context.AbstractStatuses.AddObject(obj);
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
            context.AbstractWorks.AddObject(obj);
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
            //var sourceId = underlyingContext.Abstracts.Select(o => o.SourceId).Max();
            //obj.SourceId = ++sourceId;
            context.Abstracts.AddObject(obj);
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

            // Проверяем, есть ли уже такая конференция у человека
            var personConference = context.PersonConferences.Where(o => o.Person.Id.Equals(person.Id) && o.Conference.Id.Equals(conference.Id)).FirstOrDefault();
            if (personConference == null)
            {
                // Если находимся в режиме конференции - надо задать участие в текущей конференции
                //if (DefaultManager.Instance.ConferenceMode)
                //{
                    personConference = CreateObject<PersonConference>();
                    personConference.PersonConferenceId = GuidComb.Generate();
                    personConference.PersonId = person.Id;
                    personConference.ConferenceId = conference.Id;
                    context.PersonConferences.AddObject(personConference);
                    Save();
                    // Если находимся в режиме регистрации - надо предзаполнить данные прибытия и оплаты
                    if (!DefaultManager.Instance.RegistrationMode)
                    {
                        personConference.PersonConferences_Detail = DefaultManager.Instance.DefaultPersonConferenceDetail(personConference.PersonConferenceId);
                        personConference.PersonConferences_Payment = DefaultManager.Instance.DefaultPersonConferencePayment(personConference.PersonConferenceId);
                    }
                    else
                    {
                        personConference.PersonConferences_Detail = DefaultManager.Instance.DefaultPersonConferenceDetailRegistration(personConference.PersonConferenceId);
                        personConference.PersonConferences_Payment = DefaultManager.Instance.DefaultPersonConferencePaymentRegistration(personConference.PersonConferenceId);
                    }
                    Save();
                //}
            }
            return personConference;

        }

        public void AddPersonConference(PersonConference obj)
        {
            obj.PersonConferences_Detail = DefaultManager.Instance.DefaultPersonConferenceDetail(obj.PersonConferenceId);
            obj.PersonConferences_Payment = DefaultManager.Instance.DefaultPersonConferencePayment(obj.PersonConferenceId);
            context.PersonConferences.AddObject(obj);
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

            this.context.Specialities.DeleteObject(obj);
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

            this.context.Sexes.DeleteObject(obj);
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

            this.context.ScienceDegrees.DeleteObject(obj);
            Save();
        }

        public void RemoveOrderStatus(OrderStatus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("scienceDegree");
            }

            this.CheckEntityBelongsToUnitOfWork(obj);
            this.context.OrderStatuses.DeleteObject(obj);
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

            this.context.ScienceStatuses.DeleteObject(obj);
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

            this.context.Conferences.DeleteObject(obj);
            Save();
        }

        public List<GeoBase> FindCityGeo(string city)
        {
            var list = new List<GeoBase>();

            list = context.GeoBases.Where(o => 
                o.CityName.ToUpper().Contains(city.ToUpper()) && 
                    (
                    o.CityTypeFull.Equals("Город") || o.CityTypeFull.Equals("Поселок городского типа") || o.CityTypeFull.Equals("Станица") || o.CityTypeFull.Equals("Станция"))).ToList();

            return list;
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
            this.context.Persons.DeleteObject(obj);
            Save();
        }

        public void RemovePersonConference(PersonConference obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("personConference");
            }

            foreach (var item in obj.Abstracts.ToList())
            {
                RemoveAbstract(item);
            }

            this.context.PersonConferences.DeleteObject(obj);
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
            this.context.Ranks.DeleteObject(obj);
            Save();
        }

        public void RemoveCompany(Company obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Company");
            }
            this.context.Companies.DeleteObject(obj);
            Save();
        }

        public void RemoveUser(User obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("User");
            }
            this.context.Users.DeleteObject(obj);
            this.CheckEntityBelongsToUnitOfWork(obj);
            Save();
        }

        public User GetUser(string name)
        {
            return this.context.Users.FirstOrDefault(o => o.Name == name);
        }


        public IEnumerable<User> GetUsers()
        {
            return this.context.Users.ToList();
        }

        public void RemovePaymentType(PaymentType obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("PaymentType");
            }
            this.context.PaymentTypes.DeleteObject(obj);
            Save();
        }
        #endregion



        #region Getting Users



        #endregion

        private void CheckEntityBelongsToUnitOfWork(object entity)
        {
            if (!this.context.IsObjectTracked(entity))
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
            if (this.context.IsObjectTracked(entity))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The supplied {0} is already part of this Unit of Work.", entity.GetType().Name));
            }
        }

        public void EraseData()
        {
            foreach (var obj in context.AbstractWorks.ToList())
            {
                context.AbstractWorks.DeleteObject(obj);
            }
            Save();
            foreach (var obj in context.Abstracts.ToList())
            {
                context.Abstracts.DeleteObject(obj);
            }
            Save();
            foreach (var obj in context.PersonConferences.ToList())
            {
                context.PersonConferences.DeleteObject(obj);
            }
            Save();
            IList<Person> persons = context.Persons.ToList();
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

            foreach (var obj in context.Conferences)
            {
                context.Conferences.DeleteObject(obj);
            }
            Save();
            foreach (var obj in context.ScienceDegrees)
            {
                context.ScienceDegrees.DeleteObject(obj);
            }
            Save();
            foreach (var obj in context.ScienceStatuses)
            {
                context.ScienceStatuses.DeleteObject(obj);
            }
            Save();
            foreach (var obj in context.Sexes)
            {
                context.Sexes.DeleteObject(obj);
            }
            Save();
            foreach (var obj in context.Specialities)
            {
                context.Specialities.DeleteObject(obj);
            }
            Save();
            foreach (var obj in context.Ranks)
            {
                context.Ranks.DeleteObject(obj);
            }
            Save();
            foreach (var obj in context.Companies)
            {
                context.Companies.DeleteObject(obj);
            }
            Save();
            foreach (var obj in context.PaymentTypes)
            {
                context.PaymentTypes.DeleteObject(obj);
            }
            foreach (var obj in context.OrderStatuses)
            {
                context.OrderStatuses.DeleteObject(obj);
            }
            foreach (var obj in context.OrderStatuses)
            {
                context.OrderStatuses.DeleteObject(obj);
            }
            foreach (var obj in context.ContactTypes)
            {
                context.ContactTypes.DeleteObject(obj);
            }
            foreach (var obj in context.AbstractStatuses)
            {
                context.AbstractStatuses.DeleteObject(obj);
            }
            foreach (var obj in context.Users)
            {
                context.Users.DeleteObject(obj);
            }
            Save();
        }

        public void FillData()
        {
            AddUser(new User { Id = GuidComb.Generate(), Name = "user", FullName = "user", Password = "user", Email = "user@example.com", Role = "user" });
            AddUser(new User { Id = GuidComb.Generate(), Name = "admin", FullName = "user", Password = "admin", Email = "admin@example.com", Role = "admin" });
            AddUser(new User { Id = GuidComb.Generate(), Name = "111", FullName = "user", Password = "111", Email = "111@example.com", Role = "user" });
            Save();

            AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0 });
            //AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "I", Name = "In work" });
            //AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "RV", Name = "Revision" });
            //AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "A", Name = "Accepted" });
            //AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "RJ", Name = "Rejected" });
            //AddAbstractStatus(new AbstractStatus { Id = GuidComb.Generate(), Code = "R2", Name = "Rejected Twice" });
            Save();

            AddContactType(new ContactType { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0 });
            AddContactType(new ContactType { Id = GuidComb.Generate(), Code = "H", Name = "Домашний", SourceId = 1 });
            AddContactType(new ContactType { Id = GuidComb.Generate(), Code = "W", Name = "Рабочий", SourceId = 2 });
            AddContactType(new ContactType { Id = GuidComb.Generate(), Code = "O", Name = "Другой", SourceId = 3 });
            AddContactType(new ContactType { Id = GuidComb.Generate(), Code = "EX", Name = "Прочее", SourceId = 4 });
            Save();

            AddOrderStatus(new OrderStatus { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0 });
            AddOrderStatus(new OrderStatus { Id = GuidComb.Generate(), Code = "О", Name = "Оплачен", SourceId = 1 });
            AddOrderStatus(new OrderStatus { Id = GuidComb.Generate(), Code = "В", Name = "Возврат", SourceId = 2 });
            Save();

            AddConference(new Conference { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0 });
            //AddConference(new Conference { Id = GuidComb.Generate(), Code = "", Name = "Conference 1" });
            //AddConference(new Conference { Id = GuidComb.Generate(), Code = "", Name = "Conference 2" });
            Save();
            AddScienceDegree(new ScienceDegree { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0 });
            //AddScienceDegree(new ScienceDegree { Id = GuidComb.Generate(), Code = "", Name = "Science Degree 1" });
            //AddScienceDegree(new ScienceDegree { Id = GuidComb.Generate(), Code = "", Name = "Science Degree 2" });
            Save();
            AddScienceStatus(new ScienceStatus { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0 });
            //AddScienceStatus(new ScienceStatus { Id = GuidComb.Generate(), Code = "", Name = "Science Status 1" });
            //AddScienceStatus(new ScienceStatus { Id = GuidComb.Generate(), Code = "", Name = "Science Status 2" });
            Save();
            AddSex(new Sex { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0 });
            AddSex(new Sex { Id = GuidComb.Generate(), Code = "М", Name = "Мужской", SourceId = 1 });
            AddSex(new Sex { Id = GuidComb.Generate(), Code = "Ж", Name = "Женский", SourceId = 2 });
            Save();
            AddSpeciality(new Speciality { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0 });
            //AddSpeciality(new Speciality { Id = GuidComb.Generate(), Code = "", Name = "Speciality 1" });
            //AddSpeciality(new Speciality { Id = GuidComb.Generate(), Code = "", Name = "Speciality 2" });
            Save();

            AddRank(new Rank { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0 });
            ////AddRank(new Rank { Id = GuidComb.Generate(), Code = "", Name = "Rank 1" });
            //AddRank(new Rank { Id = GuidComb.Generate(), Code = "", Name = "Rank 2" });
            Save();
            AddCompany(new Company { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0 });
            //AddCompany(new Company { Id = GuidComb.Generate(), Code = "", Name = "Company 1" });
            //AddCompany(new Company { Id = GuidComb.Generate(), Code = "", Name = "Company 2" });
            Save();
            AddPaymentType(new PaymentType { Id = GuidComb.Generate(), Code = "-", Name = "-", SourceId = 0 });
            //AddPaymentType(new PaymentType { Id = GuidComb.Generate(), Code = "", Name = "Payment type 1" });
            //AddPaymentType(new PaymentType { Id = GuidComb.Generate(), Code = "", Name = "Payment type 2" });
            Save();
        }


        public User GetUser(string name, string pass)
        {
            return this.context.Users.FirstOrDefault(o => o.Name == name && o.Password == pass);
        }

        public PersonConference GetPersonConference(Person person, Conference conference)
        {
            return
                this.context.PersonConferences.Where(
                    o => o.PersonId == person.Id & o.ConferenceId == conference.Id).FirstOrDefault();
        }

        public PersonConference GetPersonConferenceByID(string id)
        {
            return this.context.PersonConferences.SingleOrDefault(o => o.PersonConferenceId == new Guid(id));
        }

        public IEnumerable<PersonConference> GetPersonConferencesForPerson(Person person)
        {
            return this.context.PersonConferences.Include(o => o.Conference).Where(o => o.PersonId == person.Id).ToList();
        }

        public IEnumerable<ContactType> GetAllContactTypes()
        {
            return this.context.ContactTypes.ToList();
        }

        public void RemoveContactType(ContactType obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("contactType");
            }

            this.CheckEntityBelongsToUnitOfWork(obj);
            this.context.ContactTypes.DeleteObject(obj);
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

            this.context.Emails.AddObject(email);
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
            this.context.Emails.DeleteObject(email);
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

            this.context.Addresses.AddObject(address);
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
            this.context.Addresses.DeleteObject(address);
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

            this.context.Phones.AddObject(phone);
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
            this.context.Phones.DeleteObject(phone);
            Save();
        }

        public IEnumerable<AbstractStatus> GetAllAbstractStatuses()
        {
            return this.context.AbstractStatuses.ToList();
        }

        public IEnumerable<Abstract> GetAllAbstracts()
        {
            return this.context.Abstracts
                .Include(o => o.AbstractWorks)
                .Include(o => o.PersonConference.Person).ToList();
        }



        public Conference GetDefaultConference()
        {
            return this.context.Conferences.SingleOrDefault(o => o.Code == "-");
        }

        public void AddAbstractToPerson(Person person, Abstract abs)
        {
            var personConference =
                this.context.PersonConferences.FirstOrDefault(
                    o => o.PersonId == person.Id && o.ConferenceId == DefaultManager.Instance.DefaultConference.Id);
            if (personConference == null)
            {
                personConference = new PersonConference
                {
                    PersonConferenceId = GuidComb.Generate(),
                    PersonId = person.Id,
                    ConferenceId = DefaultManager.Instance.DefaultConference.Id
                };
                this.context.PersonConferences.AddObject(personConference);
                Save();
            }

            abs.PersonConferenceId = personConference.PersonConferenceId;
            this.context.Abstracts.AddObject(abs);
            Save();
        }

        public void SetPersonConferenceAbstractOn(PersonConference personConference)
        {
            // TODO - Сделать чтобы в PersonConferenceDetail проставлялся флажок IsAbstract
        }

        public IEnumerable<Abstract> GetAbstractsForPerson(Person person)
        {
            return this.context.Abstracts.Where(a => a.PersonConference.PersonId == person.Id).ToList();
        }

        public Person GetPersonByPersonConferenceId(Guid id)
        {
            return this.context.PersonConferences.FirstOrDefault(o => o.PersonConferenceId == id).Person;

        }

        public IEnumerable<Email> GetEmailsByPersonId(Guid id)
        {
            return this.context.Emails.Where(o => o.PersonId == id).ToList();
        }

        public void AddAbstractWorkToAbstract(Abstract currentAbstract, AbstractWork abstractWork)
        {
            abstractWork.AbstractId = currentAbstract.Id;
            this.context.AbstractWorks.AddObject(abstractWork);
            Save();

        }

        public void RemoveAbstractWork(AbstractWork abstractWork)
        {
            if (abstractWork == null)
            {
                throw new ArgumentNullException("abstractWork");
            }
            this.context.AbstractWorks.DeleteObject(abstractWork);
        }

        public void RemoveAbstract(Abstract p)
        {
            if (p.AbstractWorks != null)
            {
                foreach (var item in p.AbstractWorks.ToList())
                {
                    this.context.AbstractWorks.DeleteObject(item);
                }
            }
            this.context.Abstracts.DeleteObject(p);
            Save();
        }

        public Sex GetSexBySourceId(int id)
        {
            var obj = context.Sexes.FirstOrDefault(o => o.SourceId == id);
            return obj ?? DefaultManager.Instance.DefaultSex;
        }

        public Speciality GetSpecialityBySourceId(int id)
        {
            var obj = context.Specialities.FirstOrDefault(o => o.SourceId == id);
            return obj ?? DefaultManager.Instance.DefaultSpeciality;
        }

        public ScienceDegree GetScienceDegreeBySourceID(int id)
        {
            var obj = context.ScienceDegrees.FirstOrDefault(o => o.SourceId == id);
            return obj ?? DefaultManager.Instance.DefaultScienceDegree;
        }

        public ScienceStatus GetScienceStatusBySourceId(int id)
        {
            var obj = context.ScienceStatuses.FirstOrDefault(o => o.SourceId == id);
            return obj ?? DefaultManager.Instance.DefaultScienceStatus;
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
            var obj = context.Persons.FirstOrDefault(o => o.SourceId == id);
            return obj;
        }

        public Conference GetConferenceBySourceId(int id)
        {
            return context.Conferences.FirstOrDefault(o => o.SourceId == id);

        }

        public Conference GetConferenceById(Guid id)
        {
            return context.Conferences.FirstOrDefault(o => o.Id == id);
        }

        public Conference GetConferenceByName(string name)
        {
            return context.Conferences.FirstOrDefault(o => o.Name == name);
        }

        public PersonConference GetPersonConferenceBySourceID(int p)
        {
            var obj = context.PersonConferences.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public Rank GetRankBySourceId(int p)
        {
            var obj = context.Ranks.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public Company GetCompanyBySourceId(int p)
        {
            var obj = context.Companies.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public void AddPersonConferenceDetail(PersonConferences_Detail obj)
        {
            var personConference =
                context.PersonConferences.FirstOrDefault(o => o.PersonConferenceId == obj.PersonConferenceId);
            personConference.PersonConferences_Detail = obj;
            context.PersonConferences.Attach(personConference);
            Save();
        }

        public void AddPersonConferenceMoney(PersonConferences_Payment obj)
        {
            var personConference =
                context.PersonConferences.FirstOrDefault(o => o.PersonConferenceId == obj.PersonConferenceId);
            personConference.PersonConferences_Payment = obj;
            context.PersonConferences.Attach(personConference);
            Save();
        }

        public PaymentType GetPaymentTypeBySourceId(int p)
        {
            var obj = context.PaymentTypes.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public OrderStatus GetOrderStatusBySourceId(int p)
        {
            var obj = context.OrderStatuses.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public AbstractStatus GetAbstractStatusBySourceId(int p)
        {
            var obj = context.AbstractStatuses.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }



        public Abstract GetAbstractBySourceId(int p)
        {
            var obj = context.Abstracts.FirstOrDefault((o => o.SourceId == p));
            return obj;

        }

        public User GetUserBySourceId(int p)
        {
            var obj = context.Users.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public ContactType GetContactTypeBySourceId(int p)
        {
            var obj = context.ContactTypes.FirstOrDefault(o => o.SourceId == p);
            return obj;
        }

        public List<PersonConference> GetConferenceForPersonRegistered(Guid guid)
        {
            return context.PersonConferences
                .Where(o => o.PersonId == guid)
                //       .Include(o => o.PersonConferences_Detail)

                .ToList();
        }

        public List<PersonConference> GetConferenceForPersonArrived(Guid id)
        {
            return context.PersonConferences
                .Where(o => o.PersonId == id && o.PersonConferences_Detail.IsArrive == true)
                //     .Include(o => o.PersonConferences_Detail)
                .ToList();
        }



        public List<Abstract> GetAbstractsByPersonConferenceID(Guid id)
        {
            return context.Abstracts.Where(o => o.PersonConferenceId == id).OrderBy(o => o.DateAdd).ToList();
        }

        public Propertie GetPropertyValue(string name)
        {
            return context.Properties.SingleOrDefault(o => o.Name == name);
        }

        public void AddPropertie(Propertie obj)
        {
            context.Properties.AddObject(obj);
            Save();
        }



        public List<Abstract> GetAbstractsForPosterSession()
        {

            var lst = context.Abstracts.Where(o => o.PersonConference.ConferenceId == DefaultManager.Instance.DefaultConference.Id).ToList();
            return lst.Where(abs => abs.LastState == "Принят").ToList();
        }

        public List<Abstract> GetAllAbstractsForConference(Guid guid)
        {
            return context.Abstracts.Where(o => o.PersonConference.ConferenceId == guid).ToList();
        }

        public IEnumerable<BadgeType> GetAllBadges()
        {
            return context.BadgeTypes.OrderBy(o => o.Name);
        }

        public void AddElementToBadge(BadgeType badgeType, Badge element)
        {
            element.BadgeTypeId = badgeType.Id;
            this.context.Badges.AddObject(element);
            Save();
        }

        public void RemoveBadgeElement(Badge badge)
        {
            if (badge == null)
            {
                throw new ArgumentNullException("badgeelement");
            }
            this.context.Badges.DeleteObject(badge);
        }

        public void AddBadge(BadgeType element)
        {
            this.context.BadgeTypes.AddObject(element);
            Save();
        }



        public void RemoveBadge(BadgeType badgeType)
        {
            if (badgeType == null)
            {
                throw new ArgumentException("badgeType");
            }
            this.context.BadgeTypes.DeleteObject(badgeType);
            Save();
        }

        public IEnumerable<BadgesDefault> GetAllBadgesDefaults()
        {
            return context.BadgesDefaults.OrderBy(b => b.Rank.Name);
        }

        public void AddBadgesDefault(BadgesDefault obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("bdgesdefault");
            }
            //  obj.DateAdd = DateTime.Now;
            // obj.DateUpdate = DateTime.Now;
            // var currentUser = Authentification.GetCurrentUser();
            // obj.User = currentUser == null ? "-" : currentUser.Name;
            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.context.BadgesDefaults.AddObject(obj);
            Save();
        }

        public void DeleteBadgesDefault(BadgesDefault BadgeDefaultSelected)
        {
            if (BadgeDefaultSelected == null)
            {
                throw new ArgumentNullException("BadgeDefaultSelected");
            }
            this.context.BadgesDefaults.DeleteObject(BadgeDefaultSelected);
        }

        public BadgeType GetBadgeForRank(Rank rank)
        {
            var obj = DefaultManager.Instance.DefaultBadge;
            if (rank != null)
            {
                var b = context.BadgesDefaults.Where(bd => bd.RankId == rank.Id).FirstOrDefault();
                if (b != null)
                {
                    obj = context.BadgeTypes.Where(o => o.Id == b.Id).FirstOrDefault();
                }
            }
            return obj;
        }

        public BadgeType GetBadgeByID(Guid guid)
        {
            return this.context.BadgeTypes.Where(b => b.Id == guid).FirstOrDefault();
        }
        public BadgeType GetBadgeByName(string name)
        {
            return this.context.BadgeTypes.Where(b => b.Name == name).FirstOrDefault();
        }

        public IEnumerable<Printer> GetPrinters()
        {
            return context.Printers;
        }

        public void RemovePrinter(Printer obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("printer");
            }

            this.CheckEntityBelongsToUnitOfWork(obj);

            this.context.Printers.DeleteObject(obj);
            Save();
        }

        public Printer AddPrinter(Printer obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Printer");
            }
            this.context.Printers.AddObject(obj);
            Save();
            return obj;
        }

        public string GetPrinter(string docType)
        {
            var obj = context.Printers
                .Where(o => o.ComputerName == DefaultManager.Instance.ComputerName
                && o.DocumentName == docType).FirstOrDefault();
            if (obj != null)
            {
                return obj.PrinterName;
            }
            else
            {
                return DefaultManager.Instance.Printer;
            }
        }



        public List<ItemGuidName> GetAllRanksItem()
        {
            return context.Ranks.OrderBy(r => r.Name).Select(o => new ItemGuidName { Id = o.Id, Name = o.Name }).ToList();
        }

        public List<ItemGuidName> GetAllBadgesItem()
        {
            return context.BadgeTypes.OrderBy(r => r.Name).Select(o => new ItemGuidName { Id = o.Id, Name = o.Name }).ToList();
        }



        public List<PersonConference> GetPersonConferenceForPrint(ItemGuidName ConferenceSelected, ItemGuidName RankSelected, ItemIntName PrintedSelected, ItemIntName PaidSelected)
        {
            if (ConferenceSelected != null && RankSelected != null && PrintedSelected != null && PaidSelected != null)
            {
                var printed = false;
                if (PrintedSelected.Id == 1) { printed = true; }

                if (PaidSelected.Id == 1)
                {
                    return context.PersonConferences.Where(c =>
                    c.ConferenceId == ConferenceSelected.Id
                    && c.PersonConferences_Detail.RankId == RankSelected.Id
                    && c.PersonConferences_Detail.IsBadge == printed
                    && c.PersonConferences_Payment.Money > 0).ToList();
                }
                else
                {
                    return context.PersonConferences.Where(c =>
                    c.ConferenceId == ConferenceSelected.Id
                    && c.PersonConferences_Detail.RankId == RankSelected.Id
                    && c.PersonConferences_Detail.IsBadge == printed
                    && c.PersonConferences_Payment.Money == 0).ToList();
                }
            }
            else
            {
                return new List<PersonConference>();
            }
        }

        public void SetPersonConferencePrinted(PersonConference personConference)
        {
            personConference.PersonConferences_Detail.IsBadge = true;
            context.PersonConferences.Attach(personConference);
            context.Save();
        }

        public int GetOrderNumber(PersonConference CurrentPersonConference)
        {
            var orderNumber = 0;
            var on = context.PersonConferences
                .Where(p => p.ConferenceId == CurrentPersonConference.ConferenceId)
                .Select(o => o.PersonConferences_Payment.OrderNumber).Max();
            if (on > 0)
            {
                orderNumber = on;
            }
            orderNumber++;
            return orderNumber;
        }
    }
}
