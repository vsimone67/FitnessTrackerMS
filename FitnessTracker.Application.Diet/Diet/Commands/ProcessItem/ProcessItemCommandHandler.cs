using FitnessTracker.Application.Interfaces;
using FitnetssTracker.Application.Common;
using FitnessTracker.Domain.Diet;
using System;
using System.Collections.Generic;
using FitnessTracker.Application.Model.Diet;
using AutoMapper;
using System.Threading.Tasks;
using FitnessTracker.Application.Common;

namespace FitnessTracker.Application.Command
{
    public class ProcessItemCommandHandler : HandlerBase<IDietService>, ICommandHandler<ProcessItemCommand, FoodInfoDTO>
    {
        public ProcessItemCommandHandler(IDietService service, IMapper mapper) : base(service, mapper) { }
       
        public FoodInfoDTO Handle(ProcessItemCommand command)
        {
            FoodInfo newItem;

            var foodInfoCommandInput = _mapper.Map<FoodInfo>(command.FoodInfo);

            if (command.FoodInfo.ItemId == 0)
                newItem = _service.AddFood(foodInfoCommandInput);
            else
                newItem = _service.EditFood(foodInfoCommandInput);

            return _mapper.Map <FoodInfoDTO>(newItem);
        }

        public async Task<FoodInfoDTO> HandleAsync(ProcessItemCommand command)
        {
            return await Task.FromResult<FoodInfoDTO>(Handle(command));
        }
    }
}