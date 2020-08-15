using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ArticleAPIApp.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        int Create(T entity);
        int Update(T entity);
        int Delete(T entity);
        ICollection<T> GetAllByFilterOrNotFiltered(Expression<Func<T, bool>> filter = null);
        //Diğer en temel methodların yanında her entity'e idsine göre erişmek isteriz. Bu yüzden her entity için böyle bir method koydum.
        T GetById(int id);

    }
}
