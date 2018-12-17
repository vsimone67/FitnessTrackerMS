using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FitnessTracker.Common.Web.Middleware
{
    public class RequestTimingsMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingsMiddleWare> _logger;

        public RequestTimingsMiddleWare(RequestDelegate next, ILogger<RequestTimingsMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var s = new Stopwatch();
            s.Start();
            await _next(context).ConfigureAwait(false);
            s.Stop();

            TimeSpan timeSpan = s.Elapsed;
            _logger.LogTrace($"Elapsed Time: {timeSpan.Minutes}:{timeSpan.Seconds}, URL: {context.Request.GetDisplayUrl()}");
        }
    }
}