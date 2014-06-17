using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class PersonRepository : IPersonRepository
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

        public IEnumerable<Person> GetAllPersons()
        {
            return objectSet.ToList();
        }
    }
}
