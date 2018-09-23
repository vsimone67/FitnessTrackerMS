using System;

namespace FitnessTracker.Application.Model.Workout
{
    public class BodyInfoDTO
    {
 
        public int Id { get; set; }
        public string Phase { get; set; }
        public DateTime Date { get; set; }
        public double Weight { get; set; }
        public double BodyFat { get; set; }
        public string Note { get; set; }
        public int? Calories { get; set; }
        public int? Protein { get; set; }
        public int? Fat { get; set; }
        public int? Carbs { get; set; }
        public bool isBestWeight { get; set; }
        public bool isBestBodyFat { get; set; }
        public bool isWorstWeight { get; set; }
        public bool isWorstBodyFat { get; set; }
    }
}
