using AutoMapper;
using FitnessTracker.Application.MappingProfile;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Application.Workout.Workout.MappingProfile
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