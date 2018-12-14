using FitnessTracker.Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FitnessTracker.Serverless.Workout
{
    public static class GetWorkout
    {
        [FunctionName("GetWorkout")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            int id = int.Parse(req.Query["id"]);

            EnvironmentSetup env = new EnvironmentSetup(context.FunctionAppDirectory);
            var queryHanlder = new GetWorkoutForDisplayQueryHandler(env._workoutService);
            var results = queryHanlder.Handle(new GetWorkoutForDisplayQuery() { Id = id });

            return new OkObjectResult(results);
        }
    }
}