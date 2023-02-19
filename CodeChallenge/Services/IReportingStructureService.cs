using System;

using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface IReportingStructureService
    {
        /// <summary>
        /// Get the reporting structure for an employee by their Id
        /// </summary>
        /// <param name="employeeId">The employee Id</param>
        /// <returns>The reporting structure for the employee</returns>
        ReportingStructure GetByEmployeeId(String employeeId);
    }
}
