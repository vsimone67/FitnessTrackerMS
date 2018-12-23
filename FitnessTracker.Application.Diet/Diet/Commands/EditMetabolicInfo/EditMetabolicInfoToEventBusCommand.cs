using FitnessTracker.Application.Model.Diet;
using MediatR;

namespace FitnessTracker.Application.Diet.Diet.Commands
{
    public class EditMetabolicInfoToEventBusCommand : IRequest
    {
        public MetabolicInfoDTO MetabolicInfo { get; set; }
    }
}