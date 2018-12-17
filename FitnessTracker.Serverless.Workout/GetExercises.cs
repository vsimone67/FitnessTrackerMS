using FitnessTracker.Application.Workout.Queries;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Application.Workout.Workout.MappingProfile;
using FitnessTracker.Common.Serverless;
using FitnessTracker.Persistance.Workout;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FitnessTracker.Serverless.Workout
{
    public static class GetExercises
    {
        [FunctionName("GetExercises")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            IActionResult retval;

            try
            {
                EnvironmentSetup<IWorkoutService, WorkoutDB> ftEnvironment = new EnvironmentSetup<IWorkoutService, WorkoutDB>(context.FunctionAppDirectory, WorkoutMapperConfig.GetWorkoutMapperConfig());
                var queryHanlder = new GetExercisesQueryHandler(ftEnvironment.Service, ftEnvironment.Mapper);
                var results = queryHanlder.Handle(new GetExercisesQuery());

                retval = new OkObjectResult(JsonConvert.SerializeObject(results));
            }
            catch (Exception ex)
            {
                retval = new BadRequestObjectResult(ex.Message);
            }

            return retval;
        }
    }
}