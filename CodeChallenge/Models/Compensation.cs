using System;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        /// <summary>
        /// Get or set the compensation Id
        /// </summary>
        public String CompensationId { get; set; }

        /// <summary>
        /// Get or set the employee Id
        /// </summary>
        [Required]
        public String EmployeeId { get; set; }

        /// <summary>
        /// Get or set the salary for an employee
        /// </summary>
        [Required]
        public Decimal Salary { get; set; }

        /// <summary>
        /// Get or set the effective date for an employee
        /// </summary>
        [Required]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Get or set the employee
        /// </summary>
        public Employee Employee { get; set; }
    }
}
