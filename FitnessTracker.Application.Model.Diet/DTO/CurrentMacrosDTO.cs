namespace FitnessTracker.Application.Model.Diet
{
    public class CurrentMacrosDTO
    {
        public double calories { get; set; }
        public int caloriesFactor { get; set; }
        public double protein { get; set; }
        public double proteinFactor { get; set; }
        public double carbs { get; set; }
        public double carbsFactor { get; set; }
        public double fat { get; set; }
        public double fatFactor { get; set; }
        public double weight { get; set; }
        public int weightFactor { get; set; }
        public double dcr { get; set; }
        public int dcrFactor { get; set; }
    }
}