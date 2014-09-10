

using System.Collections.Generic;

namespace WPFDB.Data
{
    using System;
    using System.Data.Objects;
    using WPFDB.Model;

    public interface IConferenceContext: IDisposable 
    {

        ObjectSet<Person> Persons { get; }

        ObjectSet<Speciality> Specialities { get; }

        ObjectSet<Sex> Sexes { get; }

        ObjectSet<ScienceDegree> ScienceDegrees { get; }

        ObjectSet<ScienceStatus> ScienceStatuses { get; }

        ObjectSet<Conference>  Conferences { get; }

        ObjectSet<User> Users { get;  }

        ObjectSet<PaymentType> PaymentTypes { get; } 

        ObjectSet<Company> Companies { get; }

        ObjectSet<Rank> Ranks { get; } 

        ObjectSet<PersonConference> PersonConferences { get; }

        ObjectSet<OrderStatus> OrderStatuses { get; } 

        ObjectSet<ContactType> ContactTypes { get; } 

        ObjectSet<Email>  Emails { get; }

        ObjectSet<Address>  Addresses { get; }

        ObjectSet<Phone>  Phones { get; }

        ObjectSet<Abstract>  Abstracts { get; }

        ObjectSet<AbstractStatus> AbstractStatuses { get;  } 

        ObjectSet<AbstractWork>  AbstractWorks { get;  }

        ObjectSet<Propertie>  Properties { get; }

        //ObjectSet<PersonConferences_Detail> PersonConferenceDetails { get; }

        //ObjectSet<PersonConferences_Payment> PersonConferencePayments { get; }  

        /// <summary>
        /// Save all pending changes to the data context
        /// </summary>
        void Save();

        /// <summary>
        /// Creates a new instance of the specified object type
        /// NOTE: This pattern is used to allow the use of change tracking proxies
        ///       when running against the Entity Framework.
        /// </summary>
        /// <typeparam name="T">The type to create</typeparam>
        /// <returns>The newly created object</returns>
        T CreateObject<T>() where T : class;
        void RemoveObject<T>(T obj) where T:class;

        /// <summary>
        /// Checks if the supplied object is tracked in this data context
        /// </summary>
        /// <param name="obj">The object to check for</param>
        /// <returns>True if the object is tracked, false otherwise</returns>
        bool IsObjectTracked(object obj);

        ObjectStateManager GetObjectStateManager();

        void Refresh(RefreshMode refreshMode, IEnumerable<object> collection);
        void Detach(object obj);

    }
    
}
