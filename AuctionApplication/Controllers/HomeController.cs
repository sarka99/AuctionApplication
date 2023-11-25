using AuctionApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuctionApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() {
            //action för o gå in till index sidan
            return View();
        }

        public IActionResult Privacy()
        {
            //action för o gå in till privacy sidan

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}