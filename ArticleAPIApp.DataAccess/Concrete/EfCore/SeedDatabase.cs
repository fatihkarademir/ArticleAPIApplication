using ArticleAPIApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArticleAPIApp.DataAccess.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ArticleAppContext();
            //context üzerinden database özelliğine geçip database'e uygulanmamış migrationların olup olmadığını bu şekilde sorgulayıp işlme yaptırıyoruz.
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                }

                if (context.Authors.Count() == 0)
                {
                    context.Authors.AddRange(Authors);
                }

                context.SaveChanges();
            }
        }

        private static Category[] Categories =
        {
            new Category() {Name="Matematik" , IsActive = true , CreateDate = DateTime.Now},
            new Category() {Name="Teknoloji", IsActive = true , CreateDate = DateTime.Now},
            new Category() {Name="Bilim", IsActive = true , CreateDate = DateTime.Now},
            new Category() {Name="Sağlık", IsActive = true , CreateDate = DateTime.Now}
        };

        private static Author[] Authors =
        {
            new Author() {Name="Mehmet", Surname ="Palabıyık", IsActive = true , CreateDate = DateTime.Now},
            new Author() {Name="Ali", Surname = "Delice", IsActive = true , CreateDate = DateTime.Now},
            new Author() {Name="Ebru", Surname="Helvayiyen", IsActive = true , CreateDate = DateTime.Now, },
        };

    }
}
