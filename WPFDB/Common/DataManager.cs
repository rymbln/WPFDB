﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private PersonRepository personRepository ;
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

        public void FillData()
        {
            AddConference(new Conference { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddConference(new Conference { Id = GuidComb.Generate(), Code = "", Name = "Conference 1" });
            AddConference(new Conference { Id = GuidComb.Generate(), Code = "", Name = "Conference 2" });

            AddScienceDegree(new ScienceDegree { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddScienceDegree(new ScienceDegree { Id = GuidComb.Generate(), Code = "", Name = "Science Degree 1" });
            AddScienceDegree(new ScienceDegree { Id = GuidComb.Generate(), Code = "", Name = "Science Degree 2" });

            AddScienceStatus(new ScienceStatus { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddScienceStatus(new ScienceStatus { Id = GuidComb.Generate(), Code = "", Name = "Science Status 1" });
            AddScienceStatus(new ScienceStatus { Id = GuidComb.Generate(), Code = "", Name = "Science Status 2" });

            AddSex(new Sex { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddSex(new Sex { Id = GuidComb.Generate(), Code = "", Name = "Sex 1" });
            AddSex(new Sex { Id = GuidComb.Generate(), Code = "", Name = "Sex 2" });

            AddSpeciality(new Speciality { Id = GuidComb.Generate(), Code = "-", Name = "-" });
            AddSpeciality(new Speciality { Id = GuidComb.Generate(), Code = "", Name = "Speciality 1" });
            AddSpeciality(new Speciality { Id = GuidComb.Generate(), Code = "", Name = "Speciality 2" });


            AddRank(new Rank {Id = GuidComb.Generate(), Code = "-", Name = "-"});
            AddRank(new Rank { Id = GuidComb.Generate(), Code = "", Name = "Rank 1" });
            AddRank(new Rank { Id = GuidComb.Generate(), Code = "", Name = "Rank 2" });

            AddCompany(new Company { Id = GuidComb.Generate(), Code = "-" , Name = "-"});
            AddCompany(new Company { Id = GuidComb.Generate(), Code = "", Name = "Company 1" });
            AddCompany(new Company { Id = GuidComb.Generate(), Code = "", Name = "Company 2" });

            AddPaymentType(new PaymentType {Id = GuidComb.Generate(), Code = "-", Name = "-"});
            AddPaymentType(new PaymentType { Id = GuidComb.Generate(), Code = "", Name = "Payment type 1" });
            AddPaymentType(new PaymentType { Id = GuidComb.Generate(), Code = "", Name = "Payment type 2" });

            AddUser(new User {Id = GuidComb.Generate(), Name = "user", Password = "user", Role="user"});
            AddUser(new User { Id = GuidComb.Generate(), Name = "admin", Password = "admin", Role = "admin" });
            AddUser(new User { Id = GuidComb.Generate(), Name = "111", Password = "111", Role = "user" });


            Save();
        }

        public User GetUser(string name, string password)
        {
            return userRepository.GetUser(name, password);
        }
    }
}
