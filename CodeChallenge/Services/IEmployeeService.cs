using System;

using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface IEmployeeService
    {
        Employee GetById(String id);

        /// <summary>
        /// Get the employee by Id with or without their direct reports
        /// </summary>
        /// <param name="id">The employee Id</param>
        /// <param name="includeDirectReports">Whether or not the direct reports should be included</param>
        /// <returns>The employee</returns>
        Employee GetById(String id, Boolean includeDirectReports);
        Employee Create(Employee employee);
        Employee Replace(Employee originalEmployee, Employee newEmployee);
    }
}
