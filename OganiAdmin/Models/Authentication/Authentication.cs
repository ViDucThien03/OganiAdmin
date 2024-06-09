using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OganiAdmin.Models.Authentication
{
    public class Authentication:Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("UserName") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller","Access" },
                        {"Action","login" }
                    });
            }
        }
    }
}
