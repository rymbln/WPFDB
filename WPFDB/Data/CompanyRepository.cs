using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
    public class CompanyRepository
    {
               private IObjectSet<Company> objectSet;

        public CompanyRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context is null");
            }
            this.objectSet = context.Companies;
        }

        public IEnumerable<Company> All()
        {
            return objectSet.ToList();
        }

        public void Add(Company obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Company");
            }
            objectSet.AddObject(obj);


        }

        public void Remove(Company obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Company");
            }
            objectSet.DeleteObject(obj);
            //this.CheckEntityBelongsToUnitOfWork(obj);
            //this.underlyingContext.Persons.DeleteObject(obj);
        }
    }
}
