

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
        IObjectSet<Person> Persons { get; }

        /// <summary>
        /// Gets Specialities in the data context
        /// </summary>
        IObjectSet<Speciality> Specialities { get; }

        IObjectSet<Sex> Sexes { get; }

        IObjectSet<ScienceDegree> ScienceDegrees { get; }

        IObjectSet<ScienceStatus> ScienceStatuses { get; }

        IObjectSet<Conference>  Conferences { get; }
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
