using AutoMapper;
using EventBus.Abstractions;
using FitnessTracker.Application.Workout.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Workout.Command
{
    public class SaveWorkoutToEventBusCommandHandler : IRequestHandler<SaveWorkoutToEventBusCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;

        public SaveWorkoutToEventBusCommandHandler(IEventBus eventBus, IMapper mapper)
        {
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveWorkoutToEventBusCommand request, CancellationToken cancellationToken)
        {
            // write to event bus a new workout as been added
            var evt = new AddNewWorkoutEvent
            {
                AddedWorkout = request.Workout
            };
            _eventBus.Publish(evt);

            return await Task.FromResult(new Unit());
        }
    }
}