using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Domain.Diet
{
    public class MealInfo
    {
        [Key]
        public int MealId { get; set; }
        public string MealName { get; set; }
        public string MealDisplayName { get; set; }        
    }
}
