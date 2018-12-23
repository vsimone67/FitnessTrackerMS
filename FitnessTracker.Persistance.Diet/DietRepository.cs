using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Common.Attributes;
using FitnessTracker.Domain.Diet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Persistance.Diet
{
    [AutoRegister(typeof(IDietRepository))]
    public class DietRepository : IDietRepository
    {
        private readonly DietContext _dbContext;

        public DietRepository(IOptions<FitnessTrackerSettings> settings)
        {
            _dbContext = new DietContext(settings.Value.ConnectionString);
        }

        public async Task<FoodInfo> AddFoodAsync(FoodInfo item)
        {
            _dbContext.FoodInfo.Add(item);
            await SaveChangesAsync();

            return item;
        }

        public async Task ClearSavedMenuAsync()
        {
            await _dbContext.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE SavedMenu");
        }

        public async Task<FoodInfo> DeleteFoodAsync(FoodInfo food)
        {
            _dbContext.Entry<FoodInfo>(food).State = EntityState.Deleted;
            await SaveChangesAsync();  // save for deletes
            return food;
        }

        public async Task<FoodInfo> EditFoodAsync(FoodInfo item)
        {
            _dbContext.Entry<FoodInfo>(item).State = EntityState.Modified;

            await SaveChangesAsync();  // save for main FoodInfo object

            return item;
        }

        public async Task<MetabolicInfo> EditMetabolicInfoAsync(MetabolicInfo item)
        {
            _dbContext.Entry<MetabolicInfo>(item).State = EntityState.Modified;

            await SaveChangesAsync();

            return item;
        }

        public async Task<List<FoodInfo>> GetAllFoodDataAsync()
        {
            return await _dbContext.FoodInfo
               .Include(fd => fd.FoodDefault)
               .ThenInclude(ni => ni.NutritionInfo)
               .Include(fd => fd.FoodDefault).ThenInclude(mi => mi.MealInfo)
               .Include(sm => sm.SavedMenu)
               .ThenInclude(fi => fi.FoodInfo)
               .Include(sm => sm.SavedMenu)
               .ThenInclude(fi => fi.MealInfo)
               .ToListAsync();
        }

        public async Task<List<MealInfo>> GetColumnsAsync()
        {
            return await _dbContext.MealInfo.ToListAsync();
        }

        public async Task<List<MetabolicInfo>> GetMetabolicInfoAsync()
        {
            return await _dbContext.MetabolicInfo.ToListAsync();
        }

        public async Task SaveMenuAsync(NutritionInfo meal)
        {
            if (meal != null)
            {
                foreach (var food in meal.item)
                {
                    if (meal.id > 0)
                    {
                        SavedMenu menuItem = new SavedMenu()
                        {
                            ItemId = food.ItemID,
                            MealId = meal.id,
                            Serving = food.Serving
                        };

                        _dbContext.SavedMenu.Add(menuItem);
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
        }

        protected async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}