using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Domain.Diet;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Command
{
    public class SaveMenuCommandHandler : HandlerBase<IDietRepository>, IRequestHandler<SaveMenuCommand, List<NutritionInfoDTO>>
    {
        public SaveMenuCommandHandler(IDietRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<NutritionInfoDTO>> Handle(SaveMenuCommand request, CancellationToken cancellationToken)
        {
            await _repository.ClearSavedMenuAsync();

            var saveMenuCommandItem = _mapper.Map<List<NutritionInfo>>(request.Menu);

            foreach (var item in saveMenuCommandItem)
            {
                if (item.item.Count > 0)
                    await _repository.SaveMenuAsync(item);
            }
            //saveMenuCommandItem.ForEach(async item =>
            //{
            //    if (item.item.Count > 0)
            //        await _repository.SaveMenuAsync(item);
            //});

            return request.Menu;
        }
    }
}