using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
