using ArticleAPIApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleAPIApp.MVC.UI.Models
{
    public class ArticleListModel
    {
        public List<Article> Articles { get; set; }

        public List<Category> CategoryLists { get; set; }
    }
}
