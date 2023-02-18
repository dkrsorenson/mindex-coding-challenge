using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(String id);

        /// <summary>
        /// Get the employee by Id with or without their direct reports
        /// </summary>
        /// <param name="id">The employee Id</param>
        /// <param name="includeDirectReports">Whether or not the direct reports should be included</param>
        /// <returns>The employee</returns>
        Employee GetById(String id, Boolean includeDirectReports);
        Employee Add(Employee employee);
        Employee Remove(Employee employee);
        Task SaveAsync();
    }
}