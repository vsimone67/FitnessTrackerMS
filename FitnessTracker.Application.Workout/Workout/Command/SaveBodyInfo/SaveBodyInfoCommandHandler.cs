using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Domain.Workout;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Command
{
    public class SaveBodyInfoCommandHandler : HandlerBase<IWorkoutRepository>, IRequestHandler<SaveBodyInfoCommand, BodyInfoDTO>
    {
        public SaveBodyInfoCommandHandler(IWorkoutRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<BodyInfoDTO> Handle(SaveBodyInfoCommand request, CancellationToken cancellationToken)
        {
            var bodyInfoMap = _mapper.Map<BodyInfo>(request.BodyInfo);
            var bodyInfo = await _repository.SaveBodyInfoAsync(bodyInfoMap);

            return _mapper.Map<BodyInfoDTO>(bodyInfo);
        }
    }
}