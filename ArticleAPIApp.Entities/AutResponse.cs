using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleAPIApp.Entities
{
    public class AutResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool IsAuth { get; set; }
    }
}
