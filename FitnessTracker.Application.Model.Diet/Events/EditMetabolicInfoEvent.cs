using EventBus.Events;

namespace FitnessTracker.Application.Model.Diet.Events
{
    public class EditMetabolicInfo : IntegrationEvent
    {
        public MetabolicInfoDTO EditedMetabolicInfo { get; set; }
    }
}