using ArticleAPIApp.Entities.DTOs;
using ContentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Login(LoginDTO loginDTO)
        {
            var result = _service.Login(new Entities.DTOs.LoginDTO() { UserName = loginDTO.Email, Email = loginDTO.Email, Password = loginDTO.Password });

            //CookieOptions cookie = new CookieOptions();
            //cookie.Expires = DateTime.Now.AddYears(1);
            //Response.Cookies.Append("token", result.Data.Token, cookie);

            HttpContext.Session.SetString("token", result.Data.Token);

            return Redirect("/Home/Index");
        }
    }
}
