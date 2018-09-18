using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Model.Diet
{
    public class FoodInfoDTO
    {     
        public FoodInfoDTO()
        {
            Serving = 1;
        }
        public int ItemId { get; set; }
        public string Item { get; set; }
        public string ServingSize { get; set; }
        public double Calories { get; set; }
        public double Protien { get; set; }
        public double Carbs { get; set; }
        public double Fat { get; set; }
        public int Serving { get; set; }
        public IEnumerable<FoodDefaultDTO> FoodDefault { get; set; }
        public IEnumerable<SavedMenuDTO> SavedMenu { get; set; }
    }
}
