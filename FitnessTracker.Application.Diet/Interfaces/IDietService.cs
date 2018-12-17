using FitnessTracker.Domain.Diet;
using System.Collections.Generic;

namespace FitnessTracker.Application.Diet.Interfaces
{
    public interface IDietService
    {
        FoodInfo AddFood(FoodInfo item);

        void ClearSavedMenu();

        FoodInfo DeleteFood(FoodInfo food);

        FoodInfo EditFood(FoodInfo item);

        MetabolicInfo EditMetabolicInfo(MetabolicInfo item);

        List<FoodInfo> GetAllFoodData();

        List<MealInfo> GetColumns();

        List<MetabolicInfo> GetMetabolicInfo();

        void SaveMenu(NutritionInfo meal);
    }
}