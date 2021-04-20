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
    public class AuthorController : BaseController //  ControllerBase
    {
        IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }


        [HttpGet]
        public ServiceResult<Author> Get()
        {
            try
            {
                var authors = _authorService.GetAll();
                return new ServiceResult<Author>() { Datas = authors };

            }
            catch (Exception ex)
            {
                return new ServiceResult<Author>() { IsSucces = false, Message = ex.Message };
            }

        }
    }
}