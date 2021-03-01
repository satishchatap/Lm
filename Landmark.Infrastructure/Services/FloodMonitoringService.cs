using Landmark.Infrastructure.DataContracts;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Landmark.Infrastructure.Services
{
    public class FloodMonitoringService : IFloodMonitoringService
    {
        private readonly string _serviceUrl;
        private readonly string _getUrl;
        public FloodMonitoringService(string serviceUrl, string getUrl)
        {
            _serviceUrl = serviceUrl;
            _getUrl = getUrl;
        }

        public async Task<EnvironmentAgencyFloodAlertServicePayload> GetFoodDataAsync()
        {
            try
            {
                var environmentAgencyApiResponse = await GetEnvironmentAgencyData();

                if (environmentAgencyApiResponse.StatusCode != HttpStatusCode.OK)
                {
                    //Log message and return null
                    return null;
                }

                var environmentAgencyApiResponseContent =
                    await environmentAgencyApiResponse.Content.ReadAsStringAsync();

                var environmentAgencyFloodAlerts =
                    JsonConvert.DeserializeObject<EnvironmentAgencyFloodAlertServicePayload>(
                        environmentAgencyApiResponseContent);

                return environmentAgencyFloodAlerts;
            }
            catch (Exception)
            {
                //Log message and return null
                return null;
            }
        }
        #region Private Methods
        private async Task<HttpResponseMessage> GetEnvironmentAgencyData()
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_serviceUrl) })
            {
                return await client.GetAsync(_getUrl);
            }
        }


        #endregion
    }
}
