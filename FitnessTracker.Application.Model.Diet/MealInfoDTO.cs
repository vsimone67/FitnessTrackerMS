using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Model.Diet
{
    public class MealInfoDTO
    {       
        public int MealId { get; set; }
        public string MealName { get; set; }
        public string MealDisplayName { get; set; }
    }
}
