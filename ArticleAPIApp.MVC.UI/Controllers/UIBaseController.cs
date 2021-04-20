using ContentService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleAPIApp.MVC.UI.Controllers
{
    public class UIBaseController : Controller
    {
        protected ApiService _service;
        public override void OnActionExecuting(ActionExecutingContext context)

        {
            if (_service == null)
            {
                _service = new ApiService(HttpContext);
            }
            var token = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
            if (token == null)
            {
                //var result = _service.NonMemberShip();
                //var claims = new List<Claim>
                //{
                //    new Claim("Email", result.Data.Email),
                //    new Claim("Token", result.Data.Token),
                //    new Claim("IsAuth", result.Data.IsAuth.ToString()),
                //    new Claim(ClaimTypes.Role, "clientAuth")
                //};

                //var userIdentity = new ClaimsIdentity(claims, "clientAuth");
                //ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                //HttpContext.SignInAsync(principal);
            }
        }
    }
}
