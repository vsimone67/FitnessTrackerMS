using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Model.Workout
{
    public class DailyWorkoutInfoDTO
    {        
        public int DailyWorkoutInfoId { get; set; }
        public int WeightUsed { get; set; }
        public int DailyWorkoutId { get; set; }
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
        public int SetId { get; set; }
        public int RepsId { get; set; }
    }
}
