using FitnessTracker.Application.Queries;
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
    public static class GetBodyInfo
    {
        [FunctionName("GetBodyInfo")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            IActionResult retval;

            try
            {
                EnvironmentSetup<IWorkoutService, WorkoutDB> ftEnvironment = new EnvironmentSetup<IWorkoutService, WorkoutDB>(context.FunctionAppDirectory, WorkoutMapperConfig.GetWorkoutMapperConfig());
                var queryHanlder = new GetBodyInfoQueryHandler(ftEnvironment.Service, ftEnvironment.Mapper);
                var results = queryHanlder.Handle(new GetBodyInfoQuery());

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