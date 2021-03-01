using Landmark.Infrastructure.DataContracts;
using System.Threading.Tasks;

namespace Landmark.Infrastructure.Services
{
    public interface IFloodMonitoringService
    {
        Task<EnvironmentAgencyFloodAlertServicePayload> GetFoodDataAsync();
    }
}
