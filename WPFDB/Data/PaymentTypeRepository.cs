using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Model;

namespace WPFDB.Data
{
   public  class PaymentTypeRepository
    {
              private IObjectSet<PaymentType> objectSet;

        public PaymentTypeRepository(IConferenceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context is null");
            }
            this.objectSet = context.PaymentTypes;
        }

        public IEnumerable<PaymentType> All()
        {
            return objectSet.ToList();
        }

        public void Add(PaymentType obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("PaymentType");
            }
            objectSet.AddObject(obj);


        }

        public void Remove(PaymentType obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("PaymentType");
            }
            objectSet.DeleteObject(obj);
            //this.CheckEntityBelongsToUnitOfWork(obj);
            //this.underlyingContext.Persons.DeleteObject(obj);
        }
    }
}
