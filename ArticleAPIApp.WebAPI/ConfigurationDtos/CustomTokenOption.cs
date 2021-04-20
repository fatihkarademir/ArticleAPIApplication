using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleAPIApp.WebAPI.ConfigurationDtos
{
    public class CustomTokenOption
    {
        public List<string> Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
