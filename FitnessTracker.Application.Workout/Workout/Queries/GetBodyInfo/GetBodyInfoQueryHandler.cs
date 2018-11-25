using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Domain.Workout;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Queries
{
    public class GetBodyInfoQueryHandler : HandlerBase<IWorkoutService>, IQueryHandler<GetBodyInfoQuery, List<BodyInfoDTO>>
    {
        public GetBodyInfoQueryHandler(IWorkoutService service, IMapper mapper) : base(service, mapper)
        {
        }

        public List<BodyInfoDTO> Handle(GetBodyInfoQuery query)
        {
            // put logic here
            List<BodyInfo> bodyInfo = _service.GetBodyInfo();

            //make all false to start, initialize logic for GUI
            bodyInfo.ForEach(info =>
            {
                info.isBestBodyFat = false; info.isBestWeight = false; info.isWorstBodyFat = false; info.isWorstWeight = false;
            });
            bodyInfo.OrderByDescending(info => info.Weight).First().isWorstWeight = true;
            bodyInfo.OrderByDescending(info => info.BodyFat).First().isWorstBodyFat = true;
            bodyInfo.OrderBy(info => info.Weight).First().isBestWeight = true;
            bodyInfo.OrderBy(info => info.BodyFat).First().isBestBodyFat = true;

            return _mapper.Map<List<BodyInfoDTO>>(bodyInfo);
        }

        public async Task<List<BodyInfoDTO>> HandleAsync(GetBodyInfoQuery query)
        {
            return await Task.FromResult<List<BodyInfoDTO>>(Handle(query));
        }
    }
}