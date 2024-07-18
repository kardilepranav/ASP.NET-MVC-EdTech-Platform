using Edtech.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Edtech.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DashBoard()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Team()
        {
            return View();
        }
        public IActionResult Testimonial()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
