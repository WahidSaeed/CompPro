using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRMConfiguration.Configurations.Attributes.Identity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class hasPermission : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly List<string> claimTypes = new List<string>();
        public hasPermission(params object[] claims)
        {
            if (claimTypes.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("roles");

            foreach (var claim in claims)
            {
                this.claimTypes.Add(((int)Enum.Parse(claim.GetType(), claim.ToString())).ToString());
            }
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.IsInRole("Administrator") ? true: context.HttpContext.User.Claims.Any(c => claimTypes.Contains(c.Type));
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
