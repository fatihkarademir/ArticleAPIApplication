using ArticleAPIApp.Business.Abstract;
using ArticleAPIApp.DataAccess.Abstract;
using ArticleAPIApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArticleAPIApp.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        ICategoryDAL _categoryDAL;
        public CategoryService(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }
        public bool Create(Category entity)
        {
            return _categoryDAL.Create(entity) > 0;
        }

        public bool Delete(Category entity)
        {
            return _categoryDAL.Delete(entity) > 0;
        }

        public List<Category> GetAll()
        {
            return _categoryDAL.GetAllByFilterOrNotFiltered().ToList();
        }

        public Category GetById(int id)
        {
            return _categoryDAL.GetById(id);
        }

        public bool Update(Category entity)
        {
            return _categoryDAL.Update(entity) > 0;
        }
    }
}
