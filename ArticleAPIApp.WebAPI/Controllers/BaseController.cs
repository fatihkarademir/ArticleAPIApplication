using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleAPIApp.WebAPI.Controllers
{

    public class BaseController : Controller
    {
        string Key = "af8a4c28-9906-48a4-ae6c-c9630fcf901e";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _action = context.ActionDescriptor.RouteValues["action"].ToLower();
            var _controller = context.ActionDescriptor.RouteValues["controller"].ToLower();
            var requestHeader = context.HttpContext.Request.Headers["ApiKey"];
            if (requestHeader.ToString() != Key)
            {
                context.Result = new RedirectToRouteResult(
                 new RouteValueDictionary(new { controller = "NewApi", action = "NoAuth" }));
            }
        }
    }
}
