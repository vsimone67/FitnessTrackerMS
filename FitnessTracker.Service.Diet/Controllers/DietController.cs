using EventBus.Abstractions;
using FitnessTracker.Application.Diet.Command;
using FitnessTracker.Application.Common.Processor;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Application.Model.Diet.Events;
using FitnessTracker.Application.Diet.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly ILogger<DietController> _logger;
        private readonly IEventBus _eventBus;

        public DietController(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor, ILogger<DietController> logger, IEventBus eventBus)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("GetSavedMenuItems")]
        public async Task<IActionResult> GetSavedMenuItems()
        {
            _logger.LogInformation("Getting Saved Menu Items.....");

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

            // write to event bus a new food item has been added
            var evt = new AddNewFoodEvent
            {
                AddedFoodItem = newItem
            };
            _eventBus.Publish(evt);

            return Ok(newItem);
        }

        [HttpPost]
        [Route("DeleteFoodItem")]
        public async Task<IActionResult> DeleteFoodItem([FromBody] FoodInfoDTO item)
        {
            FoodInfoDTO deletedItem = await _commandProcessor.ProcessAsync<FoodInfoDTO>(new DeleteFoodItemCommand() { FoodInfo = item });

            // write to event bus a new food item has been deleted
            var evt = new DeleteFoodItemEvent
            {
                DeletedFoodItem = deletedItem
            };
            _eventBus.Publish(evt);

            return Ok(deletedItem);
        }

        [HttpPost]
        [Route("EditMetabolicInfo")]
        public async Task<IActionResult> EditMetabolicInfo([FromBody] MetabolicInfoDTO item)
        {
            MetabolicInfoDTO newItem = await _commandProcessor.ProcessAsync<MetabolicInfoDTO>(new EditMetabolicInfoCommand() { MetabolicInfo = item });

            // write to event bus that the metabilic info has been edited
            var evt = new EditMetabolicInfo
            {
                EditedMetabolicInfo = newItem
            };
            _eventBus.Publish(evt);

            return Ok(newItem);
        }

        [HttpPost]
        [Route("SaveMenu")]
        public async Task<IActionResult> SaveMenu([FromBody] List<NutritionInfoDTO> items)
        {
            await _commandProcessor.ProcessAsync<List<NutritionInfoDTO>>(new SaveMenuCommand() { Menu = items });

            // write to event bus that the menu has been saved
            var evt = new SaveMenuEvent
            {
                SavedMenu = items
            };
            _eventBus.Publish(evt);

            return Ok(items);
        }
    }
}