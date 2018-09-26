using AutoMapper;
using FitnessTracker.Application.MappingProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Diet.Service.AutoMapper
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