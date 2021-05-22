using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleAPIApp.Business.Abstract;
using ArticleAPIApp.Business.Concrete;
using ArticleAPIApp.DataAccess.Abstract;
using ArticleAPIApp.DataAccess.Concrete.EfCore;
using ArticleAPIApp.DataAccess.Concrete.Memory;
using ArticleAPIApp.WebAPI.ConfigurationDtos;
using ArticleAPIApp.WebAPI.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ArticleAPIApp.WebAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IArticleDAL, MemoryArticleDAL>();
            services.AddScoped<IArticleDAL, EfCoreArticleDAL>();
            services.AddScoped<IAuthorDAL, EfCoreAuthorDAL>();
            services.AddScoped<ICategoryDAL, EfCoreCategoryDAL>();

            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddControllers();

            #region Options Pattern --appSettings'den gelen objenin instance'�n� olusturma
            //services.Configure<CustomTokenOption>(Configuration.GetSection("TokenOption"));
            var tokenOptions = Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
            #endregion

            #region Adding Identity
            services.AddDbContext<AppIdentityDbContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            #endregion

            #region Configure Identity(Register i�in yap�lan konfig�rasyonlar)
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequireUppercase = true;

                ////5 hak verilir belirli s�re ge�mesi laz�m
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                ////Yeni kullan�c� lockout ge�erli olur
                //options.Lockout.AllowedForNewUsers = true;
                ////User name i�in izin verilen karakterler
                //options.User.AllowedUserNameCharacters = "";
                ////unique email
                //options.User.RequireUniqueEmail = true;
                ////mail ile onay gerekir
                //options.SignIn.RequireConfirmedEmail = false;
                ////telefon onay�
                //options.SignIn.RequireConfirmedPhoneNumber = false;
            });
            #endregion


      

            #region Jwt Implementing
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "http://localhost:49983",
                        //ValidIssuer = tokenOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = "http://localhost:49983",
                        ////ValidAudience = tokenOptions.Audience.FirstOrDefault(),
                        ////IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("SecurityKey:securityKey")))
                        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
                        //ValidateIssuer = false,
                        //ValidateAudience = false

                        //ValidateIssuer ve ValidateAudience leri false ve Valid olan httpsleri de vermemi�ti �smail FreddoEcommerce projesinde. Bende bu �ekilde yapmak istedim ve false a �ektim loginden sonra token yarat�l�rken de ValidIssuer ve ValidAudience de�erlerini olu�trumad�m token da ancak s�rekli 401 unauthorized hatas� ald�m tan�mlay�nca d�zeldi.

                        //Account Controller da bir t�rl� Login e d��m�yor idi. Orada da Controller �zerine [AllowAnoymus] attrsi verdim d�zeldi.

                        //Bu projede UI Api ye her istek att���nda Headers de�erine bir ApiKey  ekliyor ve bu ApiKey de�erini Api Controllerlar� BaseController(BaseController da Controller dan miras al�r) dan miras alarak bu header de�erini kontrol ediyor.

                        //appSettings'den gelen objenin instance'�n� olusturma i�lemini yukar�da ger�ekle�tridik. Buradaki t�m de�erleri tek yerden y�netmek i�in kullanlabilir ki kullan�yorum. Ba�ka de�erler i�inde kullan�labilir.

                        //Identity ile userManager , roleManager gibi �zellikler ile kullan�c� logini ve haz�r �yelik i�in gerekli tablolar� login ve logut i�lemlerini y�netiyoruz. JWT ile Autharization ve Authentication i�lemlerini y�netiyoruz.


                        //Tokendaki Claimslerden de Role leri string role = ApiGlobal.GetRole(HttpContext); bu methodu kullanarak alabiliyoruz. [Authrorize] attrsinde token� de�i�tirip g�nderirsek zaten forbidden 403 yada 401 hatas� al�yoruz. Bu Attryi kald�r�p ApiGlobal den Claimsdeki de�erleri okumaya �al���rsak ve ayn� �ekilde token� de�i�tirip g�nderirsek claimsler null olarak atan�yor ve bu method bir role bulam�yor. Buradan da g�venlik alt�na alabiliyoruz Apilerimizi. CreateArticle EditArticle Delete lerde ArticleControllelar da �rnekleri var. Ayr�ca _Layout ta da bunu butonlar� saklamak i�in kulland�m. Denedim ama butonlar� Layout ta saklayamad�m. ��nk� View de g�nderdi�im Context nesnesi Role olarak UI da benim ekledi�im role olan clientAuth u ve di�er claimleri al�yor. GetRole e gitti�inde admin rol�n� bulam�yor. Apilere giden HttpContext nesnesinde admin rol� gidiyor. 2.1


                    };
                });
            #endregion

            //Swagger kullan�m� i�in SwashBuckle.AspNetCore inidirip bu ayar ile a�a��daki iki ayar� ekledik.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            #region JWT Using
            app.UseAuthentication();
            app.UseAuthorization();
            #endregion

            //Swagger kullan�m�
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
