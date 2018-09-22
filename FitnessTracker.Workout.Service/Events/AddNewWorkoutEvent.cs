using EventBus.Events;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Workout.Service.Events
{
    public class AddNewWorkoutEvent : IntegrationEvent
    {
        public WorkoutDTO AddedWorkout { get; set; }
    }
}