using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleAPIApp.Entities.DTOs
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
