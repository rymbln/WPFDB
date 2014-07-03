using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Common;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class PersonRepository
    {
        private IObjectSet<Person> objectSet;
        private IConferenceContext context;

        public PersonRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.context = context;
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

        public void AddConferenceInfoToPerson(Person person, Conference conference, PersonConferences_Detail detail, PersonConferences_Payment payment)
        {
            var personConferenceTypeId = GuidComb.Generate();
            var personConference = new PersonConference {PersonConferenceId = personConferenceTypeId, Conference = conference, Person = person};
            //detail.PersonConferenceId = personConferenceTypeId;
            //payment.PersonConferenceId = personConferenceTypeId;
            personConference.PersonConferences_Detail = detail;
            personConference.PersonConferences_Payment = payment;
            context.PersonConferences.AddObject(personConference);

            
        }

        
    }
}
