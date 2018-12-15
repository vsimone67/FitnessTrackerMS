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
        public static BodyInfoDTO SaveToDB([ActivityTrigger] BodyInfoDTO bodyInfo, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup ftEnvironment = new EnvironmentSetup(context.FunctionAppDirectory);
            var commandHanlder = new SaveBodyInfoCommandHandler(ftEnvironment.WorkoutSerivce, ftEnvironment.Mapper);
            return commandHanlder.Handle(new SaveBodyInfoCommand() { BodyInfo = bodyInfo });
        }

        [FunctionName("SaveBodyInfoToSB")]
        public static BodyInfoDTO SendToSB([ActivityTrigger] BodyInfoDTO bodyInfo, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup ftEnvironment = new EnvironmentSetup(context.FunctionAppDirectory);

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