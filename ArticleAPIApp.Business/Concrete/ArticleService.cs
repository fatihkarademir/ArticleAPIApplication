using ArticleAPIApp.Business.Abstract;
using ArticleAPIApp.DataAccess.Abstract;
using ArticleAPIApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArticleAPIApp.Business.Concrete
{
    public class ArticleService : IArticleService
    {
        IArticleDAL _articleDAL;
        public ArticleService(IArticleDAL articleDAL)
        {
            _articleDAL = articleDAL;
        }
        public bool Delete(Article entity)
        {
            return _articleDAL.Delete(entity) > 0;
        }

        public List<Article> GetAll()
        {
            //notfiltered 
            return _articleDAL.GetAllByFilterOrNotFiltered().ToList();
        }
        void ValidationOfArticle(Article entity)
        {
            if (entity.Title == string.Empty || entity.Title == null)
            {
                throw new Exception("Başlık alanını boş bırakmayınız");
            }
            if (entity.Content == string.Empty || entity.Content == null)
            {
                throw new Exception("İçeriği boş bırakmayınız");
            }
            if (entity.Content.Length <50 )
            {
                throw new Exception("İçerik 50 karakterden az olamaz");
            }

        }
        public bool Create(Article entity)
        {
            entity.IsActive = true;
            entity.CreateDate = DateTime.Now;
            ValidationOfArticle(entity);
            return _articleDAL.Create(entity) > 0;
        }

        public bool Update(Article entity)
        {
            ValidationOfArticle(entity);
            return _articleDAL.Update(entity) > 0;
        }

        public Article GetById(int id)
        {
            return _articleDAL.GetById(id);
        }

        public List<Article> GetArticlesWithCategoryAndAuthor()
        {
            return _articleDAL.GetArticlesWithCategoryAndAuthor();
        }

        public List<Article> GetArticlesByCatId(int catId)
        {
            return _articleDAL.GetArticlesByCatId(catId);
        }
    }
}
