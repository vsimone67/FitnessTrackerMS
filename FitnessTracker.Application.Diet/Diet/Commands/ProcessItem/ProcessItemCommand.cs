using FitnessTracker.Application.Model.Diet;
using MediatR;

namespace FitnessTracker.Application.Diet.Command
{
    public class ProcessItemCommand : IRequest<FoodInfoDTO>
    {
        public FoodInfoDTO FoodInfo { get; set; }
    }
}