using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class ScienceDegreeRepository
    {
        private IObjectSet<ScienceDegree> objectSet;

        public ScienceDegreeRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context is null");
            }
            this.objectSet = context.ScienceDegrees;
        }

        public IEnumerable<ScienceDegree> All()
        {
            return objectSet.ToList();
        }

        public void Add(ScienceDegree obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("ScienceDegree");
            }
            objectSet.AddObject(obj);


        }

        public void Remove(ScienceDegree obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("ScienceDegree");
            }
            objectSet.DeleteObject(obj);
            //this.CheckEntityBelongsToUnitOfWork(obj);
            //this.underlyingContext.Persons.DeleteObject(obj);
        }
    }
}
