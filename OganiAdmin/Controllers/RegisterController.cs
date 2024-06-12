using Microsoft.AspNetCore.Mvc;
using OganiAdmin.Models;

namespace OganiAdmin.Controllers
{
    public class RegisterController : Controller
    {
        OganiContext db = new OganiContext();
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Customer cus)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Customers.FirstOrDefault(x => x.CusEmail == cus.CusEmail);
                if (existingUser == null)
                {

                    db.Customers.Add(cus);
                    db.SaveChanges();
                    HttpContext.Session.SetString("UserName", cus.CusEmail);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email đã tồn tại");
                }
            }
            return View(cus);
        }
    }
}
