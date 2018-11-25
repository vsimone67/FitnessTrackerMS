using System.Collections.Generic;

namespace FitnessTracker.Domain.Diet
{
    public class NutritionInfo
    {
        public int id { get; set; }
        public string meal { get; set; }
        public int calories { get; set; }
        public double protein { get; set; }
        public double carbs { get; set; }
        public double fat { get; set; }
        public List<CurrentMenu> item { get; set; }
    }
}