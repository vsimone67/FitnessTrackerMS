using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Domain.Diet;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Command
{
    public class DeleteFoodItemCommandHandler : HandlerBase<IDietService>, ICommandHandler<DeleteFoodItemCommand, FoodInfoDTO>
    {
        public DeleteFoodItemCommandHandler(IDietService service, IMapper mapper) : base(service, mapper)
        {
        }

        public FoodInfoDTO Handle(DeleteFoodItemCommand command)
        {
            var foodCommandDTO = _mapper.Map<FoodInfo>(command.FoodInfo);
            var foodinfo = _service.DeleteFood(foodCommandDTO);

            return _mapper.Map<FoodInfoDTO>(foodinfo);
        }

        public async Task<FoodInfoDTO> HandleAsync(DeleteFoodItemCommand command)
        {
            return await Task.Run<FoodInfoDTO>(() => Handle(command)).ConfigureAwait(false);
        }
    }
}