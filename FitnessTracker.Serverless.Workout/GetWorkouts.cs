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
    public static class GetWorkouts
    {
        [FunctionName("GetWorkouts")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            IActionResult retval;

            try
            {
                EnvironmentSetup<IWorkoutRepository, WorkoutRepository> ftEnvironment = new EnvironmentSetup<IWorkoutRepository, WorkoutRepository>(context.FunctionAppDirectory, WorkoutMapperConfig.GetWorkoutMapperConfig());
                var queryHanlder = new GetAllWorkoutsQueryHandler(ftEnvironment.Service, ftEnvironment.Mapper);
                var results = await queryHanlder.Handle(new GetAllWorkoutsQuery(), new System.Threading.CancellationToken());

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