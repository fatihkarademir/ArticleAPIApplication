using ArticleAPIApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleAPIApp.Business.Abstract
{
    public interface IArticleService : IBaseService<Article>
    {
        List<Article> GetArticlesWithCategoryAndAuthor();
        List<Article> GetArticlesByCatId(int catId);
    }
}
