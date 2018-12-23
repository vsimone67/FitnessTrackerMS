using FitnessTracker.Application.Diet.Diet.MappingProfile;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Diet.Queries;
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
    public static class GetColumns
    {
        [FunctionName("GetColumns")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            IActionResult retval;

            try
            {
                EnvironmentSetup<IDietRepository, DietRepository> ftEnvironment = new EnvironmentSetup<IDietRepository, DietRepository>(context.FunctionAppDirectory, DietMapperConfig.GetDietMapperConfig());
                var queryHanlder = new GetColumnsQueryHandler(ftEnvironment.Service, ftEnvironment.Mapper);
                var results = queryHanlder.Handle(new GetColumnsQuery(), new System.Threading.CancellationToken());

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