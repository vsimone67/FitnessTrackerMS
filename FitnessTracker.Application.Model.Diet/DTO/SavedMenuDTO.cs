using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Model.Diet
{
    public class SavedMenuDTO
    {       
        public int Id { get; set; }
        public double Serving { get; set; }
        public int ItemId { get; set; }
        public int MealId { get; set; }
    }
}
