using EventBus.Events;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Workout.Service.Events
{
    public class WorkoutCompletedEvent : IntegrationEvent
    {
        public DailyWorkoutDTO CompletedWorkout { get; set; }
    }
}