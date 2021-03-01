using AutoMapper;
using Landmark.Domain;
using Landmark.FloodData.Modules;
using Landmark.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Landmark.FloodData.Controllers.Tests
{
    [TestClass()]
    public class FloodControllerTests
    {
        private const string _serviceUrl = "http://environment.data.gov.uk";
        private const string _getUrl = "flood-monitoring/id/floods";
        private IFloodMonitoringService _service;
        private Mapper _mapper;

        [TestInitialize]
        public void Init()
        {
            _service = new FloodMonitoringService(_serviceUrl, _getUrl);
            _mapper = new Mapper(AutomapperConfiguration.GetConfig());
        }

        [TestMethod()]
        public async Task GetTest_Success()
        {
            var controller = new FloodController(_service, _mapper);
            var result = await controller.Get() as OkNegotiatedContentResult<IEnumerable<Flood>>;
            Assert.IsNotNull(result.Content);
        }

        [TestMethod()]
        public async Task GetTest_Fail()
        {
            var controller = new FloodController(new FloodMonitoringService("", ""), _mapper);
            var result = await controller.Get() as NotFoundResult;
            Assert.IsTrue(result.GetType() == typeof(System.Web.Http.Results.NotFoundResult));
        }
        [TestMethod()]
        public async Task GetTest_Area_Success()
        {
            var controller = new FloodController(new FloodMonitoringService("", ""), _mapper);
            var result = await controller.Get("Thames") as OkNegotiatedContentResult<IEnumerable<Flood>>;
            Assert.IsNotNull(result.Content);
        }

        [TestMethod()]
        public async Task GetTest_Area_NULL_Fail()
        {
            var controller = new FloodController(new FloodMonitoringService("", ""), _mapper);
            var result = await controller.Get("") as NotFoundResult;
            Assert.IsNull(result);
        }

        [TestMethod()]
        public async Task GetTest_Area_Fail()
        {
            var controller = new FloodController(new FloodMonitoringService("", ""), _mapper);
            var result = await controller.Get("") as OkNegotiatedContentResult<IEnumerable<Flood>>; ;
            Assert.IsFalse(result.Content.Any());
        }
    }
}