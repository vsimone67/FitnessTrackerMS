using AutoMapper;
using EventBus.Abstractions;
using FitnessTracker.Application.Model.Workout.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Workout.Command
{
    public class UpdateWorkoutToEventBusCommandHandler : IRequestHandler<UpdateWorkoutToEventBusCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;

        public UpdateWorkoutToEventBusCommandHandler(IEventBus eventBus, IMapper mapper)
        {
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateWorkoutToEventBusCommand request, CancellationToken cancellationToken)
        {
            // write to event bus a new workout as been added
            var evt = new EditWorkoutEvent
            {
                EditedWorkout = request.Workout
            };
            _eventBus.Publish(evt);

            return await Task.FromResult(new Unit());
        }
    }
}