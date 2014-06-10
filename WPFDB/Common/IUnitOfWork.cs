using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDB.Common
{
    using System;
    using WPFDB.Model;

    /// <summary>
    /// Encapsulates changes to underlying data
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save all pending changes in this UnitOfWork
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
        /// Registers the addition of a new department
        /// </summary>
        /// <param name="department">The department to add</param>
        /// <exception cref="InvalidOperationException">Thrown if department is already added to UnitOfWork</exception>
        void AddSpeciality(Speciality speciality);
        void AddSex(Sex sex);
        void AddScienceDegree(ScienceDegree scienceDegree);
        void AddScienceStatus(ScienceStatus scienceStatus);
        void AddConference(Conference conference);


        /// <summary>
        /// Registers the addition of a new employee
        /// </summary>
        /// <param name="employee">The employee to add</param>
        /// <exception cref="InvalidOperationException">Thrown if employee is already added to UnitOfWork</exception>
        void AddPerson(Person person);

        /// <summary>
        /// Registers the addition of a new contact detail
        /// </summary>
        /// <param name="employee">The employee to add the contact detail to</param>
        /// <param name="detail">The contact detail to add</param>
        /// <exception cref="InvalidOperationException">Thrown if employee is not tracked by this UnitOfWork</exception>
        /// <exception cref="InvalidOperationException">Thrown if contact detail is already added to UnitOfWork</exception>
        // void AddContactDetail(Employee employee, ContactDetail detail);

        /// <summary>
        /// Registers the removal of an existing department
        /// </summary>
        /// <param name="department">The department to remove</param>
        /// <exception cref="InvalidOperationException">Thrown if department is not tracked by this UnitOfWork</exception>
        void RemoveSpeciality(Speciality speciality);
        void RemoveSex(Sex sex);
        void RemoveScienceDegree(ScienceDegree scienceDegree);
        void RemoveScienceStatus(ScienceStatus scienceStatus);
        void RemoveConference(Conference conference);
        /// <summary>
        /// Registers the removal of an existing employee
        /// </summary>
        /// <param name="employee">The employee to remove</param>
        /// <exception cref="InvalidOperationException">Thrown if employee is not tracked by this UnitOfWork</exception>
        void RemovePerson(Person person);

        /// <summary>
        /// Registers the removal of an existing contact detail
        /// </summary>
        /// <param name="employee">The employee to remove the contact detail from</param>
        /// <param name="detail">The contact detail to remove</param>
        /// <exception cref="InvalidOperationException">Thrown if employee is not tracked by this UnitOfWork</exception>
        /// <exception cref="InvalidOperationException">Thrown if contact detail is not tracked by this UnitOfWork</exception>
        //void RemoveContactDetail(Employee employee, ContactDetail detail);
    }
}
