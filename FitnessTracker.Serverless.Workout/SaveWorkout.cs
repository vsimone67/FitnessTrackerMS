using FitnessTracker.Application.Command;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Events;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace FitnessTracker.Serverless.Workout
{
    public static class SaveWorkout
    {
        [FunctionName("SaveWorkoutOrchestration")]
        public static async Task RunOrchestrator(
          [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var workout = await context.CallActivityAsync<WorkoutDTO>("SaveWorkoutData", context.GetInput<WorkoutDTO>());
            await context.CallActivityAsync<WorkoutDTO>("SaveWorkoutToSB", workout);
        }

        [FunctionName("SaveWorkoutData")]
        public static WorkoutDTO SaveToDB([ActivityTrigger] WorkoutDTO workout, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup ftEnvironment = new EnvironmentSetup(context.FunctionAppDirectory);
            var commandHanlder = new SaveWorkoutCommandHandler(ftEnvironment.WorkoutSerivce, ftEnvironment.Mapper);
            return commandHanlder.Handle(new SaveWorkoutCommand() { Workout = workout });
        }

        [FunctionName("SaveWorkoutToSB")]
        public static WorkoutDTO SendToSB([ActivityTrigger] WorkoutDTO workout, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup ftEnvironment = new EnvironmentSetup(context.FunctionAppDirectory);

            var evt = new AddNewWorkoutEvent
            {
                AddedWorkout = workout
            };

            SendEventToServiceBus serviceBus = new SendEventToServiceBus(ftEnvironment.Settings.Value.AzureConnectionSettings.ConnectionString, ftEnvironment.Settings.Value.AzureConnectionSettings.TopicName);
            serviceBus.Send(evt);

            return workout;
        }

        [FunctionName("SaveWorkout")]
        public static async Task<HttpResponseMessage> HttpStart(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req,
           [OrchestrationClient]DurableOrchestrationClient starter,
           ILogger log)
        {
            var workout = await req.Content.ReadAsAsync<WorkoutDTO>();   // passed by client

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("SaveWorkoutOrchestration", workout);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}