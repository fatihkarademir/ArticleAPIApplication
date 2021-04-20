using ArticleAPIApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleAPIApp.DataAccess.Concrete.EfCore
{
    public class ArticleAppContext : DbContext
    {
        //private IConfiguration Configuration;
        //public ArticleAppContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-BTUMAUU\SQLEXPRESS;Database=ArticleAPIDb;integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
