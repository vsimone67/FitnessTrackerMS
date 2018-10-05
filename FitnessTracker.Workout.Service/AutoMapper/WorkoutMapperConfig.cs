using AutoMapper;
using FitnessTracker.Application.MappingProfile;

namespace FitnessTracker.Workout.Service.AutoMapper
{
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