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
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The underlying context tracking changes
        /// </summary>
        private IConferenceContext underlyingContext;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// Changes registered in the UnitOfWork are recorded in the supplied context
        /// </summary>
        /// <param name="context">The underlying context for this UnitOfWork</param>
        ///   public UnitOfWork(IEmployeeContext context)
        public UnitOfWork(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.underlyingContext = context;
        }

       

        /// <summary>
        /// Save all pending changes in this UnitOfWork
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


        /// <summary>
        /// Registers the addition of a new speciality
        /// </summary>
        /// <param name="speciality">The speciality to add</param>
        /// <exception cref="InvalidOperationException">Thrown if speciality is already added to UnitOfWork</exception>
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
        /// <summary>
        /// Registers the addition of a new conference
        /// </summary>
        /// <param name="employee">The person to add</param>
        /// <exception cref="InvalidOperationException">Thrown if person is already added to UnitOfWork</exception>
        public void AddPerson(Person obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("person");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(obj);
            this.underlyingContext.Persons.AddObject(obj);
        }

        /// <summary>
        /// Registers the removal of an existing speciality
        /// </summary>
        /// <param name="speciality">The speciality to remove</param>
        /// <exception cref="InvalidOperationException">Thrown if speciality is not tracked by this UnitOfWork</exception>
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
        /// <summary>
        /// Registers the removal of an existing person
        /// </summary>
        /// <param name="employee">The person to remove</param>
        /// <exception cref="InvalidOperationException">Thrown if person is not tracked by this UnitOfWork</exception>
        public void RemovePerson(Person obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("person");
            }

            this.CheckEntityBelongsToUnitOfWork(obj);
            this.underlyingContext.Persons.DeleteObject(obj);
        }

        /// <summary>
        /// Verifies that the specified entity is tracked in this UnitOfWork
        /// </summary>
        /// <param name="entity">The object to search for</param>
        /// <exception cref="InvalidOperationException">Thrown if object is not tracked by this UnitOfWork</exception>
        private void CheckEntityBelongsToUnitOfWork(object entity)
        {
            if (!this.underlyingContext.IsObjectTracked(entity))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The supplied {0} is not part of this Unit of Work.", entity.GetType().Name));
            }
        }

        /// <summary>
        /// Verifies that the specified entity is not tracked in this UnitOfWork
        /// </summary>
        /// <param name="entity">The object to search for</param>
        /// <exception cref="InvalidOperationException">Thrown if object is tracked by this UnitOfWork</exception>
        private void CheckEntityDoesNotBelongToUnitOfWork(object entity)
        {
            if (this.underlyingContext.IsObjectTracked(entity))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The supplied {0} is already part of this Unit of Work.", entity.GetType().Name));
            }
        }
    }
}
