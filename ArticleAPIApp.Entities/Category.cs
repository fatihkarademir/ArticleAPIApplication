using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleAPIApp.Entities
{
    public class Category : BaseEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsEnable { get; set; }
        public ICollection<Article> Articles { get; set; }

    }
}
