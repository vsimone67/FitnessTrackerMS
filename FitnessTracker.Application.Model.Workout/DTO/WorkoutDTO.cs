using System.Collections.Generic;

namespace FitnessTracker.Application.Model.Workout
{
    public class WorkoutDTO
    {
        public int WorkoutId { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public IEnumerable<SetDTO> Set { get; set; }
        public IEnumerable<DailyWorkoutDTO> DailyWorkout { get; set; }
    }
}