using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleAPIApp.Business.Abstract;
using ArticleAPIApp.Entities;
using ArticleAPIApp.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleAPIApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public ServiceResult<Category> Get()
        {
            try
            {
                var categories = _categoryService.GetAll();
                return new ServiceResult<Category>() { Datas = categories };
              
            }
            catch (Exception ex)
            {
                return new ServiceResult<Category>() { IsSucces = false, Message = ex.Message };
            }

        }
    }
}