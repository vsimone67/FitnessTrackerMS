using EventBus.Events;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Application.Workout.Events
{
    public class BodyInfoSavedEvent : IntegrationEvent
    {
        public BodyInfoDTO SavedBodyInfo { get; set; }
    }
}