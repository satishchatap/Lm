using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Landmark.Infrastructure.Services.Tests
{
    [TestClass()]
    public class FloodMonitoringServiceTests
    {
        private const string _serviceUrl = "http://environment.data.gov.uk";
        private const string _getUrl = "flood-monitoring/id/floods";

        [TestMethod()]
        public async Task GetFoodDataAsyncTest_Has_Data()
        {
            var service = new FloodMonitoringService(_serviceUrl, _getUrl);
            var result = await service.GetFoodDataAsync();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Items.Any());
        }
        [TestMethod()]
        public async Task GetFoodDataAsyncTest_Has_No_Data()
        {
            var service = new FloodMonitoringService(_serviceUrl, "asdfasdf");
            var result = await service.GetFoodDataAsync();
            Assert.IsNull(result);
        }
    }
}