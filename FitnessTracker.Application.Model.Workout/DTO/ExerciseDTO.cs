using System.Collections.Generic;

namespace FitnessTracker.Application.Model.Workout
{
    public class ExerciseDTO
    {
        public int ExerciseId { get; set; }
        public string Measure { get; set; }
        public int SetId { get; set; }
        public int ExerciseNameId { get; set; }
        public int? ExerciseOrder { get; set; }
        public IEnumerable<RepsDTO> Reps { get; set; }
        public int ExerciseNameExerciseNameId { get; set; }
        public string ExerciseNameName { get; set; }
        public IEnumerable<ExerciseDTO> ExerciseNameExercise { get; set; }
    }
}