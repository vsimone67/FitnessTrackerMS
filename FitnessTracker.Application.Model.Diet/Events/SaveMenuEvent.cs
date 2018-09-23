using EventBus.Events;
using System.Collections.Generic;

namespace FitnessTracker.Application.Model.Diet.Events
{
    public class SaveMenuEvent : IntegrationEvent
    {
        public List<NutritionInfoDTO> SavedMenu { get; set; }
    }
}