using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleAPIApp.MVC.UI.Models
{
    public class ServiceResult<T>
    {
        public bool IsSucces { get ; set ; }
        public string Message { get ; set; }
        public List<T> Datas { get; set ; }
        public T Data { get; set; }
    }
}
