using FitnessTracker.Application.Diet.Command;
using FitnessTracker.Application.Diet.Diet.MappingProfile;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Application.Model.Diet.Events;
using FitnessTracker.Common.Serverless;
using FitnessTracker.Persistance.Diet;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace FitnessTracker.Serverless.Diet
{
    public static class ProcessItem
    {
        [FunctionName("ProcessItemOrchestration")]
        public static async Task RunOrchestrator(
          [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var foodInfo = await context.CallActivityAsync<FoodInfoDTO>("ProcessItemData", context.GetInput<FoodInfoDTO>());
            await context.CallActivityAsync<FoodInfoDTO>("ProcessItemToSB", foodInfo);
        }

        [FunctionName("ProcessItemData")]
        public static FoodInfoDTO SaveToDB([ActivityTrigger] FoodInfoDTO foodInfo, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup<IDietService, DietDB> ftEnvironment = new EnvironmentSetup<IDietService, DietDB>(context.FunctionAppDirectory, DietMapperConfig.GetDietMapperConfig());
            var queryHanlder = new ProcessItemCommandHandler(ftEnvironment.Service, ftEnvironment.Mapper);
            return queryHanlder.Handle(new ProcessItemCommand() { FoodInfo = foodInfo });
        }

        [FunctionName("ProcessItemToSB")]
        public static FoodInfoDTO SendToSB([ActivityTrigger] FoodInfoDTO foodInfo, ILogger log, ExecutionContext context)
        {
            EnvironmentSetup<IDietService, DietDB> ftEnvironment = new EnvironmentSetup<IDietService, DietDB>(context.FunctionAppDirectory, DietMapperConfig.GetDietMapperConfig());

            var evt = new AddNewFoodEvent
            {
                AddedFoodItem = foodInfo
            };
            SendEventToServiceBus serviceBus = new SendEventToServiceBus(ftEnvironment.Settings.Value.AzureConnectionSettings.ConnectionString, ftEnvironment.Settings.Value.AzureConnectionSettings.TopicName);
            serviceBus.Send(evt);

            return foodInfo;
        }

        [FunctionName("ProcessItem")]
        public static async Task<HttpResponseMessage> HttpStart(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req,
           [OrchestrationClient]DurableOrchestrationClient starter,
           ILogger log)
        {
            var foodInfo = await req.Content.ReadAsAsync<FoodInfoDTO>();   // passed by client

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("ProcessItemOrchestration", foodInfo);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}