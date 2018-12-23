using EventBus.Abstractions;
using FitnessTracker.Application.Model.Diet.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Diet.Commands
{
    public class EditMetabolicInfoToEventBusCommandHandler : IRequestHandler<EditMetabolicInfoToEventBusCommand>
    {
        private readonly IEventBus _eventBus;

        public EditMetabolicInfoToEventBusCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(EditMetabolicInfoToEventBusCommand request, CancellationToken cancellationToken)
        {
            // write to event bus that the metabilic info has been edited
            var evt = new EditMetabolicInfo
            {
                EditedMetabolicInfo = request.MetabolicInfo
            };
            _eventBus.Publish(evt);

            return await Task.FromResult(new Unit()).ConfigureAwait(false);
        }
    }
}