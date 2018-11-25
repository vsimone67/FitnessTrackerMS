using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Domain.Diet;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Command
{
    public class ProcessItemCommandHandler : HandlerBase<IDietService>, ICommandHandler<ProcessItemCommand, FoodInfoDTO>
    {
        public ProcessItemCommandHandler(IDietService service, IMapper mapper) : base(service, mapper)
        {
        }

        public FoodInfoDTO Handle(ProcessItemCommand command)
        {
            FoodInfo newItem;

            var foodInfoCommandInput = _mapper.Map<FoodInfo>(command.FoodInfo);

            if (command.FoodInfo.ItemId == 0)
                newItem = _service.AddFood(foodInfoCommandInput);
            else
                newItem = _service.EditFood(foodInfoCommandInput);

            return _mapper.Map<FoodInfoDTO>(newItem);
        }

        public async Task<FoodInfoDTO> HandleAsync(ProcessItemCommand command)
        {
            return await Task.FromResult<FoodInfoDTO>(Handle(command));
        }
    }
}