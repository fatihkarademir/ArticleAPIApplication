using ArticleAPIApp.Entities.DTOs;
using ContentService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArticleAPIApp.MVC.UI.Controllers
{
    public class MemberController : UIBaseController
    {
        //ApiService _service;
        //public MemberController()
        //{
        //    _service = new ApiService(HttpContext);
        //}

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = _service.Login(new Entities.DTOs.LoginDTO() { UserName = loginDTO.Email, Email = loginDTO.Email, Password = loginDTO.Password });

            //CookieOptions cookie = new CookieOptions();
            //cookie.Expires = DateTime.Now.AddYears(1);
            //Response.Cookies.Append("token", result.Data.Token, cookie);

            //HttpContext.Session.SetString("token", result.Data.Token);
            if (result.IsSucces)
            {
                var claims = new List<Claim>
                {
                    //new Claim("Email", result.Data.Email),
                    new Claim("Token", result.Data.Token),
                    new Claim("IsAuth", result.Data.IsAuth.ToString()),
                    new Claim(ClaimTypes.Role, "clientAuth")
                };

                var userIdentity = new ClaimsIdentity(claims, "clientAuth");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return Redirect("/Home/Index");
            }
            return Redirect("/Home/Index");
        }
    }
}
