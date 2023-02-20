using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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

            // Fix direct report references before adding
            FixUpReferences(employee);

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

        /// <summary>
        /// Fix the direct report references when adding a new employee. 
        /// This will fix the references when creating new employees with direct reports that already exist in the employees data.
        /// </summary>
        /// <param name="employee">The employee being added</param>
        private void FixUpReferences(Employee employee)
        {
            var employeeIdRefMap = from emp in _employeeContext.Employees
                                   select new { Id = emp.EmployeeId, EmployeeRef = emp };

            if (employee.DirectReports != null)
            {
                var referencedEmployees = new List<Employee>(employee.DirectReports.Count);
                employee.DirectReports.ForEach(report =>
                {
                    var referencedEmployee = employeeIdRefMap.FirstOrDefault(e => e.Id == report.EmployeeId);
                    if (referencedEmployee != null)
                    {
                        referencedEmployees.Add(referencedEmployee.EmployeeRef);
                    }
                });

                employee.DirectReports = referencedEmployees;
            }
        }
    }
}
