using AutoMapper;
using Landmark.Domain;
using Landmark.Infrastructure.DataContracts;
using Landmark.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Landmark.FloodData.Controllers
{
    public class FloodController : ApiController
    {
        private readonly IFloodMonitoringService _floodMonitoringService;
        private readonly Mapper _mapper;
        public FloodController(IFloodMonitoringService foodMonitoringService, Mapper mapper)
        {
            _floodMonitoringService = foodMonitoringService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Flood Data
        /// </summary>
        /// <returns>List of Flod Data</returns>
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var environmentAgencyFloodAlerts = await _floodMonitoringService.GetFoodDataAsync();

                if (environmentAgencyFloodAlerts == null)
                    return NotFound();

                var processedData = ProcessDataData(environmentAgencyFloodAlerts);

                return Ok(processedData);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Get Flood Data by AreaName
        /// </summary>
        /// <returns>List of Flod Data</returns>
        public async Task<IHttpActionResult> Get(string region)
        {
            try
            {
                var environmentAgencyApiResponse = await _floodMonitoringService.GetFoodDataAsync();

                var processedData = ProcessDataData(environmentAgencyApiResponse);

                var filteredData = FilterData(processedData, region);

                return Ok(filteredData);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        private IEnumerable<Flood> ProcessDataData(EnvironmentAgencyFloodAlertServicePayload environmentAgencyFloodAlerts)
        {
            var floodData = new List<Flood>();

            if (environmentAgencyFloodAlerts == null || !environmentAgencyFloodAlerts.Items.Any())
            {
                return floodData;
            }
            floodData = _mapper.Map<List<Flood>>(environmentAgencyFloodAlerts.Items);

            return floodData;
        }

        private IEnumerable<Flood> FilterData(IEnumerable<Flood> inputFloodData, string eaAreaName)
        {
            return inputFloodData.Where(
                item => string.Equals(item.EaAreaName, eaAreaName, StringComparison.CurrentCultureIgnoreCase)
                );
        }
    }
}
