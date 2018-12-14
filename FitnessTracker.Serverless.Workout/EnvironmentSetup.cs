using AutoMapper;
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
        public IMapper _mapper;
        public IWorkoutService _workoutService;
        public IOptions<FitnessTrackerSettings> _settings;

        public EnvironmentSetup(string appDir)
        {
            _mapper = WorkoutMapperConfig.GetWorkoutMapperConfig();
            _settings = Options.Create(new FitnessTrackerSettings());

            var config = new ConfigurationBuilder()
                   .SetBasePath(appDir)
                   .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables()
                   .Build();

            _settings.Value.ConnectionString = config.GetValue<string>("ConnectionString");
            _workoutService = new WorkoutDB(_settings);
        }
    }

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