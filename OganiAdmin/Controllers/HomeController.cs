using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiAdmin.Models;
using OganiAdmin.Models.Authentication;
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
        [Authentication]
        public IActionResult Index()
        {
            return View();
        }
        [Authentication]
        public IActionResult Products()
        {
            return View();
        }
        [Authentication]
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
