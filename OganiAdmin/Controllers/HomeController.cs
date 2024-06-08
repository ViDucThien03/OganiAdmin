using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiAdmin.Models;
using System.Diagnostics;
using X.PagedList;

namespace OganiAdmin.Controllers
{
    public class HomeController : Controller
    {
        OganiContext data = new OganiContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult Privacy()
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
