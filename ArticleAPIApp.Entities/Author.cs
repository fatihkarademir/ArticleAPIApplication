using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleAPIApp.Entities
{
    public class Author : BaseEntity
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Article> Articles { get; set; }

    }
}
