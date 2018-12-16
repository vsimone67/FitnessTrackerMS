using FitnessTracker.Application.Diet.Diet.MappingProfile;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Queries;
using FitnessTracker.Common.Serverless;
using FitnessTracker.Persistance.Diet;
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
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetMetabolicInfoCalcType/{type:alpha}")] HttpRequest req, string type, ILogger log, ExecutionContext context)
        {
            IActionResult retval;

            try
            {
                EnvironmentSetup<IDietService, DietDB> ftEnvironment = new EnvironmentSetup<IDietService, DietDB>(context.FunctionAppDirectory, DietMapperConfig.GetDietMapperConfig());
                var queryHanlder = new GetMetabolicInfoCalcTypeQueryHandler(ftEnvironment.Service, ftEnvironment.Mapper);
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