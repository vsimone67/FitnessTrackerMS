using FitnessTracker.Application.Command;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Application.Queries;
using FitnetssTracker.Application.Common.Processor;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Service.Controllers
{
    [Route("api/v1/[controller]")]
    //[CustomExceptionFilterAttribute]
    public class DietController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandProcessor _commandProcessor;

        public DietController(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor)
        {
            _queryProcessor = queryProcessor;
            _commandProcessor = commandProcessor;
        }

        [HttpGet]
        [Route("GetSavedMenuItems")]
        public async Task<IActionResult> GetSavedMenuItems()
        {
            List<FoodInfoDTO> foodList = await _queryProcessor.ProcessAsync(new GetSavedMenuItemsQuery());
            return Ok(foodList);
        }

        [HttpGet]
        [Route("GetColumns")]
        public async Task<IActionResult> GetColumns()
        {
            List<MealInfoDTO> foodList = await _queryProcessor.ProcessAsync(new GetColumnsQuery());
            return Ok(foodList);
        }

        [HttpGet]
        [Route("GetMetabolicInfoCalcType/{id}")]
        public async Task<IActionResult> GetMetabolicInfoCalcType(string id)
        {
            CurrentMacrosDTO currentMacro = await _queryProcessor.ProcessAsync(new GetMetabolicInfoCalcTypeQuery() { Id = id });
            return Ok(currentMacro);
        }

        [HttpGet]
        [Route("GetMetabolicInfo")]
        public async Task<IActionResult> GetMetabolicInfo()
        {
            List<MetabolicInfoDTO> metabolicInfoList = await _queryProcessor.ProcessAsync(new GetMetabolicInfoQuery());
            return Ok(metabolicInfoList);
        }

        [HttpPost]
        [Route("ProcessItem")]
        public async Task<IActionResult> ProcessItem([FromBody] FoodInfoDTO item)
        {
            FoodInfoDTO newItem = await _commandProcessor.ProcessAsync<FoodInfoDTO>(new ProcessItemCommand() { FoodInfo = item });
            return Ok(newItem);
        }

        [HttpPost]
        [Route("DeleteFoodItem")]
        public async Task<IActionResult> DeleteFoodItem([FromBody] FoodInfoDTO item)
        {
            FoodInfoDTO deletedItem = await _commandProcessor.ProcessAsync<FoodInfoDTO>(new DeleteFoodItemCommand() { FoodInfo = item });
            return Ok(deletedItem);
        }

        [HttpPost]
        [Route("EditMetabolicInfo")]
        public async Task<IActionResult> EditMetabolicInfo([FromBody] MetabolicInfoDTO item)
        {
            MetabolicInfoDTO newItem = await _commandProcessor.ProcessAsync<MetabolicInfoDTO>(new EditMetabolicInfoCommand() { MetabolicInfo = item });
            return Ok(newItem);
        }

        [HttpPost]
        [Route("SaveMenu")]
        public async Task<IActionResult> SaveMenu([FromBody] List<NutritionInfoDTO> items)
        {
            await _commandProcessor.ProcessAsync<List<NutritionInfoDTO>>(new SaveMenuCommand() { Menu = items });
            return Ok(items);
        }
    }
}