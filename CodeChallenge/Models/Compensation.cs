using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
