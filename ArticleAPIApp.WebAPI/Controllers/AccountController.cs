using ArticleAPIApp.Entities;
using ArticleAPIApp.WebAPI.ConfigurationDtos;
using ArticleAPIApp.WebAPI.Identity;
using ArticleAPIApp.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ArticleAPIApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AccountController : BaseController //BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private CustomTokenOption _tokenOptions { get; }
        //private readonly CustomTokenOption _tokenOption;

        public AccountController(UserManager<IdentityUser> userManager,IConfiguration configuration, SignInManager<IdentityUser> signInManager)//,IOptions<CustomTokenOption> options
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOption>();
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ServiceResult<AutResponse>> Login([FromBody] LoginModel model)
        {
            //var user = await _userManager.FindByNameAsync(model.UserName);

            //if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            if (model.UserName == "fatih@asd" && model.Password == "123")
            {
                //var userRoles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>
                {
                    //new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sub,model.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role,"admin")
                    //new Claim(ClaimTypes.Role,"uye")
                };

                //foreach (var role in userRoles)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role,role));
                //}


                //TO DO : Configurationdan secretkey al
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("SecurityKey:securityKey")));

                var token = new JwtSecurityToken(
                    issuer: "http://localhost:49983",
                    audience: "http://localhost:49983",
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

                //_signInManager.PasswordSignInAsync(model.UserName)

                return new ServiceResult<AutResponse>()
                {
                    IsSucces = true,
                    Message = "Giris Basarili",
                    Data = new AutResponse()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        IsAuth = true,
                        Message = "Giris Basarili"
                    }

                };

                //return Ok(new
                //{
                //    token = new JwtSecurityTokenHandler().WriteToken(token),
                //    expiration = token.ValidTo,
                //    message = "Giris Basarili"
                //});
            }

            //return BadRequest(new
            //{
            //    message = "Kullanici Adi veya Sifre yanlıs"
            //});

            return new ServiceResult<AutResponse>()
            {
                IsSucces = false,
                Message = "Giris Basarisiz",
                Data = new AutResponse()
                {
                    IsAuth = false,
                    Message = "Giris Basarisiz"
                }

            };
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // generate token
                // send email
                return RedirectToAction("account", "login");
            }

            ViewBag.ErrorMessage = result.Errors.Select(i => i.Description).ToList();

            return View(model);
        }

       


        #region Private Methods
        private JwtSecurityToken CreateToken(ApplicationUser user)
        {
            //yukarıda yada bunu kullanabilirsin

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            var token = new JwtSecurityToken(
                   issuer: _tokenOptions.Issuer,
                   audience: _tokenOptions.Audience.FirstOrDefault(),
                   expires: DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration),
                   claims: claims,
                   signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
        #endregion
    }
}
