using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class ReportingStructure
    {
        /// <summary>
        /// Get or set the employee
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Get or set the total number of reports for an employee
        /// </summary>
        public int NumberOfReports { get; set; }
    }
}
