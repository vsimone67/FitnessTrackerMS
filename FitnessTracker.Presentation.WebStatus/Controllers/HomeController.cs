using FitnessTracker.Presentation.WebStatus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.WebStatus.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHealthCheckPublisher _healthCheckSvc;
        private readonly ILogger _logger;

        public HomeController(IHealthCheckPublisher checkSvc, ILogger<HomeController> logger)
        {
            _healthCheckSvc = checkSvc;
            _logger = logger;
        }

        //public async Task<IActionResult> Index()
        //{
        //    _logger.LogInformation("Checking Health Status.....");

        //    var result = await _healthCheckSvc..CheckHealthAsync();

        //    var data = new HealthStatusViewModel(result.CheckStatus);

        //    foreach (var checkResult in result.Results)
        //    {
        //        data.AddResult(checkResult.Key, checkResult.Value);
        //    }

        //    ViewBag.RefreshSeconds = 60;

        //    return View(data);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}