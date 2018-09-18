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
    public class EditMetabolicInfoCommandHandler : HandlerBase<IDietService>, ICommandHandler<EditMetabolicInfoCommand, MetabolicInfoDTO>
    {
        public EditMetabolicInfoCommandHandler(IDietService service, IMapper mapper) : base(service, mapper) { }

        public MetabolicInfoDTO Handle(EditMetabolicInfoCommand command)
        {
            var metabolicInfo = _mapper.Map<MetabolicInfo>(command.MetabolicInfo);
            var  editRecord = _service.EditMetabolicInfo(metabolicInfo);

            return _mapper.Map<MetabolicInfoDTO>(editRecord);
        }

        public async Task<MetabolicInfoDTO> HandleAsync(EditMetabolicInfoCommand command)
        {
            return await Task.FromResult<MetabolicInfoDTO>(Handle(command));
        }
    }
}