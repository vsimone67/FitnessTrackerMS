using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Domain.Diet
{
    public class SavedMenu
    {
        [Key]
        public int Id { get; set; }
        public double Serving { get; set; }
        public int ItemId { get; set; }
        public int MealId { get; set; }
        [ForeignKey("ItemId")]
        public virtual FoodInfo FoodInfo { get; set; }
        [ForeignKey("MealId")]
        public virtual MealInfo MealInfo { get; set; }
    }
}
