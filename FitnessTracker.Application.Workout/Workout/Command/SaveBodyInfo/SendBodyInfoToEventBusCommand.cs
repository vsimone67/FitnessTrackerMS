using FitnessTracker.Application.Model.Workout;
using MediatR;

namespace FitnessTracker.Application.Workout.Workout.Command
{
    public class SendBodyInfoToEventBusCommand : IRequest
    {
        public BodyInfoDTO BodyInfo { get; set; }
    }
}