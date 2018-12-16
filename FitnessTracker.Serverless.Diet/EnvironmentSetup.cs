using AutoMapper;
using EventBusAzure;
using FitnessTracker.Application.MappingProfile;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Persistance.Diet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using FitnessTracker.Application.Interfaces;

namespace FitnessTracker.Serverless.Diet
{
    public class EnvironmentSetup
    {
        public IMapper Mapper { get; set; }
        public IDietService DietService { get; set; }
        public IOptions<FitnessTrackerSettings> Settings { get; set; }

        public EnvironmentSetup(string appDir)
        {
            Mapper = DietMapperConfig.GetDietMapperConfig();
            Settings = Options.Create(new FitnessTrackerSettings());

            var config = new ConfigurationBuilder()
                   .SetBasePath(appDir)
                   .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables()
                   .Build();

            Settings.Value.ConnectionString = config.GetValue<string>("ConnectionString");

            Settings.Value.AzureConnectionSettings = new AzureConnectionSettings
            {
                ConnectionString = config.GetValue<string>("AzureConnectionSettings:ConnectionString"),
                TopicName = config.GetValue<string>("AzureConnectionSettings:TopicName"),
                SubscriptionClientName = config.GetValue<string>("AzureConnectionSettings:SubscriptionClientName")
            };

            DietService = new DietDB(Settings);
        }
    }

    //TODO:  Move to separate file (common)
    public class DietMapperConfig
    {
        public IMapper GetMapperConfiguration()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DietMappingProfile());
            });

            return mapperConfig.CreateMapper();
        }

        public static IMapper GetDietMapperConfig()
        {
            return new DietMapperConfig().GetMapperConfiguration();
        }
    }
}