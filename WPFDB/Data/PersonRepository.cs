using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class PersonRepository
    {
        private IObjectSet<Person> objectSet;

        public PersonRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.objectSet = context.Persons;
        }

        public IEnumerable<Person> All()
        {
            return objectSet.ToList();
        }

        public void Add(Person obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("person");
            }
            objectSet.AddObject(obj);


        }

        public void Remove(Person obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("person");
            }
            objectSet.DeleteObject(obj);
            //this.CheckEntityBelongsToUnitOfWork(obj);
            //this.underlyingContext.Persons.DeleteObject(obj);
        }
    }
}
