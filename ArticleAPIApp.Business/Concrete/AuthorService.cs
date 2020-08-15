using ArticleAPIApp.Business.Abstract;
using ArticleAPIApp.DataAccess.Abstract;
using ArticleAPIApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArticleAPIApp.Business.Concrete
{
    public class AuthorService : IAuthorService
    {
        IAuthorDAL _authorDAL;
        public AuthorService(IAuthorDAL authorDAL)
        {
            _authorDAL = authorDAL;
        }
        public bool Create(Author entity)
        {
            return _authorDAL.Create(entity) > 0;
        }

        public bool Delete(Author entity)
        {
            return _authorDAL.Delete(entity) > 0;
        }

        public List<Author> GetAll()
        {
            return _authorDAL.GetAllByFilterOrNotFiltered().ToList();
        }

        public Author GetById(int id)
        {
            return _authorDAL.GetById(id);
        }

        public bool Update(Author entity)
        {
            return _authorDAL.Update(entity) > 0;
        }
    }
}
