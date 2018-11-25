using FitnessTracker.Application.Interfaces;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Common.Attributes;
using FitnessTracker.Domain.Diet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace FitnessTracker.Persistance.Diet
{
    [AutoRegister(typeof(IDietService))]
    public class DietDB : IDietService
    {
        private DietContext _dbContext;

        public DietDB(IOptions<FitnessTrackerSettings> settings)
        {
            _dbContext = new DietContext(settings.Value.ConnectionString);
        }

        public List<FoodInfo> GetAllFoodData()
        {
            // Use Fluet API because EF Core does not lazy load
            return _dbContext.FoodInfo
                .Include(fd => fd.FoodDefault)
                .ThenInclude(ni => ni.NutritionInfo)
                .Include(fd => fd.FoodDefault).ThenInclude(mi => mi.MealInfo)
                .Include(sm => sm.SavedMenu)
                .ThenInclude(fi => fi.FoodInfo)
                .Include(sm => sm.SavedMenu)
                .ThenInclude(fi => fi.MealInfo)
                .ToList();
        }

        public List<MealInfo> GetColumns()
        {
            return _dbContext.MealInfo.ToList();
        }

        public FoodInfo AddFood(FoodInfo item)
        {
            _dbContext.FoodInfo.Add(item);
            SaveChanges();

            return item;
        }

        public FoodInfo EditFood(FoodInfo item)
        {
            _dbContext.Entry<FoodInfo>(item).State = EntityState.Modified;

            SaveChanges();  // save for main FoodInfo object

            return item;
        }

        public FoodInfo DeleteFood(FoodInfo food)
        {
            _dbContext.Entry<FoodInfo>(food).State = EntityState.Deleted;
            SaveChanges();  // save for deletes
            return food;
        }

        public MetabolicInfo EditMetabolicInfo(MetabolicInfo item)
        {
            _dbContext.Entry<MetabolicInfo>(item).State = EntityState.Modified;

            SaveChanges();

            return item;
        }

        public List<MetabolicInfo> GetMetabolicInfo()
        {
            return _dbContext.MetabolicInfo.ToList();
        }

        public void ClearSavedMenu()
        {
            // clear old menu
            _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE SavedMenu");
        }

        public void SaveMenu(NutritionInfo meal)
        {
            // save new menu
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
                        _dbContext.SaveChanges();
                    }
                }
            }
        }

        protected int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}