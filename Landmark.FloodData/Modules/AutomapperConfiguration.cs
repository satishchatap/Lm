using AutoMapper;
using Landmark.Domain;
using Landmark.Infrastructure.DataContracts;

namespace Landmark.FloodData.Modules
{
    public static class AutomapperConfiguration
    {
        public static MapperConfiguration GetConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
               cfg.CreateMap<EnvironmentAgencyFloodAlert, Flood>()
                 .ForMember(dest => dest.Severity, opt => opt.MapFrom<FloodSeverityLevelResolver>())
                 .ForMember(dest => dest.Action, opt => opt.MapFrom<FloodActionResolver>())
                 .ForMember(dest => dest.Region, m => m.MapFrom(src => src.EaRegionName))
                 .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id.Replace("http://environment.data.gov.uk/flood-monitoring/id/floods/", ""))));



            configuration.AssertConfigurationIsValid();

            return configuration;
        }
    }
}