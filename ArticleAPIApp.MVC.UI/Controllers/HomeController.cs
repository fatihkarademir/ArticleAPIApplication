using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleAPIApp.Business.Abstract;
using ArticleAPIApp.Entities;
using ArticleAPIApp.MVC.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ArticleAPIApp.MVC.UI.Controllers
{
    public class HomeController : Controller
    {

       
        ContentService _service;
        public HomeController(ContentService contentService)
        {          
            _service = contentService;
        }
        public IActionResult Index(int categoryId = 0)
        {
            var categories = _service.GetCategories().Datas;

            if (categoryId == 0)
            {
                var articles = _service.GetArticles().Datas;
               
                return View(new ArticleListModel()
                {
                    Articles = articles,
                    CategoryLists = categories

                });
            }
            return View(new ArticleListModel()
            {
                Articles = _service.GetArticles().Datas.Where(x => x.CategoryId == categoryId).ToList(),
                CategoryLists = categories
            });

        }
        [HttpGet]
        public IActionResult OtherProcesses()
        {
            return View(new ArticleListModel()
            {
                Articles = _service.GetArticles().Datas,

            });
        }

        private void GetCategoriesWithListItems()
        {
            List<Category> categories = _service.GetCategories().Datas;
            List<SelectListItem> categoriesListItems = new List<SelectListItem>();
            foreach (Category item in categories)
            {
                categoriesListItems.Add(new SelectListItem() { Text = item.Name, Value = item.CategoryId.ToString() });
            }
            ViewBag.Categories = categoriesListItems;
        }
        private void GetAuthorsWithListItems()
        {
            List<Author> authors = _service.GetAuthors().Datas;
            List<SelectListItem> authorsListItems = new List<SelectListItem>();
            foreach (Author item in authors)
            {
                authorsListItems.Add(new SelectListItem() { Text = item.Name + " " + item.Surname, Value = item.AuthorId.ToString() });
            }
            ViewBag.Authors = authorsListItems;
        }

        [HttpGet]
        public IActionResult CreateArticle()
        {
            GetCategoriesWithListItems();
            GetAuthorsWithListItems();
            return View();
        }

        [HttpPost]
        public IActionResult CreateArticle(Article article)
        {

            GetCategoriesWithListItems();
            GetAuthorsWithListItems();
            ServiceResult<Article> result = _service.CreateArticle(article);
            if (result.IsSucces)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = result.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult EditArticle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var entity = _articleService.GetById((int)id);
            var data = _service.GetArticle((int)id).Data;
            GetCategoriesWithListItems();
            GetAuthorsWithListItems();
            return View(data);
        }

        [HttpPost]
        public IActionResult EditArticle(Article article)
        {
            var entity = _service.GetArticle(article.ArticleId).Data;

            if (entity == null)
            {
                return NotFound();
            }

            entity.Title = article.Title;
            entity.Content = article.Content;
            ServiceResult<Article> result = _service.UpdateArticle(entity);
            if (result.IsSucces)
            {
                return RedirectToAction("OtherProcesses");
            }
            else
            {
                ViewBag.Error = result.Message;
                return View(entity);
            }         
        }

        [HttpPost]
        public IActionResult DeleteArticle(int articleId)
        {
            var entity = _service.GetArticle(articleId).Data;
            if (entity == null)
            {
                NotFound();
            }
            bool result = _service.DeleteArticle(articleId).IsSucces;
            return RedirectToAction("OtherProcesses");
        }
    }
}