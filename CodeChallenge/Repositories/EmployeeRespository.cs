using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using CodeChallenge.Data;
using CodeChallenge.Models;

namespace CodeChallenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        /// <summary>
        /// Get the employee by Id with or without their direct reports
        /// </summary>
        /// <param name="id">The employee Id</param>
        /// <param name="includeDirectReports">Whether or not the direct reports should be included</param>
        /// <returns>The employee</returns>
        public Employee GetById(string id, bool includeDirectReports)
        {
            // Only get the direct reports if needed
            if (includeDirectReports)
                return _employeeContext.Employees.Include(x => x.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
                
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
