using EventBus.Events;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Workout.Service.Events
{
    public class BodyInfoSavedEvent : IntegrationEvent
    {
        public BodyInfoDTO SavedBodyInfo { get; set; }
    }
}