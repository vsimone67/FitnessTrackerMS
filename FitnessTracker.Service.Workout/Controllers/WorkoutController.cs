using EventBus.Abstractions;
using FitnessTracker.Application.Workout.Command;
using FitnessTracker.Application.Common.Processor;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Queries;
using FitnessTracker.Application.Workout.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Service.Controllers
{
    [Route("api/v1/[controller]")]
    public class WorkoutController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandProcessor _commandProcessor;
        private readonly ILogger<WorkoutController> _logger;
        private readonly IEventBus _eventBus;

        public WorkoutController(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor, ILogger<WorkoutController> logger, IEventBus eventBus)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("GetWorkouts")]
        public async Task<IActionResult> GetWorkouts()
        {
            _logger.LogInformation("Getting Workouts.....");

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

            // write to event bus that a bodyinfo was saved
            var evt = new BodyInfoSavedEvent
            {
                SavedBodyInfo = savedBodyInfo
            };
            _eventBus.Publish(evt);

            return Ok(savedBodyInfo);
        }

        [HttpPost]
        [Route("SaveDailyWorkout")]
        public async Task<IActionResult> SaveDailyWorkout([FromBody] WorkoutDisplayDTO item)
        {
            DailyWorkoutDTO savedWorkout = await _commandProcessor.ProcessAsync<DailyWorkoutDTO>(new SaveDailyWorkoutCommand() { Workout = item });

            // write to event bus a  workout as been completed
            var evt = new WorkoutCompletedEvent
            {
                CompletedWorkout = savedWorkout
            };
            _eventBus.Publish(evt);

            return Ok(savedWorkout);
        }

        [HttpPost]
        [Route("SaveWorkout")]
        public async Task<IActionResult> SaveWorkout([FromBody] WorkoutDTO item)
        {
            WorkoutDTO savedWorkout = await _commandProcessor.ProcessAsync<WorkoutDTO>(new SaveWorkoutCommand() { Workout = item });

            // write to event bus a new workout as been added
            var evt = new AddNewWorkoutEvent
            {
                AddedWorkout = savedWorkout
            };
            _eventBus.Publish(evt);

            return Ok(savedWorkout);
        }
    }
}