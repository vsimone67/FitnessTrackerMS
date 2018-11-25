using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Domain.Diet;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Command
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
                if (item.item.Count() > 0)
                    _service.SaveMenu(item);
            });

            return command.Menu;
        }

        public async Task<List<NutritionInfoDTO>> HandleAsync(SaveMenuCommand command)
        {
            return await Task.FromResult<List<NutritionInfoDTO>>(Handle(command));
        }
    }
}