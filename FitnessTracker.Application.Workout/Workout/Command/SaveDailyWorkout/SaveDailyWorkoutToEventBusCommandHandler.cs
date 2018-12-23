using AutoMapper;
using EventBus.Abstractions;
using FitnessTracker.Application.Workout.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Workout.Command
{
    public class SaveDailyWorkoutToEventBusCommandHandler : IRequestHandler<SaveDailyWorkoutToEventBusCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;

        public SaveDailyWorkoutToEventBusCommandHandler(IEventBus eventBus, IMapper mapper)
        {
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveDailyWorkoutToEventBusCommand request, CancellationToken cancellationToken)
        {
            var evt = new WorkoutCompletedEvent
            {
                CompletedWorkout = request.Workout
            };
            _eventBus.Publish(evt);

            return await Task.FromResult(new Unit());
        }
    }
}