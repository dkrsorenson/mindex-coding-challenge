using System;

using Microsoft.Extensions.Logging;

using CodeChallenge.Repositories;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create a new compensation record
        /// </summary>
        /// <param name="compensation">The compensation to create</param>
        /// <returns>The compensation record that was created</returns>
        public Compensation Create(Compensation compensation)
        {
            if (compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        /// <summary>
        /// Get the compensation for an employee by their Id
        /// </summary>
        /// <param name="id">The employee Id</param>
        /// <returns>The compensation</returns>
        public Compensation GetByEmployeeId(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetByEmployeeId(id);
            }

            return null;
        }
    }
}
