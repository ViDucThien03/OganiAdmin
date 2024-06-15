using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OganiAdmin.Models;

namespace OganiAdmin.Controllers
{
    public class ReportController : Controller
    {
        private OganiContext data = new OganiContext();
        public async Task<IActionResult> Index()
        {
            var dailyReport = await data.Orders
                .GroupBy(o => o.OrderDate.Value.Date)
                //.GroupBy(o => new { o.OrderDate.Value.Date })
                .Select(g => new
                {
                    OrderDate = g.Key,
                    OrderCount = g.Count(),
                   
                    TotalRevenue = g.Sum(o => o.OrderTotalprice)
                })
                .OrderBy(r => r.OrderDate)
                .ToListAsync();
              
            var monthlyReport = await data.Orders
                .GroupBy(o => new { o.OrderDate.Value.Year, o.OrderDate.Value.Month })
                .Select(g => new
                {
                    OrderYear = g.Key.Year,
                    OrderMonth = g.Key.Month,
                    OrderCount = g.Count(),
                    TotalRevenue = g.Sum(o => o.OrderTotalprice)
                })
                .OrderBy(r => r.OrderYear)
                .ThenBy(r => r.OrderMonth)
                .ToListAsync();

            var yearlyReport = await data.Orders
                .GroupBy(o => o.OrderDate.Value.Year)
                .Select(g => new
                {
                    OrderYear = g.Key,
                    OrderCount = g.Count(),
                    TotalRevenue = g.Sum(o => o.OrderTotalprice)
                })
                .OrderBy(r => r.OrderYear)
                .ToListAsync();

            ViewData["DailyReport"] = dailyReport;
            ViewData["MonthlyReport"] = monthlyReport;
            ViewData["YearlyReport"] = yearlyReport;


            return View();
        }
    }
}
