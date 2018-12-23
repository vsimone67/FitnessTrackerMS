using EventBus.Abstractions;
using FitnessTracker.Application.Model.Diet.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Diet.Commands
{
    public class DeleteFoodItemToEventBusCommandHandler : IRequestHandler<DeleteFoodItemToEventBusCommand>
    {
        private readonly IEventBus _eventBus;

        public DeleteFoodItemToEventBusCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(DeleteFoodItemToEventBusCommand request, CancellationToken cancellationToken)
        {
            // write to event bus a new food item has been deleted
            var evt = new DeleteFoodItemEvent
            {
                DeletedFoodItem = request.FoodInfo
            };
            _eventBus.Publish(evt);

            return await Task.FromResult(new Unit()).ConfigureAwait(false);
        }
    }
}