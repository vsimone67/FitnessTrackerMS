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
    public class EditMetabolicInfoCommandHandler : HandlerBase<IDietRepository>, IRequestHandler<EditMetabolicInfoCommand, MetabolicInfoDTO>
    {
        public EditMetabolicInfoCommandHandler(IDietRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<MetabolicInfoDTO> Handle(EditMetabolicInfoCommand request, CancellationToken cancellationToken)
        {
            var metabolicInfo = _mapper.Map<MetabolicInfo>(request.MetabolicInfo);
            var editRecord = await _repository.EditMetabolicInfoAsync(metabolicInfo);

            return _mapper.Map<MetabolicInfoDTO>(editRecord);
        }
    }
}