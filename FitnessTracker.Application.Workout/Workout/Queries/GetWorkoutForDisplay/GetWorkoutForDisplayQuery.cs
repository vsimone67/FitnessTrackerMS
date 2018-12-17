using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetWorkoutForDisplayQuery : IQuery<WorkoutDisplayDTO>
    {
        public int Id { get; set; }
    }
}