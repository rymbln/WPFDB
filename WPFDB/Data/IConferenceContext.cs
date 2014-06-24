

namespace WPFDB.Data
{
    using System;
    using System.Data.Objects;
    using WPFDB.Model;
    /// <summary>
    /// Data context containing data for the Conference model
    /// </summary>
    public interface IConferenceContext: IDisposable 
    {
        /// <summary>
        /// Gets Persons in the data context
        /// </summary>
        ObjectSet<Person> Persons { get; }

        /// <summary>
        /// Gets Specialities in the data context
        /// </summary>
        ObjectSet<Speciality> Specialities { get; }

        ObjectSet<Sex> Sexes { get; }

        ObjectSet<ScienceDegree> ScienceDegrees { get; }

        ObjectSet<ScienceStatus> ScienceStatuses { get; }

        ObjectSet<Conference>  Conferences { get; }

        ObjectSet<User> Users { get;  }
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

        /// <summary>
        /// Checks if the supplied object is tracked in this data context
        /// </summary>
        /// <param name="obj">The object to check for</param>
        /// <returns>True if the object is tracked, false otherwise</returns>
        bool IsObjectTracked(object obj);
    }
    
}
