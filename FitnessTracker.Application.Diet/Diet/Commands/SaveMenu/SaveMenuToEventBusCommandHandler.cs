using EventBus.Abstractions;
using FitnessTracker.Application.Model.Diet.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Diet.Commands
{
    public class SaveMenuToEventBusCommandHandler : IRequestHandler<SaveMenuToEventBusCommand>
    {
        private readonly IEventBus _eventBus;

        public SaveMenuToEventBusCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(SaveMenuToEventBusCommand request, CancellationToken cancellationToken)
        {
            // write to event bus that the menu has been saved
            var evt = new SaveMenuEvent
            {
                SavedMenu = request.Menu
            };
            _eventBus.Publish(evt);

            return await Task.FromResult(new Unit()).ConfigureAwait(false);
        }
    }
}