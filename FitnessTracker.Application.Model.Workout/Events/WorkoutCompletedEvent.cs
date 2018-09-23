using EventBus.Events;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Application.Workout.Events
{
    public class WorkoutCompletedEvent : IntegrationEvent
    {
        public DailyWorkoutDTO CompletedWorkout { get; set; }
    }
}