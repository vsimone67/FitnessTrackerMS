using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Model.Diet
{
    public class FoodDefaultDTO
    {      
        public int DefaultId { get; set; }
        public double DefaultServing { get; set; }
        public int ItemId { get; set; }
        public int MealId { get; set; }
        public MealInfoDTO MealInfo { get; set; }
    }
}
