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
    public class DeleteFoodItemCommandHandler : HandlerBase<IDietRepository>, IRequestHandler<DeleteFoodItemCommand, FoodInfoDTO>
    {
        public DeleteFoodItemCommandHandler(IDietRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<FoodInfoDTO> Handle(DeleteFoodItemCommand request, CancellationToken cancellationToken)
        {
            var foodCommandDTO = _mapper.Map<FoodInfo>(request.FoodInfo);
            var foodinfo = await _repository.DeleteFoodAsync(foodCommandDTO);

            return _mapper.Map<FoodInfoDTO>(foodinfo);
        }
    }
}