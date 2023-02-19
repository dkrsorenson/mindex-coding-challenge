using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        /// <summary>
        /// Add new compensation record to the Compensations DB set
        /// </summary>
        /// <param name="compensation">The compensation record to add</param>
        /// <returns>The compensation record that was added</returns>
        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();

            // Get the employee record
            var employee = _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == compensation.EmployeeId);
            compensation.Employee = employee;

            _employeeContext.Compensations.Add(compensation);
            
            return compensation;
        }

        /// <summary>
        /// Gets the compensation record by employee Id
        /// </summary>
        /// <param name="employeeId">The employee Id</param>
        /// <returns>The compensation record</returns>
        public Compensation GetByEmployeeId(string employeeId)
        {
            // Notes:
            // This is a workaround to only include the employee if one exists with the provided Id.
            // Typically would rely on Foreign Key constraints to ensure that compensation records are created only for existing employees, 
            // so this check wouldn't normally be needed.
            var employee = _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                // Notes:
                // Using OrderBy and FirstOrDefault here (and below) because there can be multiple Compensation records for one employee with the current setup.
                // Typically would either limit it to one compensation record per employee with unique indexes/checks when creating compensation records
                // or would have more logic around selecting the current or "active" compensation record.
                // Used the below for simplicity for now. 
                return _employeeContext.Compensations.OrderBy(c => c.EffectiveDate).FirstOrDefault(c => c.EmployeeId == employeeId);
            }

            return _employeeContext.Compensations.Include(e => e.Employee).OrderBy(c => c.EffectiveDate).FirstOrDefault(c => c.EmployeeId == employeeId);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}
