using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDB.Data;

namespace WPFDB.Model
{


    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class ConferenceEntities : ObjectContext, IConferenceContext
    {

        //public ObjectSet<Detail> PersonConferenceDetails
        //{
        //    get
        //    {
        //        if ((_PersonConferenceDetails == null))
        //        {
        //            _PersonConferenceDetails = base.CreateObjectSet<Detail>("Details");
        //        }
        //        return _PersonConferenceDetails;
        //    }
        //}
        //private ObjectSet<Detail> _PersonConferenceDetails;

        //public ObjectSet<Payment> PersonConferencePayments
        //{
        //    get
        //    {
        //        if ((_PersonConferencePayments == null))
        //        {
        //            _PersonConferencePayments = base.CreateObjectSet<Payment>("Payments");
        //        }
        //        return _PersonConferencePayments;
        //    }
        //}
        //private ObjectSet<Payment> _PersonConferencePayments;

        public void Save()
        {
            this.SaveChanges();
        }

        /// <summary>
        /// Checks if the supplied object is tracked in this data context
        /// </summary>
        /// <param name="obj">The object to check for</param>
        /// <returns>True if the object is tracked, false otherwise</returns>
        public bool IsObjectTracked(object entity)
        {
            ObjectStateEntry ose;
            return this.ObjectStateManager.TryGetObjectStateEntry(entity, out ose);
        }
    }
}
