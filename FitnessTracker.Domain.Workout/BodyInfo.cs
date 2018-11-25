using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Domain.Workout
{
    public partial class BodyInfo
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

        [NotMapped]
        public bool isBestWeight { get; set; }

        [NotMapped]
        public bool isBestBodyFat { get; set; }

        [NotMapped]
        public bool isWorstWeight { get; set; }

        [NotMapped]
        public bool isWorstBodyFat { get; set; }
    }
}