using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Domain.Diet
{
    public class FoodInfo
    {
       
        [Key]
        public int ItemId { get; set; }
        public string Item { get; set; }
        public string ServingSize { get; set; }
        public double Calories { get; set; }
        public double Protien { get; set; }
        public double Carbs { get; set; }
        public double Fat { get; set; }             
        [ForeignKey("ItemId")]
        public virtual ICollection<FoodDefault> FoodDefault { get; set; }
        [ForeignKey("ItemId")]
        public virtual ICollection<SavedMenu> SavedMenu { get; set; }
    }
}
