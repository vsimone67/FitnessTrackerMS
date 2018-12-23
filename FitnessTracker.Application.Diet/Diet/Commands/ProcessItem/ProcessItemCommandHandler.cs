using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Domain.Diet;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Command
{
    public class ProcessItemCommandHandler : HandlerBase<IDietRepository>, IRequestHandler<ProcessItemCommand, FoodInfoDTO>
    {
        public ProcessItemCommandHandler(IDietRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<FoodInfoDTO> Handle(ProcessItemCommand request, CancellationToken cancellationToken)
        {
            FoodInfo newItem;

            var foodInfoCommandInput = _mapper.Map<FoodInfo>(request.FoodInfo);

            if (request.FoodInfo.ItemId == 0)
                newItem = await _repository.AddFoodAsync(foodInfoCommandInput);
            else
                newItem = await _repository.EditFoodAsync(foodInfoCommandInput);

            return _mapper.Map<FoodInfoDTO>(newItem);
        }
    }
}