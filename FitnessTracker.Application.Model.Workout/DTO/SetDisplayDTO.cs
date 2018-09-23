using System.Collections.Generic;

namespace FitnessTracker.Application.Model.Workout
{
    public class SetDisplayDTO
    {
        public string Name { get; set; }
        public int SetId { get; set; }

        public List<RepsDisplayDTO> DisplayReps;
        public List<ExerciseDisplayDTO> Exercise { get; set; }

        public int AdditionalSets { get; set; }
    }
}