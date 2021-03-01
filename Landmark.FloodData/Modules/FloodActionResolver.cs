using AutoMapper;
using Landmark.Domain;
using Landmark.Infrastructure.DataContracts;

namespace Landmark.FloodData.Modules
{
    public class FloodActionResolver :
        IValueResolver<EnvironmentAgencyFloodAlert, Flood, FloodAction>
    {
        public FloodAction Resolve(
            EnvironmentAgencyFloodAlert source, Flood destination, FloodAction destMember, ResolutionContext context)
        {
            FloodAction action;
            switch (source.EaRegionName.ToLower())
            {
                case "south west":
                case "south east":
                    {
                        action = FloodAction.MonitorHourly;
                        break;
                    }
                case "north west":
                    {
                        action = FloodAction.MonitorDaily;
                        break;
                    }
                default:
                    {
                        action = FloodAction.Ignore;
                        break;
                    }
            }

            return action;
        }
    }
}