using System;
using System.Collections.Generic;

namespace FitnessTracker.Domain.Workout
{
    public partial class DailyWorkout
    {
        public DailyWorkout()
        {
            DailyWorkoutInfo = new HashSet<DailyWorkoutInfo>();
        }

        public int DailyWorkoutId { get; set; }
        public DateTime WorkoutDate { get; set; }
        public string Phase { get; set; }
        public int WorkoutId { get; set; }
        public int? Duration { get; set; }        
        public virtual ICollection<DailyWorkoutInfo> DailyWorkoutInfo { get; set; }
    }
}
