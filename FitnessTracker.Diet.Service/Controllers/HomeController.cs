using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Workout.Service.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}