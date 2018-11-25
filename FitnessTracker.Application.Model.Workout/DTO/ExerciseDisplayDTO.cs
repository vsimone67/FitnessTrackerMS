using System.Collections.Generic;

namespace FitnessTracker.Application.Model.Workout
{
    public class ExerciseDisplayDTO
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public string Measure { get; set; }
        public int SetId { get; set; }
        public List<RepsDisplayDTO> Reps { get; set; }
    }
}