using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Domain.Workout;
using FitnessTracker.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Command
{
    public class SaveBodyInfoCommandHandler : HandlerBase<IWorkoutService>, ICommandHandler<SaveBodyInfoCommand, BodyInfoDTO>
    {
        public SaveBodyInfoCommandHandler(IWorkoutService service, IMapper mapper) : base(service, mapper)
        {
        }

        public BodyInfoDTO Handle(SaveBodyInfoCommand command)
        {
            var bodyInfoMap = _mapper.Map<BodyInfo>(command.BodyInfo);
            var bodyInfo = _service.SaveBodyInfo(bodyInfoMap);

            return _mapper.Map<BodyInfoDTO>(bodyInfo);
        }

        public async Task<BodyInfoDTO> HandleAsync(SaveBodyInfoCommand command)
        {
            return await Task.FromResult<BodyInfoDTO>(Handle(command));
        }
    }
}