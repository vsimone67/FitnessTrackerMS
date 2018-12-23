using AutoMapper;
using EventBus.Abstractions;
using FitnessTracker.Application.Workout.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Workout.Command
{
    public class SendBodyInfoToEventBusCommandHandler : IRequestHandler<SendBodyInfoToEventBusCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;

        public SendBodyInfoToEventBusCommandHandler(IEventBus eventBus, IMapper mapper)
        {
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SendBodyInfoToEventBusCommand request, CancellationToken cancellationToken)
        {
            // write to event bus that a bodyinfo was saved
            var evt = new BodyInfoSavedEvent
            {
                SavedBodyInfo = request.BodyInfo
            };
            _eventBus.Publish(evt);

            return await Task.FromResult(new Unit());
        }
    }
}