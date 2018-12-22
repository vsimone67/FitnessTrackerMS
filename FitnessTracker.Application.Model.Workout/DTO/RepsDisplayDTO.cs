namespace FitnessTracker.Application.Model.Workout
{
    public class RepsDisplayDTO
    {
        public int RepsId { get; set; }
        public int Weight { get; set; }
        public string Name { get; set; }
        public string TimeToNextExercise { get; set; }
        public int RepOrder { get; set; }
        public int ExerciseId { get; set; }
        public int SetId { get; set; }
        public int RepsNameId { get; set; }
    }
}