using ArticleAPIApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleAPIApp.DataAccess.Abstract
{
    public interface IArticleDAL : IRepository<Article>
    {
        //Article entitysi için ona özel bir property için methodları burada oluşturup daha sonra concrete dal class da interface'i implemente ettiğimiz için oraya da method eklenmek zorunda olacaktır. SOLID prensiplerine aykırı bir durum oluşturmamak için gereksiz diğer classlar için olması gerekmeyen methodları IRepository'e eklemeyiz.
        List<Article> GetArticlesWithCategoryAndAuthor();
        List<Article> GetArticlesByCatId(int catId);
    }
}
