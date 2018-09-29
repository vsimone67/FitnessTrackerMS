using FitnessTracker.Common.HTTP;
using FitnessTracker.Presentation.WebStatus.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.WebStatus.BackgroundProcesses
{
    public class WebStatusHostedService : IHostedService
    {
        private readonly ILogger<WebStatusHostedService> _logger;
        private readonly IOptions<KeepAlive> _pingSettings;

        public WebStatusHostedService(ILogger<WebStatusHostedService> logger, IOptions<KeepAlive> pingSettings)
        {
            _logger = logger;
            _pingSettings = pingSettings;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // this is an example of how to run a background process in .NET core
            _logger.LogInformation("Keep ALive Process Starting......");

            return Task.Factory.StartNew(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"Pinging Servers - {DateTime.Now}");

                    try
                    {
                        await PingServerAsync(_pingSettings.Value.WorkoutServiceURL);
                        await PingServerAsync(_pingSettings.Value.DietServiceURL);
                        await Task.Delay(TimeSpan.FromSeconds(_pingSettings.Value.WakeupInterval), cancellationToken);
                    }
                    catch (OperationCanceledException) { }
                }
            }, cancellationToken);
        }

        public async Task PingServerAsync(string URL)
        {
            var json = await HttpHelper.GetAsync<List<dynamic>>(URL);

            if (json.Count == 0)
                _logger.LogError($"Site {URL} is not returning any data.....");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted service stopping");
            return Task.CompletedTask;
        }
    }
}