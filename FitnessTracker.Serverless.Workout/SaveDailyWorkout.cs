using FitnessTracker.Application.Workout.Command;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Events;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Application.Workout.Workout.MappingProfile;
using FitnessTracker.Common.Serverless;
using FitnessTracker.Persistance.Workout;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace FitnessTracker.Serverless.Workout
{
    public static class SavesavedWorkout
    {
        [FunctionName("SaveDailyWorkoutOrchestration")]
        public static async Task RunOrchestrator(
          [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var savedWorkout = await context.CallActivityAsync<DailyWorkoutDTO>("SaveDailyWorkoutData", context.GetInput<WorkoutDisplayDTO>());
            await context.CallActivityAsync<DailyWorkoutDTO>("SaveDailyWorkoutToSB", savedWorkout);
        }

        [FunctionName("SaveDailyWorkoutData")]
        public static DailyWorkoutDTO SaveToDB([ActivityTrigger] WorkoutDisplayDTO savedWorkout, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup<IWorkoutService, WorkoutDB> ftEnvironment = new EnvironmentSetup<IWorkoutService, WorkoutDB>(context.FunctionAppDirectory, WorkoutMapperConfig.GetWorkoutMapperConfig());
            var commandHanlder = new SaveDailyWorkoutCommandHandler(ftEnvironment.Service, ftEnvironment.Mapper);
            return commandHanlder.Handle(new SaveDailyWorkoutCommand() { Workout = savedWorkout });
        }

        [FunctionName("SaveDailyWorkoutToSB")]
        public static DailyWorkoutDTO SendToSB([ActivityTrigger] DailyWorkoutDTO savedWorkout, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup<IWorkoutService, WorkoutDB> ftEnvironment = new EnvironmentSetup<IWorkoutService, WorkoutDB>(context.FunctionAppDirectory, WorkoutMapperConfig.GetWorkoutMapperConfig());

            var evt = new WorkoutCompletedEvent
            {
                CompletedWorkout = savedWorkout
            };

            SendEventToServiceBus serviceBus = new SendEventToServiceBus(ftEnvironment.Settings.Value.AzureConnectionSettings.ConnectionString, ftEnvironment.Settings.Value.AzureConnectionSettings.TopicName);
            serviceBus.Send(evt);

            return savedWorkout;
        }

        [FunctionName("SaveDailyWorkout")]
        public static async Task<HttpResponseMessage> HttpStart(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req,
           [OrchestrationClient]DurableOrchestrationClient starter,
           ILogger log)
        {
            var savedWorkout = await req.Content.ReadAsAsync<WorkoutDisplayDTO>();   // passed by client

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("SaveDailyWorkoutOrchestration", savedWorkout);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}