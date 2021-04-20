using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleAPIApp.Business.Abstract;
using ArticleAPIApp.Entities;
using ArticleAPIApp.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleAPIApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : BaseController // ControllerBase
    {

        IArticleService _articleService;
        ICategoryService _categoryService;
        IAuthorService _authorService;
        public ArticleController(IArticleService articleService, ICategoryService categoryService, IAuthorService authorService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _authorService = authorService;
        }

        [HttpGet]
        public ServiceResult<Article> Get()
        {
            try
            {
                var author = _authorService.GetAll();
                var article = _articleService.GetAll();
                var category = _categoryService.GetAll();
                article = article.Select(x => { x.Author = author.Where(k => k.AuthorId == x.AuthorId).FirstOrDefault(); return x; }).ToList();
                article = article.Select(x => { x.Category = category.Where(k => k.CategoryId == x.CategoryId).FirstOrDefault(); return x; }).ToList();
                return new ServiceResult<Article>() { Datas = article };
            }
            catch (Exception ex)
            {
                return new ServiceResult<Article>() { IsSucces = false, Message = ex.Message };
            }

        }

        [HttpGet("{id}")]
        public ServiceResult<Article> Get(int id)
        {
            try
            {
                return new ServiceResult<Article>() { Data = _articleService.GetById(id) };
            }
            catch (Exception ex)
            {
                return new ServiceResult<Article>() { IsSucces = false, Message = ex.Message };
            }
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public ServiceResult<Article> Post([FromBody]Article article)
        {
            try
            {
                _articleService.Create(article);
                return new ServiceResult<Article>() { IsSucces = true};
            }
            catch (Exception ex)
            {
                return new ServiceResult<Article>() { IsSucces = false, Message = ex.Message };
            }
        }

        [HttpPut]
        [Authorize(Roles ="gelmesinburaya")]
        public ServiceResult<Article> Put([FromBody]Article article)
        {
            try
            {
                _articleService.Update(article);
                return new ServiceResult<Article>() { IsSucces = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult<Article>() { IsSucces = false, Message = ex.Message };
            }
        }

        [HttpDelete("{id}")]
        public ServiceResult<Article> Delete(int id)
        {
            try
            {
                _articleService.Delete(_articleService.GetById(id));
                return new ServiceResult<Article>() { IsSucces = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult<Article>() { IsSucces = false, Message = ex.Message };
            }
        }
    }
}