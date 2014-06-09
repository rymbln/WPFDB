using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class SpecialityRepository :ISpecialityRepository
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

        /// <summary>
        /// All specialities
        /// </summary>
        /// <returns>Enumerable of all specialities</returns>
        public IEnumerable<Speciality> GetAllSpecialities()
        {
            // NOTE: Some points considered during implementation of data access methods:
            //    -  ToList is used to ensure any data access related exceptions are thrown
            //       during execution of this method rather than when the data is enumerated.
            //    -  Returning IEnumerable rather than IQueryable ensures the repository has full control
            //       over how data is retrieved from the store, returning IQueryable would allow consumers
            //       to add additional operators and affect the query sent to the store.
            return this.objectSet.ToList();
        }
    }

}
