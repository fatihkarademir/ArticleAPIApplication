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

            //Session kullanabilmek i�in bu ayar ile Configure daki app.UserSession() � ekledik. Ayr�ca Nugettan Microsoft.AspNetCore.Session � ekledik.1.0
            //services.AddSession();
            //services.AddScoped<IAuthorDAL, EfCoreAuthorDAL>();
            //services.AddScoped<ICategoryDAL, EfCoreCategoryDAL>();
            //services.AddScoped<IAuthorService, AuthorService>();
            //services.AddScoped<ICategoryService, CategoryService>();
            //services.AddSingleton<ContentService>(new ContentService().getInstance());
            //Session kullanabilmek i�in bu ayar ile Configure daki app.UserSession() � ekledik. Ayr�ca Nugettan Microsoft.AspNetCore.Session � ekledik.1.1
            //services.AddAuthentication();


            // �smailin HttpContext.SignInAsync methoduyla birlikte kullan�labilir oluyor. 2.0
            //if (HttpContext.User.Identity.IsAuthenticated)
            //{

            //}

            //Tokendaki Claimslerden de Role leri string role = ApiGlobal.GetRole(HttpContext); bu methodu kullanarak alabiliyoruz. [Authrorize] attrsinde token� de�i�tirip g�nderirsek zaten forbidden 403 yada 401 hatas� al�yoruz. Bu Attryi kald�r�p ApiGlobal den Claimsdeki de�erleri okumaya �al���rsak ve ayn� �ekilde token� de�i�tirip g�nderirsek claimsler null olarak atan�yor ve bu method bir role bulam�yor. Buradan da g�venlik alt�na alabiliyoruz Apilerimizi. CreateArticle EditArticle Delete lerde ArticleControllelar da �rnekleri var. Ayr�ca _Layout ta da bunu butonlar� saklamak i�in kulland�m. 2.1


            //�smailin SignInAsync Methodunu kullanabilmek i�in bu ayarlar� yap�yoruz 2.2
            // if(HttpContext.User.Identity.IsAuthenticated) bunu kullanabiliyoruz.
            // Authorize attrsini de kullanabiliriz. Login methodunda Apiye gidip do�rulan�rsa kullan�c� SignInAsnyc oluyor ve HttpCpntext e yaz�l�yor. Authorize attrsi bu ayarlarla birlikte cookie ye login olundu�u i�in kontrol edip e�er kullan�c� login olamaz ise Member/Login e y�nlendirir. Bu �ekilde Api den tekrar tekrar do�rulama yapmadan da y�netim yapabiliriz.
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
            //cookie kullanabilmek i�in yeterli
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
