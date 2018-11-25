using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Common;

namespace FitnessTracker.Application.Queries
{
    public class GetWorkoutForDisplayQuery : IQuery<WorkoutDisplayDTO>
    {
        public int Id { get; set; }
    }
}
