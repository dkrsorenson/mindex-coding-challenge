
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void GetReportingStructureByEmployeeId_Returns_Ok()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";
            var expectedNumberOfReports = 4;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/employee/{employeeId}");
            var response = getRequestTask.Result;

            var reportingStructure = response.DeserializeContent<ReportingStructure>();

            // Assert request status
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Assert reporting structure results
            Assert.AreEqual(expectedFirstName, reportingStructure.Employee.FirstName);
            Assert.AreEqual(expectedLastName, reportingStructure.Employee.LastName);
            Assert.AreEqual(expectedNumberOfReports, reportingStructure.NumberOfReports);
        }

        [TestMethod]
        public void GetReportingStructureByEmployeeId_Returns_NotFound()
        {
            // Arrange
            var employeeId = "a90er89a-edd3-7845-11ce-v1008e82e56d";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/employee/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
