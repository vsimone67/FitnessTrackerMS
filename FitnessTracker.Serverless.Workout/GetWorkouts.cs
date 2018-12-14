using FitnessTracker.Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FitnessTracker.Serverless.Workout
{
    public static class GetWorkouts
    {
        [FunctionName("GetWorkouts")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup env = new EnvironmentSetup(context.FunctionAppDirectory);
            var queryHanlder = new GetAllWorkoutsQueryHandler(env._workoutService, env._mapper);
            var results = queryHanlder.Handle(new GetAllWorkoutsQuery());

            return new OkObjectResult(results);
        }
    }
}