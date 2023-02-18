using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        /// <summary>
        /// Get the reporting structure for an employee by their Id
        /// </summary>
        /// <param name="employeeId">The employee Id</param>
        /// <returns>The reporting structure for the employee</returns>
        public ReportingStructure GetByEmployeeId(string employeeId)
        {
            Employee employee = _employeeService.GetById(employeeId, false);

            if (employee == null)
                return null;

            int numberOfReports = GetNumberOfReportsByEmployeeId(employee.EmployeeId);
            
            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = numberOfReports
            };
        }

        /// <summary>
        /// Calculates the number of reports under an employee. 
        /// The number of reports includes all the direct reports for an employee and all of their direct reports.
        /// </summary>
        /// <param name="employeeId">The employee Id</param>
        /// <returns>The number of reports under the employee</returns>
        private int GetNumberOfReportsByEmployeeId(string employeeId)
        {
            Employee employee = _employeeService.GetById(employeeId, true);

            // If the employee has no direct reports listed, return total = 0
            if (employee.DirectReports == null)
                return 0;
            
            // Start with the count of direct reports the employee has
            int totalReports = employee.DirectReports.Count;

            // Add the number of reports for each of their direct reports
            foreach (var report in employee.DirectReports)
            { 
                totalReports += GetNumberOfReportsByEmployeeId(report.EmployeeId);
            }

            return totalReports;
        }
    }
}
