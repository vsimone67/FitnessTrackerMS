using AutoMapper;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Domain.Diet;

namespace FitnessTracker.Application.MappingProfile
{
    public class DietMappingProfile : Profile
    {
        public DietMappingProfile()
        {

            CreateMap<MealInfo, MealInfoDTO>();
            CreateMap<MealInfoDTO, MealInfo>();
                        
            CreateMap<CurrentMacros, CurrentMacrosDTO>();
            CreateMap<CurrentMacrosDTO, CurrentMacros>();
                        
            CreateMap<MetabolicInfo, MetabolicInfoDTO>();
            CreateMap<MetabolicInfoDTO, MetabolicInfo>();

            CreateMap<FoodInfo, FoodInfoDTO>();
            CreateMap<FoodInfoDTO, FoodInfo>();

            CreateMap<FoodDefault, FoodDefaultDTO>();
            CreateMap<FoodDefaultDTO, FoodDefault>();

            CreateMap<SavedMenu, SavedMenuDTO>();
            CreateMap<SavedMenuDTO, SavedMenu>();

            CreateMap<NutritionInfo, NutritionInfoDTO>();
            CreateMap<NutritionInfoDTO, NutritionInfo>();

            CreateMap<CurrentMenu, CurrentMenuDTO>();
            CreateMap<CurrentMenuDTO, CurrentMenu>();


        }
    }
}
