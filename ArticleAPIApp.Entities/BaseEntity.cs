using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleAPIApp.Entities
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
