using AutoMapper;
using EventBusAzure;
using FitnessTracker.Application.MappingProfile;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Persistance.Workout;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FitnessTracker.Serverless.Workout
{
    public class EnvironmentSetup
    {
        public IMapper Mapper { get; set; }
        public IWorkoutService WorkoutSerivce { get; set; }
        public IOptions<FitnessTrackerSettings> Settings { get; set; }

        public EnvironmentSetup(string appDir)
        {
            Mapper = WorkoutMapperConfig.GetWorkoutMapperConfig();
            Settings = Options.Create(new FitnessTrackerSettings());

            var config = new ConfigurationBuilder()
                   .SetBasePath(appDir)
                   .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables()
                   .Build();

            Settings.Value.ConnectionString = config.GetValue<string>("ConnectionString");

            Settings.Value.AzureConnectionSettings = new AzureConnectionSettings();
            Settings.Value.AzureConnectionSettings.ConnectionString = config.GetValue<string>("AzureConnectionSettings:ConnectionString");
            Settings.Value.AzureConnectionSettings.TopicName = config.GetValue<string>("AzureConnectionSettings:TopicName");
            Settings.Value.AzureConnectionSettings.SubscriptionClientName = config.GetValue<string>("AzureConnectionSettings:SubscriptionClientName");

            WorkoutSerivce = new WorkoutDB(Settings);
        }
    }

    //TODO:  Move to separate file (common)
    public class WorkoutMapperConfig
    {
        public IMapper GetMapperConfiguration()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WorkoutMappingProfile());
            });

            return mapperConfig.CreateMapper();
        }

        public static IMapper GetWorkoutMapperConfig()
        {
            return new WorkoutMapperConfig().GetMapperConfiguration();
        }
    }
}