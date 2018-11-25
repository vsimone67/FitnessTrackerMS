using System.Collections.Generic;

namespace FitnessTracker.Application.Model.Diet
{
    public class NutritionInfoDTO
    {
        public int id { get; set; }
        public string meal { get; set; }
        public int calories { get; set; }
        public double protein { get; set; }
        public double carbs { get; set; }
        public double fat { get; set; }
        public IEnumerable<CurrentMenuDTO> item { get; set; }
    }
}