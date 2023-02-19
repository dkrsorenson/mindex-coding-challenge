using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        /// <summary>
        /// Add new compensation record to the Compensations DB set
        /// </summary>
        /// <param name="compensation">The compensation record to add</param>
        /// <returns>The compensation record that was added</returns>
        Compensation Add(Compensation employee);

        /// <summary>
        /// Gets the compensation record by employee Id
        /// </summary>
        /// <param name="employeeId">The employee Id</param>
        /// <returns>The compensation record</returns>
        Compensation GetByEmployeeId(String employeeId);

        Task SaveAsync();
    }
}