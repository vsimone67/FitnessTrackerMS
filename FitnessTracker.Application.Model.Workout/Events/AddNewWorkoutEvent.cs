using EventBus.Events;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Application.Workout.Events
{
    public class AddNewWorkoutEvent : IntegrationEvent
    {
        public WorkoutDTO AddedWorkout { get; set; }
    }
}