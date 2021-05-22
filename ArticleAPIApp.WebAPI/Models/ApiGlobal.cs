using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArticleAPIApp.WebAPI.Models
{
    public static class ApiGlobal
    {
        public static string GetEmail(HttpContext context)
        {
            var identity = ((ClaimsIdentity)context.User.Identity).Claims;
            List<Claim> claims = identity.ToList();
            if (claims != null && claims.Any())
            {
                return claims.First(q => q.Type == ClaimTypes.Email)?.Value;
            }
            return null;

        }

        public static string GetType(HttpContext context)
        {
            var identity = ((ClaimsIdentity)context.User.Identity).Claims;
            List<Claim> claims = identity.ToList();
            if (claims != null && claims.Any())
            {
                return claims.First(q => q.Type == "isAuth")?.Value;
            }
            return null;

        }

        public static string GetId(HttpContext context)
        {
            var identity = ((ClaimsIdentity)context.User.Identity).Claims;
            List<Claim> claims = identity.ToList();
            if (claims != null && claims.Any())
            {
                return claims.First(q => q.Type == "id")?.Value;
            }
            return null;
        }

        public static string GetRole(HttpContext context)
        {
            var identity = ((ClaimsIdentity)context.User.Identity).Claims;
            List<Claim> claims = identity.ToList();
            if (claims != null && claims.Any())
            {
                return claims.First(q => q.Type == ClaimTypes.Role)?.Value;
            }
            return null;
        }
    }
}
