using FitnessTracker.Application.Command;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Application.Model.Diet.Events;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace FitnessTracker.Serverless.Diet
{
    public static class EditMetabolicInfoFunc
    {
        [FunctionName("EditMetabolicInfoOrchestration")]
        public static async Task RunOrchestrator(
          [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var metabolicInfo = await context.CallActivityAsync<MetabolicInfoDTO>("EditMetabolicInfoData", context.GetInput<MetabolicInfoDTO>());
            await context.CallActivityAsync<MetabolicInfoDTO>("EditMetabolicInfoToSB", metabolicInfo);
        }

        [FunctionName("EditMetabolicInfoData")]
        public static MetabolicInfoDTO SaveToDB([ActivityTrigger] MetabolicInfoDTO metabolicInfo, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup ftEnvironment = new EnvironmentSetup(context.FunctionAppDirectory);
            var queryHanlder = new EditMetabolicInfoCommandHandler(ftEnvironment.DietService, ftEnvironment.Mapper);
            return queryHanlder.Handle(new EditMetabolicInfoCommand() { MetabolicInfo = metabolicInfo });
        }

        [FunctionName("EditMetabolicInfoToSB")]
        public static MetabolicInfoDTO SendToSB([ActivityTrigger] MetabolicInfoDTO metabolicInfo, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup ftEnvironment = new EnvironmentSetup(context.FunctionAppDirectory);
            var evt = new EditMetabolicInfo
            {
                EditedMetabolicInfo = metabolicInfo
            };

            SendEventToServiceBus serviceBus = new SendEventToServiceBus(ftEnvironment.Settings.Value.AzureConnectionSettings.ConnectionString, ftEnvironment.Settings.Value.AzureConnectionSettings.TopicName);
            serviceBus.Send(evt);

            return metabolicInfo;
        }

        [FunctionName("EditMetabolicInfo")]
        public static async Task<HttpResponseMessage> HttpStart(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req,
           [OrchestrationClient]DurableOrchestrationClient starter,
           ILogger log)
        {
            var metabolicInfo = await req.Content.ReadAsAsync<MetabolicInfoDTO>();   // passed by client

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("EditMetabolicInfoOrchestration", metabolicInfo);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}