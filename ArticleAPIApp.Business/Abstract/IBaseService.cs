using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleAPIApp.Business.Abstract
{
    public interface IBaseService<T>
    {
        bool Create(T entity);
        bool Delete(T entity);
        bool Update(T entity);
        List<T> GetAll();
        T GetById(int id);
    }
}
