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
    public static class GetMetabolicInfoCalcType
    {
        [FunctionName("GetMetabolicInfoCalcType")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetMetabolicInfoCalcType/{type:alpha}")] HttpRequest req, string type, ILogger log, ExecutionContext context)
        {
            IActionResult retval;

            try
            {
                EnvironmentSetup ftEnvironment = new EnvironmentSetup(context.FunctionAppDirectory);
                var queryHanlder = new GetMetabolicInfoCalcTypeQueryHandler(ftEnvironment.DietService, ftEnvironment.Mapper);
                var results = queryHanlder.Handle(new GetMetabolicInfoCalcTypeQuery() { Id = type });
                retval = new OkObjectResult(JsonConvert.SerializeObject(results)); // the reason why we are using jsonconvert and not passing the object directly to OKObjectResult is the json the OKObjectResult method transforms ais all lowercase we need it to be in the form of the DTO
            }
            catch (Exception ex)
            {
                retval = new BadRequestObjectResult(ex.Message);
            }

            return retval;
        }
    }
}