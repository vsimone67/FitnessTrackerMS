using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Domain.Diet;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Command
{
    public class EditMetabolicInfoCommandHandler : HandlerBase<IDietService>, ICommandHandler<EditMetabolicInfoCommand, MetabolicInfoDTO>
    {
        public EditMetabolicInfoCommandHandler(IDietService service, IMapper mapper) : base(service, mapper)
        {
        }

        public MetabolicInfoDTO Handle(EditMetabolicInfoCommand command)
        {
            var metabolicInfo = _mapper.Map<MetabolicInfo>(command.MetabolicInfo);
            var editRecord = _service.EditMetabolicInfo(metabolicInfo);

            return _mapper.Map<MetabolicInfoDTO>(editRecord);
        }

        public async Task<MetabolicInfoDTO> HandleAsync(EditMetabolicInfoCommand command)
        {
            return await Task.Run<MetabolicInfoDTO>(() => Handle(command)).ConfigureAwait(false);
        }
    }
}