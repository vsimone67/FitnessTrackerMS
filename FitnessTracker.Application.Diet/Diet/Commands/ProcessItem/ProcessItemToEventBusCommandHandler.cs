using EventBus.Abstractions;
using FitnessTracker.Application.Model.Diet.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Diet.Commands
{
    public class ProcessItemToEventBusCommandHandler : IRequestHandler<ProcessItemToEventBusCommand>
    {
        private readonly IEventBus _eventBus;

        public ProcessItemToEventBusCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(ProcessItemToEventBusCommand request, CancellationToken cancellationToken)
        {
            var evt = new AddNewFoodEvent
            {
                AddedFoodItem = request.FoodInfo
            };
            _eventBus.Publish(evt);

            return await Task.FromResult(new Unit()).ConfigureAwait(false);
        }
    }
}