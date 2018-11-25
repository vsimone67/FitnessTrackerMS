using System.Collections.Generic;

namespace FitnessTracker.Application.Model.Workout
{
    public class WorkoutDisplayDTO
    {
        public int WorkoutId { get; set; }
        public string Name { get; set; }
        public List<SetDisplayDTO> Set { get; set; }
        public string Phase { get; set; }
        public int Duration { get; set; }
    }
}