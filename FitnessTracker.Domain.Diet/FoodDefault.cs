using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Domain.Diet
{
    public class FoodDefault
    {
        [Key]
        public int DefaultId { get; set; }

        public double DefaultServing { get; set; }
        public int ItemId { get; set; }
        public int MealId { get; set; }

        [ForeignKey("ItemId")]
        public virtual FoodInfo NutritionInfo { get; set; }

        [ForeignKey("MealId")]
        public virtual MealInfo MealInfo { get; set; }
    }
}