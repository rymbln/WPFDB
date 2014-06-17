using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class SpecialityRepository
    {
        /// <summary>
        /// Underlying ObjectSet to retrieve data from
        /// </summary>
        private IObjectSet<Speciality> objectSet;

        /// <summary>
        /// Initializes a new instance of the SpecialityRepository class.
        /// </summary>
        /// <param name="context">Context to retrieve data from</param>
        public SpecialityRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context is null");
            }

            this.objectSet = context.Specialities;
        }

        public IEnumerable<Speciality> All()
        {
            return objectSet.ToList();
        }

        public void Add(Speciality obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Speciality");
            }
            objectSet.AddObject(obj);


        }

        public void Remove(Speciality obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Speciality");
            }
            objectSet.DeleteObject(obj);
            //this.CheckEntityBelongsToUnitOfWork(obj);
            //this.underlyingContext.Persons.DeleteObject(obj);
        }
    }

}
