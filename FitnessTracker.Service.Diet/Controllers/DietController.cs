using FitnessTracker.Application.Diet.Command;
using FitnessTracker.Application.Diet.Diet.Commands;
using FitnessTracker.Application.Diet.Queries;
using FitnessTracker.Application.Model.Diet;
using MediatR;
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
        private readonly IMediator _mediator;
        private readonly ILogger<DietController> _logger;

        public DietController(IMediator mediator, ILogger<DietController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("GetSavedMenuItems")]
        public async Task<IActionResult> GetSavedMenuItems()
        {
            _logger.LogInformation("Getting Saved Menu Items.....");

            List<FoodInfoDTO> foodList = await _mediator.Send<List<FoodInfoDTO>>(new GetSavedMenuItemsQuery());
            return Ok(foodList);
        }

        [HttpGet]
        [Route("GetColumns")]
        public async Task<IActionResult> GetColumns()
        {
            List<MealInfoDTO> foodList = await _mediator.Send<List<MealInfoDTO>>(new GetColumnsQuery());
            return Ok(foodList);
        }

        [HttpGet]
        [Route("GetMetabolicInfoCalcType/{id}")]
        public async Task<IActionResult> GetMetabolicInfoCalcType(string id)
        {
            CurrentMacrosDTO currentMacro = await _mediator.Send<CurrentMacrosDTO>(new GetMetabolicInfoCalcTypeQuery() { Id = id });
            return Ok(currentMacro);
        }

        [HttpGet]
        [Route("GetMetabolicInfo")]
        public async Task<IActionResult> GetMetabolicInfo()
        {
            List<MetabolicInfoDTO> metabolicInfoList = await _mediator.Send<List<MetabolicInfoDTO>>(new GetMetabolicInfoQuery());
            return Ok(metabolicInfoList);
        }

        [HttpPost]
        [Route("ProcessItem")]
        public async Task<IActionResult> ProcessItem([FromBody] FoodInfoDTO item)
        {
            FoodInfoDTO newItem = await _mediator.Send<FoodInfoDTO>(new ProcessItemCommand() { FoodInfo = item });
            await _mediator.Send<Unit>(new ProcessItemToEventBusCommand() { FoodInfo = newItem });  // send to event bus

            return Ok(newItem);
        }

        [HttpPost]
        [Route("DeleteFoodItem")]
        public async Task<IActionResult> DeleteFoodItem([FromBody] FoodInfoDTO item)
        {
            FoodInfoDTO deletedItem = await _mediator.Send<FoodInfoDTO>(new DeleteFoodItemCommand() { FoodInfo = item });
            await _mediator.Send<Unit>(new DeleteFoodItemToEventBusCommand() { FoodInfo = deletedItem });

            return Ok(deletedItem);
        }

        [HttpPost]
        [Route("EditMetabolicInfo")]
        public async Task<IActionResult> EditMetabolicInfo([FromBody] MetabolicInfoDTO item)
        {
            MetabolicInfoDTO newItem = await _mediator.Send<MetabolicInfoDTO>(new EditMetabolicInfoCommand() { MetabolicInfo = item });
            await _mediator.Send<Unit>(new EditMetabolicInfoToEventBusCommand() { MetabolicInfo = newItem });

            return Ok(newItem);
        }

        [HttpPost]
        [Route("SaveMenu")]
        public async Task<IActionResult> SaveMenu([FromBody] List<NutritionInfoDTO> items)
        {
            await _mediator.Send<List<NutritionInfoDTO>>(new SaveMenuCommand() { Menu = items });
            await _mediator.Send<Unit>(new SaveMenuToEventBusCommand() { Menu = items });

            return Ok(items);
        }
    }
}