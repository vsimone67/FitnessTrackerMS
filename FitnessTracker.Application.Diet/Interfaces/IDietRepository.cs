using FitnessTracker.Domain.Diet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Interfaces
{
    public interface IDietRepository
    {
        Task<FoodInfo> AddFoodAsync(FoodInfo item);

        Task ClearSavedMenuAsync();

        Task<FoodInfo> DeleteFoodAsync(FoodInfo food);

        Task<FoodInfo> EditFoodAsync(FoodInfo item);

        Task<MetabolicInfo> EditMetabolicInfoAsync(MetabolicInfo item);

        Task<List<FoodInfo>> GetAllFoodDataAsync();

        Task<List<MealInfo>> GetColumnsAsync();

        Task<List<MetabolicInfo>> GetMetabolicInfoAsync();

        Task SaveMenuAsync(NutritionInfo meal);
    }
}