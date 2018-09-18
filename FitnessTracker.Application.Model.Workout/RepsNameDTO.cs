using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Model.Workout
{
    public class RepsNameDTO
    {        
        public int RepsNameId { get; set; }
        public string Name { get; set; }
        public int RepOrder { get; set; }
    }
}
