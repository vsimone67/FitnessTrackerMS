using FitnessTracker.Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FitnessTracker.Serverless.Diet
{
    public static class GetSavedMenuItems
    {
        [FunctionName("GetSavedMenuItems")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            IActionResult retval;

            try
            {
                EnvironmentSetup ftEnvironment = new EnvironmentSetup(context.FunctionAppDirectory);
                var queryHanlder = new GetSavedMenuItemsQueryHandler(ftEnvironment.DietService, ftEnvironment.Mapper);
                var results = queryHanlder.Handle(new GetSavedMenuItemsQuery());

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