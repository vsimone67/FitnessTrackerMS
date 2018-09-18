using FitnessTracker.Application.Command;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Queries;
using FitnetssTracker.Application.Common.Processor;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Service.Controllers
{
    [Route("api/v1/[controller]")]
    //[CustomExceptionFilterAttribute]

    public class WorkoutController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandProcessor _commandProcessor;

        public WorkoutController(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor)
        {
            _queryProcessor = queryProcessor;
            _commandProcessor = commandProcessor;
        }

        [HttpGet]
        [Route("GetWorkouts")]
        public async Task<IActionResult> GetWorkouts()
        {
            List<Application.Model.Workout.WorkoutDTO> workout = await _queryProcessor.ProcessAsync(new GetAllWorkoutsQuery());
            return Ok(workout);
        }

        [HttpGet]
        [Route("GetWorkoutForDisplay/{id}")]
        public async Task<IActionResult> GetWorkoutForDisplay(int? id)
        {
            WorkoutDisplayDTO workout = await _queryProcessor.ProcessAsync(new GetWorkoutForDisplayQuery() { Id = id.Value });
            return Ok(workout);
        }

        [HttpGet]
        [Route("GetBodyInfo")]
        public async Task<IActionResult> GetBodyInfo()
        {
            List<BodyInfoDTO> bodyInfo = await _queryProcessor.ProcessAsync(new GetBodyInfoQuery());
            return Ok(bodyInfo);
        }

        [HttpGet]
        [Route("GetSets")]
        public async Task<IActionResult> GetSets()
        {
            var retSet = await _queryProcessor.ProcessAsync(new GetSetQuery());
            return Ok(retSet);
        }

        [HttpGet]
        [Route("GetExercises")]
        public async Task<IActionResult> GetExercises()
        {
            List<ExerciseNameDTO> exercise = await _queryProcessor.ProcessAsync(new GetExercisesQuery());
            return Ok(exercise);
        }

        [HttpGet]
        [Route("GetReps")]
        public async Task<IActionResult> GetReps()
        {
            List<RepsNameDTO> reps = await _queryProcessor.ProcessAsync(new GetRepsQuery());
            return Ok(reps);
        }

        [HttpGet]
        [Route("GetLastSavedWorkout/{id}")]
        public async Task<IActionResult> GetLastSavedWorkout(int? id)
        {
            List<DailyWorkoutDTO> savedWorkout = await _queryProcessor.ProcessAsync(new GetLastSavedWorkoutQuery() { Id = id.Value });
            return Ok(savedWorkout);
        }

        [HttpPost]
        [Route("SaveBodyInfo")]
        public async Task<IActionResult> SaveBodyInfo([FromBody] BodyInfoDTO item)
        {
            BodyInfoDTO savedBodyInfo = await _commandProcessor.ProcessAsync<BodyInfoDTO>(new SaveBodyInfoCommand() { BodyInfo = item });
            return Ok(savedBodyInfo);
        }

        [HttpPost]
        [Route("SaveDailyWorkout")]
        public async Task<IActionResult> SaveDailyWorkout([FromBody] WorkoutDisplayDTO item)
        {
            DailyWorkoutDTO savedWorkout = await _commandProcessor.ProcessAsync<DailyWorkoutDTO>(new SaveDailyWorkoutCommand() { Workout = item });
            return Ok(savedWorkout);
        }

        [HttpPost]
        [Route("SaveWorkout")]
        public async Task<IActionResult> SaveWorkout([FromBody] WorkoutDTO item)
        {
            WorkoutDTO savedWorkout = await _commandProcessor.ProcessAsync<WorkoutDTO>(new SaveWorkoutCommand() { Workout = item });
            return Ok(savedWorkout);
        }
    }
}