using System;
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

        private PersonRepository personRepository;
        private ConferenceRepository conferenceRepository;
        private SexRepository sexRepository;
        private SpecialityRepository specialityRepository;
        private ScienceDegreeRepository scienceDegreeRepository;
        private ScienceStatusRepository scienceStatusRepository;

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
        #endregion

        public User GetUser(string name)
        {
            return this.underlyingContext.Users.FirstOrDefault(o => o.Name == name);
        }

        public User GetUser(string name, string pass)
        {
            return this.underlyingContext.Users.FirstOrDefault(o => o.Name == name && o.Password == pass);
        }

        public IEnumerable<User> GetUsers()
        {
            return this.underlyingContext.Users.ToList();
        }

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
    }
}
