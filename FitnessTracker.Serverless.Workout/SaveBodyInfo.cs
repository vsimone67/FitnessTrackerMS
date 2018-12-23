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
    public static class SaveBodyInfo
    {
        [FunctionName("SaveBodyInfoOrchestration")]
        public static async Task RunOrchestrator(
           [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var bodyInfo = await context.CallActivityAsync<BodyInfoDTO>("SaveBodyInfoData", context.GetInput<BodyInfoDTO>());
            await context.CallActivityAsync<BodyInfoDTO>("SaveBodyInfoToSB", bodyInfo);
        }

        [FunctionName("SaveBodyInfoData")]
        public async static Task<BodyInfoDTO> SaveToDB([ActivityTrigger] BodyInfoDTO bodyInfo, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup<IWorkoutRepository, WorkoutRepository> ftEnvironment = new EnvironmentSetup<IWorkoutRepository, WorkoutRepository>(context.FunctionAppDirectory, WorkoutMapperConfig.GetWorkoutMapperConfig());
            var commandHanlder = new SaveBodyInfoCommandHandler(ftEnvironment.Service, ftEnvironment.Mapper);
            return await commandHanlder.Handle(new SaveBodyInfoCommand() { BodyInfo = bodyInfo }, new System.Threading.CancellationToken());
        }

        [FunctionName("SaveBodyInfoToSB")]
        public static BodyInfoDTO SendToSB([ActivityTrigger] BodyInfoDTO bodyInfo, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup<IWorkoutRepository, WorkoutRepository> ftEnvironment = new EnvironmentSetup<IWorkoutRepository, WorkoutRepository>(context.FunctionAppDirectory, WorkoutMapperConfig.GetWorkoutMapperConfig());

            var evt = new BodyInfoSavedEvent
            {
                SavedBodyInfo = bodyInfo
            };

            SendEventToServiceBus serviceBus = new SendEventToServiceBus(ftEnvironment.Settings.Value.AzureConnectionSettings.ConnectionString, ftEnvironment.Settings.Value.AzureConnectionSettings.TopicName);
            serviceBus.Send(evt);

            return bodyInfo;
        }

        [FunctionName("SaveBodyInfo")]
        public static async Task<HttpResponseMessage> HttpStart(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req,
           [OrchestrationClient]DurableOrchestrationClient starter,
           ILogger log)
        {
            var bodyInfo = await req.Content.ReadAsAsync<BodyInfoDTO>();   // passed by client

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("SaveBodyInfoOrchestration", bodyInfo);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}