using ArticleAPIApp.Entities;
using ArticleAPIApp.Entities.DTOs;
using ArticleAPIApp.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ContentService
{
    public class ApiService
    {
        HttpClient client;
        private string serviceUrl = "http://localhost:49983/api/";

        private string Key = "af8a4c28-9906-48a4-ae6c-c9630fcf901e";

        //public ApiService(HttpContext context)
        //{
        //    client = new HttpClient();
        //    client.DefaultRequestHeaders.Add("ApiKey", Key);
        //    var token = context.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
        //    if (token != null)
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    }
        //}

        public ApiService(HttpContext context)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", Key);
            
            var token = context.Session.GetString("token");
            var token1 = context.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }


        private string PostData(string url, Object data)
        {
            var result = client.PostAsync(serviceUrl + url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = result.Result.Content.ReadAsStringAsync().Result;
            return resultString;
       
        }
        private string PutData(string url, Object data)
        {
            var result = client.PutAsync(serviceUrl + url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")).Result;
            return result.Content.ReadAsStringAsync().Result;
        }
        private string DeleteData(string url)
        {
            var result = client.DeleteAsync(serviceUrl + url).Result;
            return result.Content.ReadAsStringAsync().Result;
        }

        private string GetData(string url)
        {
            var result = client.GetStringAsync(serviceUrl + url).Result;
            return result;
        }

        public ServiceResult<Article> DeleteArticle(int id)
        {
            return JsonConvert.DeserializeObject<ServiceResult<Article>>(DeleteData("article/" + id));
        }

        public ServiceResult<Article> GetArticles()
        {
            return JsonConvert.DeserializeObject<ServiceResult<Article>>(GetData("article"));
        }

        public ServiceResult<Author> GetAuthors()
        {
            return JsonConvert.DeserializeObject<ServiceResult<Author>>(GetData("author"));
        }

        public ServiceResult<Category> GetCategories()
        {
            return JsonConvert.DeserializeObject<ServiceResult<Category>>(GetData("category"));
        }

        public ServiceResult<Article> GetArticle(int articleId)
        {
            return JsonConvert.DeserializeObject<ServiceResult<Article>>(GetData("article/" + articleId));
        }

        public ServiceResult<Article> UpdateArticle(Article article)
        {
            return JsonConvert.DeserializeObject<ServiceResult<Article>>(PutData("article/", article));
        }

        public ServiceResult<Article> CreateArticle(Article article)
        {
            return JsonConvert.DeserializeObject<ServiceResult<Article>>(PostData("article/", article));
        }

        public ServiceResult<AutResponse> Login(LoginDTO loginDTO)
        {
            return JsonConvert.DeserializeObject<ServiceResult<AutResponse>>(PostData("Account/Login", loginDTO));
        }
    }
}
