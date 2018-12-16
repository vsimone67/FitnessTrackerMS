using FitnessTracker.Application.Command;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Application.Model.Diet.Events;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FitnessTracker.Serverless.Diet
{
    public static class SaveMenu
    {
        [FunctionName("SaveMenuOrchestration")]
        public static async Task RunOrchestrator(
          [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var nutritionInfo = await context.CallActivityAsync<List<NutritionInfoDTO>>("SaveMenuData", context.GetInput<List<NutritionInfoDTO>>());
            await context.CallActivityAsync<List<NutritionInfoDTO>>("SaveMenuToSB", nutritionInfo);
        }

        [FunctionName("SaveMenuData")]
        public static List<NutritionInfoDTO> SaveToDB([ActivityTrigger] List<NutritionInfoDTO> nutritionInfo, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup ftEnvironment = new EnvironmentSetup(context.FunctionAppDirectory);
            var queryHanlder = new SaveMenuCommandHandler(ftEnvironment.DietService, ftEnvironment.Mapper);
            return queryHanlder.Handle(new SaveMenuCommand() { Menu = nutritionInfo });
        }

        [FunctionName("SaveMenuToSB")]
        public static List<NutritionInfoDTO> SendToSB([ActivityTrigger] List<NutritionInfoDTO> nutritionInfo, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup ftEnvironment = new EnvironmentSetup(context.FunctionAppDirectory);

            var evt = new SaveMenuEvent
            {
                SavedMenu = nutritionInfo
            };
            SendEventToServiceBus serviceBus = new SendEventToServiceBus(ftEnvironment.Settings.Value.AzureConnectionSettings.ConnectionString, ftEnvironment.Settings.Value.AzureConnectionSettings.TopicName);
            serviceBus.Send(evt);

            return nutritionInfo;
        }

        [FunctionName("SaveMenu")]
        public static async Task<HttpResponseMessage> HttpStart(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req,
           [OrchestrationClient]DurableOrchestrationClient starter,
           ILogger log)
        {
            var nutritionInfo = await req.Content.ReadAsAsync<List<NutritionInfoDTO>>();   // passed by client

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("SaveMenuOrchestration", nutritionInfo);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}