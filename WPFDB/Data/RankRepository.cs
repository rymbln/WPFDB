using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class RankRepository
    {
               private IObjectSet<Rank> objectSet;

        public RankRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context is null");
            }
            this.objectSet = context.Ranks;
        }

        public IEnumerable<Rank> All()
        {
            return objectSet.ToList();
        }

        public void Add(Rank obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Rank");
            }
            objectSet.AddObject(obj);


        }

        public void Remove(Rank obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Rank");
            }
            objectSet.DeleteObject(obj);
            //this.CheckEntityBelongsToUnitOfWork(obj);
            //this.underlyingContext.Persons.DeleteObject(obj);
        }

        public Rank GetByName(string name)
        {

            if (name == null)
            {
                throw new ArgumentNullException("PaymentType");
            }
           return  objectSet.FirstOrDefault(o => o.Name == name);
        }
    }
}
