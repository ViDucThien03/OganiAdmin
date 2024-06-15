using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiAdmin.Models;

namespace OganiAdmin.Controllers
{
    public class ExportReportController : Controller
    {
        OganiContext data = new OganiContext();
        
        [HttpGet]
        public async Task<IActionResult> ExportDailyReport()
        {
            var dailyReport = await data.Orders
                .GroupBy(o => o.OrderDate.Value.Date)
                .Select(g => new
                {
                    OrderDate = g.Key,
                    OrderCount = g.Count(),
                    TotalRevenue = g.Sum(o => o.OrderTotalprice)
                })
                .OrderBy(r => r.OrderDate)
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Daily Report");
                worksheet.Cell(1, 1).Value = "Date";
                worksheet.Cell(1, 2).Value = "Order Count";
                worksheet.Cell(1, 3).Value = "Total Revenue";

                for (int i = 0; i < dailyReport.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = dailyReport[i].OrderDate.ToShortDateString();
                    worksheet.Cell(i + 2, 2).Value = dailyReport[i].OrderCount;
                    worksheet.Cell(i + 2, 3).Value = dailyReport[i].TotalRevenue;
                }

                using (var stream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DailyReport.xlsx");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExportMonthlyReport()
        {
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

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Monthly Report");
                worksheet.Cell(1, 1).Value = "Year";
                worksheet.Cell(1, 2).Value = "Month";
                worksheet.Cell(1, 3).Value = "Order Count";
                worksheet.Cell(1, 4).Value = "Total Revenue";

                for (int i = 0; i < monthlyReport.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = monthlyReport[i].OrderYear;
                    worksheet.Cell(i + 2, 2).Value = monthlyReport[i].OrderMonth;
                    worksheet.Cell(i + 2, 3).Value = monthlyReport[i].OrderCount;
                    worksheet.Cell(i + 2, 4).Value = monthlyReport[i].TotalRevenue;
                }

                using (var stream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MonthlyReport.xlsx");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExportYearlyReport()
        {
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

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Yearly Report");
                worksheet.Cell(1, 1).Value = "Year";
                worksheet.Cell(1, 2).Value = "Order Count";
                worksheet.Cell(1, 3).Value = "Total Revenue";

                for (int i = 0; i < yearlyReport.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = yearlyReport[i].OrderYear;
                    worksheet.Cell(i + 2, 2).Value = yearlyReport[i].OrderCount;
                    worksheet.Cell(i + 2, 3).Value = yearlyReport[i].TotalRevenue;
                }

                using (var stream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "YearlyReport.xlsx");
                }
            }
        }
    }
}
