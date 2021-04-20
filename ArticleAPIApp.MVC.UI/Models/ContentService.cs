using ArticleAPIApp.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArticleAPIApp.MVC.UI.Models
{
    //public class ContentService
    //{
    //    HttpClient client;
    //    private string serviceUrl = "http://localhost:49983/api/";
    //    public ContentService()
    //    {
    //        client = new HttpClient();
    //    }

    //    ContentService Service;
    //    public ContentService getInstance()
    //    {
    //        if (Service == null)
    //        {
    //            Service = new ContentService();
    //        }

    //        return Service;
    //    }
    //    private string PostData(string url, Object data)
    //    {
    //        var result = client.PostAsync(serviceUrl + url, new StringContent(JsonConvert.SerializeObject(data),Encoding.UTF8, "application/json"));
    //        var resultString = result.Result.Content.ReadAsStringAsync().Result;
    //        return resultString;
    //    }
    //    private string PutData(string url, Object data)
    //    {
    //        var result = client.PutAsync(serviceUrl + url, new StringContent(JsonConvert.SerializeObject(data),Encoding.UTF8, "application/json")).Result;
    //        return result.Content.ReadAsStringAsync().Result;
    //    }
    //    private string DeleteData(string url)
    //    {
    //        var result = client.DeleteAsync(serviceUrl + url).Result;
    //        return result.Content.ReadAsStringAsync().Result;
    //    }

    //    private string GetData(string url)
    //    {
    //        var result = client.GetStringAsync(serviceUrl + url).Result;
    //        return result;
    //    }

    //    public ServiceResult<Article> DeleteArticle(int id)
    //    {
    //       return JsonConvert.DeserializeObject<ServiceResult<Article>>(DeleteData("article/"+id));
    //    }

    //    public ServiceResult<Article> GetArticles()
    //    {
    //        return JsonConvert.DeserializeObject<ServiceResult<Article>>(GetData("article"));
    //    }

    //    public ServiceResult<Author> GetAuthors()
    //    {
    //        return JsonConvert.DeserializeObject<ServiceResult<Author>>(GetData("author"));
    //    }

    //    public ServiceResult<Category> GetCategories()
    //    {
    //        return JsonConvert.DeserializeObject<ServiceResult<Category>>(GetData("category"));
    //    }

    //    public ServiceResult<Article> GetArticle(int articleId)
    //    {
    //        return JsonConvert.DeserializeObject<ServiceResult<Article>>(GetData("article/"+articleId));
    //    }

    //    public ServiceResult<Article> UpdateArticle(Article article)
    //    {
    //        return JsonConvert.DeserializeObject<ServiceResult<Article>>(PutData("article/",article));
    //    }

    //    public ServiceResult<Article> CreateArticle(Article article)
    //    {
    //        return JsonConvert.DeserializeObject<ServiceResult<Article>>(PostData("article/", article));
    //    }


    //}
}
