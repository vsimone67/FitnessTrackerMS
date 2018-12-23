using EventBus.Events;

namespace FitnessTracker.Application.Model.Workout.Events
{
    public class EditWorkoutEvent : IntegrationEvent
    {
        public WorkoutDTO EditedWorkout { get; set; }
    }
}