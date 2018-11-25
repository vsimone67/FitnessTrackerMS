namespace FitnessTracker.Mobile.Models
{
    public class ExerciseData
    {
        public int NextSet { get; set; }
        public int NextExercise { get; set; }
        public int NextRep { get; set; }
        public bool IsCompleted { get; set; }

        public ExerciseData()
        {
        }

        public ExerciseData(int nextSet, int nextExercise, int nextRep, bool isCompleted)
        {
            NextSet = nextSet;
            NextExercise = nextExercise;
            NextRep = nextRep;
            IsCompleted = isCompleted;
        }
    }
}