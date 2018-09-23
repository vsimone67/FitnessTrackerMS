using EventBus.Events;

namespace FitnessTracker.Application.Model.Diet.Events
{
    public class AddNewFoodEvent : IntegrationEvent
    {
        public FoodInfoDTO AddedFoodItem { get; set; }
    }
}