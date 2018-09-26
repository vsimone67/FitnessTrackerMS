using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.WebStatus.BackgroundProcesses
{
    public class WebStatusHostedService : IHostedService
    {
        private readonly ILogger<WebStatusHostedService> logger;

        public WebStatusHostedService(ILogger<WebStatusHostedService> logger)
        {
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // this is an example of how to run a background process in .NET core
            logger.LogInformation("Hosted service starting");

            return Task.Factory.StartNew(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    logger.LogInformation("Hosted service executing - {0}", DateTime.Now);
                    try
                    {
                        await Task.Delay(TimeSpan.FromSeconds(360), cancellationToken);
                    }
                    catch (OperationCanceledException) { }
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Hosted service stopping");
            return Task.CompletedTask;
        }
    }
}