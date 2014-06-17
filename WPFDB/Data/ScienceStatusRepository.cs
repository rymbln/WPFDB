using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class ScienceStatusRepository
    {
        private IObjectSet<ScienceStatus> objectSet;

        public ScienceStatusRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context is null");
            }

            this.objectSet = context.ScienceStatuses;
        }

        public IEnumerable<ScienceStatus> All()
        {
            return objectSet.ToList();
        }

        public void Add(ScienceStatus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("ScienceStatus");
            }
            objectSet.AddObject(obj);


        }

        public void Remove(ScienceStatus obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("ScienceStatus");
            }
            objectSet.DeleteObject(obj);
            //this.CheckEntityBelongsToUnitOfWork(obj);
            //this.underlyingContext.Persons.DeleteObject(obj);
        }
    }
}
