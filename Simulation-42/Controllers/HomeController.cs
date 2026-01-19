using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Simulation_42.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
