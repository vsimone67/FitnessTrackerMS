using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Domain.Diet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Command
{
    public class SaveMenuCommandHandler : HandlerBase<IDietService>, ICommandHandler<SaveMenuCommand, List<NutritionInfoDTO>>
    {
        public SaveMenuCommandHandler(IDietService service, IMapper mapper) : base(service, mapper)
        {
        }

        public List<NutritionInfoDTO> Handle(SaveMenuCommand command)
        {
            _service.ClearSavedMenu();

            var saveMenuCommandItem = _mapper.Map<List<NutritionInfo>>(command.Menu);
            saveMenuCommandItem.ForEach(item =>
            {
                if (item.item.Count > 0)
                    _service.SaveMenu(item);
            });

            return command.Menu;
        }

        public async Task<List<NutritionInfoDTO>> HandleAsync(SaveMenuCommand command)
        {
            return await Task.Run<List<NutritionInfoDTO>>(() => Handle(command)).ConfigureAwait(false);
        }
    }
}