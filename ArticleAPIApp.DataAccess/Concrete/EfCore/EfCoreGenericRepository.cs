using ArticleAPIApp.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ArticleAPIApp.DataAccess.Concrete.EfCore
{
    public class EfCoreGenericRepository<T, TContext> : IRepository<T>
        where T : class
        where TContext : DbContext, new()
    {

        public int Create(T entity)
        {
            using (var context = new TContext())
            {
                context.Set<T>().Add(entity);
                return context.SaveChanges();
            }
        }

        public int Delete(T entity)
        {
            using (var context = new TContext())
            {
                context.Set<T>().Remove(entity);
                return context.SaveChanges();
            }
        }

        public ICollection<T> GetAllByFilterOrNotFiltered(Expression<Func<T, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null ? context.Set<T>().ToList()
                                      : context.Set<T>().Where(filter).ToList();
            }
        }

        public T GetById(int id)
        {
            using (var context = new TContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public int Update(T entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                return context.SaveChanges();
            }
        }
    }
}
