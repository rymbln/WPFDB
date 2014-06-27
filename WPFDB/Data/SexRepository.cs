using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class SexRepository
    {
        /// <summary>
        /// Underlying ObjectSet to retrieve data from
        /// </summary>
        private IObjectSet<Sex> objectSet;

        /// <summary>
        /// Initializes a new instance of the SexRepository class.
        /// </summary>
        /// <param name="context">Context to retrieve data from</param>
        public SexRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context is null");
            }

            this.objectSet = context.Sexes;
        }

        public IEnumerable<Sex> All()
        {
            return objectSet.ToList();
        }

        public void Add(Sex obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("sex");
            }
            objectSet.AddObject(obj);


        }

        public void Remove(Sex obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("sex");
            }
            objectSet.DeleteObject(obj);
            //this.CheckEntityBelongsToUnitOfWork(obj);
            //this.underlyingContext.Persons.DeleteObject(obj);
        }

        public Sex GetByName(string name)
        {

            if (name == null)
            {
                throw new ArgumentNullException("PaymentType");
            }
            return objectSet.FirstOrDefault(o => o.Name == name);
        }
    }
}
