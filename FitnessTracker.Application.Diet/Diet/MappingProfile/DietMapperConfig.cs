using AutoMapper;
using FitnessTracker.Application.MappingProfile;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Application.Diet.Diet.MappingProfile
{
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