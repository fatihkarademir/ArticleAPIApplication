using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleAPIApp.Entities
{
    //Classlar default AccessModifier internal olarak gelir. Ancak bu ve diğer entity classlarını diğer namesapacelerde erişilebilir hale getirmek için public olarak set ediyoruz.
    public class Article : BaseEntity
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public Author Author { get; set; }
        public Category Category { get; set; }



    }
}
