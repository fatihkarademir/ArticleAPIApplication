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

            #region Options Pattern --appSettings'den gelen objenin instance'ýný olusturma
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

            #region Configure Identity(Register için yapýlan konfigürasyonlar)
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequireUppercase = true;

                ////5 hak verilir belirli süre geçmesi lazým
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                ////Yeni kullanýcý lockout geçerli olur
                //options.Lockout.AllowedForNewUsers = true;
                ////User name için izin verilen karakterler
                //options.User.AllowedUserNameCharacters = "";
                ////unique email
                //options.User.RequireUniqueEmail = true;
                ////mail ile onay gerekir
                //options.SignIn.RequireConfirmedEmail = false;
                ////telefon onayý
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

                        //ValidateIssuer ve ValidateAudience leri false ve Valid olan httpsleri de vermemiþti Ýsmail FreddoEcommerce projesinde. Bende bu þekilde yapmak istedim ve false a çektim loginden sonra token yaratýlýrken de ValidIssuer ve ValidAudience deðerlerini oluþtrumadým token da ancak sürekli 401 unauthorized hatasý aldým tanýmlayýnca düzeldi.

                        //Account Controller da bir türlü Login e düþmüyor idi. Orada da Controller üzerine [AllowAnoymus] attrsi verdim düzeldi.

                        //Bu projede UI Api ye her istek attýðýnda Headers deðerine bir ApiKey  ekliyor ve bu ApiKey deðerini Api Controllerlarý BaseController(BaseController da Controller dan miras alýr) dan miras alarak bu header deðerini kontrol ediyor.

                        //appSettings'den gelen objenin instance'ýný olusturma iþlemini yukarýda gerçekleþtridik. Buradaki tüm deðerleri tek yerden yönetmek için kullanlabilir ki kullanýyorum. Baþka deðerler içinde kullanýlabilir.

                        //Identity ile userManager , roleManager gibi özellikler ile kullanýcý logini ve hazýr üyelik için gerekli tablolarý login ve logut iþlemlerini yönetiyoruz. JWT ile Autharization ve Authentication iþlemlerini yönetiyoruz.


                        //Tokendaki Claimslerden de Role leri string role = ApiGlobal.GetRole(HttpContext); bu methodu kullanarak alabiliyoruz. [Authrorize] attrsinde tokený deðiþtirip gönderirsek zaten forbidden 403 yada 401 hatasý alýyoruz. Bu Attryi kaldýrýp ApiGlobal den Claimsdeki deðerleri okumaya çalýþýrsak ve ayný þekilde tokený deðiþtirip gönderirsek claimsler null olarak atanýyor ve bu method bir role bulamýyor. Buradan da güvenlik altýna alabiliyoruz Apilerimizi. CreateArticle EditArticle Delete lerde ArticleControllelar da örnekleri var. Ayrýca _Layout ta da bunu butonlarý saklamak için kullandým. Denedim ama butonlarý Layout ta saklayamadým. Çünkü View de gönderdiðim Context nesnesi Role olarak UI da benim eklediðim role olan clientAuth u ve diðer claimleri alýyor. GetRole e gittiðinde admin rolünü bulamýyor. Apilere giden HttpContext nesnesinde admin rolü gidiyor. 2.1


                    };
                });
            #endregion

            //Swagger kullanýmý için SwashBuckle.AspNetCore inidirip bu ayar ile aþaðýdaki iki ayarý ekledik.
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

            //Swagger kullanýmý
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
