using ArticleAPIApp.DataAccess.Abstract;
using ArticleAPIApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArticleAPIApp.DataAccess.Concrete.EfCore
{
    public class EfCoreArticleDAL : EfCoreGenericRepository<Article, ArticleAppContext>, IArticleDAL
    {
        public List<Article> GetArticlesByCatId(int catId)
        {
            using (var context = new ArticleAppContext())
            {
                return context.Articles
                               .Include(i => i.Author)
                               .Include(i => i.Category)
                               .Where(i => i.CategoryId == catId).ToList();

                //Sadece Articles nesnesini verir. Category ve Author nesneleri de dolu gelsin istiyor isek Include vermemiz gerekir.
                //context.Articles.Where(i => i.CategoryId == catId).ToList();
            }
        }

        public List<Article> GetArticlesWithCategoryAndAuthor()
        {
            using (var context = new ArticleAppContext())
            {
                return context.Articles
                               .Include(i => i.Author)
                               .Include(i => i.Category)
                               .ToList();
            }
        }
    }
}
