using EventBus.Events;

namespace FitnessTracker.Application.Model.Diet.Events
{
    public class DeleteFoodItemEvent : IntegrationEvent
    {
        public FoodInfoDTO DeletedFoodItem { get; set; }
    }
}