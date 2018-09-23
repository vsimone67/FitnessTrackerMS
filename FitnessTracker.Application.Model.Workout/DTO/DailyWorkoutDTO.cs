using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Model.Workout
{
    public class DailyWorkoutDTO
    {      
        public int DailyWorkoutId { get; set; }
        public DateTime WorkoutDate { get; set; }
        public string Phase { get; set; }
        public int WorkoutId { get; set; }
        public int Duration { get; set; }
        public IEnumerable<DailyWorkoutInfoDTO> DailyWorkoutInfo { get; set; }
    }
}
