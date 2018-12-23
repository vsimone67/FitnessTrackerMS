using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetRepsQueryHanlder : HandlerBase<IWorkoutRepository>, IRequestHandler<GetRepsQuery, List<RepsNameDTO>>
    {
        public GetRepsQueryHanlder(IWorkoutRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<RepsNameDTO>> Handle(GetRepsQuery request, CancellationToken cancellationToken)
        {
            var reps = await _repository.GetRepsAsync();

            return _mapper.Map<List<RepsNameDTO>>(reps.OrderBy(exp => exp.RepOrder).ToList());
        }
    }
}