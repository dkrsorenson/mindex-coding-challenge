using System;

using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        /// <summary>
        /// Create a new compensation record
        /// </summary>
        /// <param name="compensation">The compensation to create</param>
        /// <returns>The compensation record that was created</returns>
        Compensation Create(Compensation compensation);

        /// <summary>
        /// Get the compensation for an employee by their Id
        /// </summary>
        /// <param name="id">The employee Id</param>
        /// <returns>The compensation</returns>
        Compensation GetByEmployeeId(String id);
    }
}
