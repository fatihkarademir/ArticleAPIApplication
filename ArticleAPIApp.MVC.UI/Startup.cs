using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleAPIApp.Business.Abstract;
using ArticleAPIApp.Business.Concrete;
using ArticleAPIApp.DataAccess.Abstract;
using ArticleAPIApp.DataAccess.Concrete.EfCore;
using ArticleAPIApp.MVC.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArticleAPIApp.MVC.UI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc(option => option.EnableEndpointRouting = false);

            //Session kullanabilmek için bu ayar ile Configure daki app.UserSession() ý ekledik. Ayrýca Nugettan Microsoft.AspNetCore.Session ý ekledik.
            //services.AddSession();
            //services.AddScoped<IAuthorDAL, EfCoreAuthorDAL>();
            //services.AddScoped<ICategoryDAL, EfCoreCategoryDAL>();
            //services.AddScoped<IAuthorService, AuthorService>();
            //services.AddScoped<ICategoryService, CategoryService>();
            //services.AddSingleton<ContentService>(new ContentService().getInstance());

            //services.AddAuthentication();


            // Ýsmailin HttpContext.SignInAsync methoduyla birlikte kullanýlabilir oluyor.
            //if (HttpContext.User.Identity.IsAuthenticated)
            //{

            //}

            //Tokendaki Claimslerden de Role leri string role = ApiGlobal.GetRole(HttpContext); bu methodu kullanarak alabiliyoruz. [Authrorize] attrsinde tokený deðiþtirip gönderirsek zaten forbidden 403 yada 401 hatasý alýyoruz. Bu Attryi kaldýrýp ApiGlobal den Claimsdeki deðerleri okumaya çalýþýrsak ve ayný þekilde tokený deðiþtirip gönderirsek claimsler null olarak atanýyor ve bu method bir role bulamýyor. Buradan da güvenlik altýna alabiliyoruz Apilerimizi.


            //Ýsmailin SignInAsync Methodunu kullanabilmek için bu ayarlarý yapýyoruz
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.LoginPath = "/Member/Login";
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                 options.Cookie.IsEssential = true;
             });
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                options.Cookie.IsEssential = true;
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(50);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseSession();
            //cookie kullanabilmek için yeterli
            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{Id?}"
                );
            });
        }
    }
}
