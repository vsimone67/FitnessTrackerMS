using System;
using System.Collections.Generic;

namespace FitnessTracker.Domain.Workout
{
    public partial class DailyWorkoutInfo
    {
        public int DailyWorkoutInfoId { get; set; }
        public int WeightUsed { get; set; }
        public int DailyWorkoutId { get; set; }
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
        public int SetId { get; set; }
        public int RepsId { get; set; }
        public virtual DailyWorkout DailyWorkout { get; set; }
        public virtual Exercise Exercise { get; set; }
        public virtual Reps Reps { get; set; }
        public virtual Set Set { get; set; }
    }
}
