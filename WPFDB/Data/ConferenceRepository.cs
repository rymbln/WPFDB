using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class ConferenceRepository
    {
        private IObjectSet<Conference> objectSet;

        public ConferenceRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context is null");
            }

            this.objectSet = context.Conferences;
        }
        public IEnumerable<Conference> All()
        {
            return objectSet.ToList();
        }

        public void Add(Conference obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Conference");
            }
            objectSet.AddObject(obj);


        }

        public void Remove(Conference obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Conference");
            }
            objectSet.DeleteObject(obj);
            //this.CheckEntityBelongsToUnitOfWork(obj);
            //this.underlyingContext.Persons.DeleteObject(obj);
        }

        public Conference GetByName(string name)
        {

            if (name == null)
            {
                throw new ArgumentNullException("PaymentType");
            }
            return objectSet.FirstOrDefault(o => o.Name == name);
        }
    }
}
