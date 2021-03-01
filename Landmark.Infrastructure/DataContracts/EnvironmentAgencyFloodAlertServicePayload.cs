using Newtonsoft.Json;
using System.Collections.Generic;

namespace Landmark.Infrastructure.DataContracts
{
    public class EnvironmentAgencyFloodAlertServicePayload
    {
        [JsonProperty(PropertyName = "items")]
        public IEnumerable<EnvironmentAgencyFloodAlert> Items { get; set; }
    }
}
