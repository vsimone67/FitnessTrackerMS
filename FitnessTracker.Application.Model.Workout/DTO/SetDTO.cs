using System.Collections.Generic;

namespace FitnessTracker.Application.Model.Workout
{
    public class SetDTO
    {
        public int SetId { get; set; }
        public int WorkoutId { get; set; }
        public int SetNameId { get; set; }
        public int? SetOrder { get; set; }
        public IEnumerable<ExerciseDTO> Exercise { get; set; }
    }
}