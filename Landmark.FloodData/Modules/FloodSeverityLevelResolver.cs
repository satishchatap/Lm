using AutoMapper;
using Landmark.Domain;
using Landmark.Infrastructure.DataContracts;

namespace Landmark.FloodData.Modules
{
    public class FloodSeverityLevelResolver :
         IValueResolver<EnvironmentAgencyFloodAlert, Flood, SeverityLevel>
    {
        public SeverityLevel Resolve(
            EnvironmentAgencyFloodAlert source, Flood destination, SeverityLevel destMember, ResolutionContext context)
        {
            return (SeverityLevel)source.SeverityLevel;
        }
    }
}