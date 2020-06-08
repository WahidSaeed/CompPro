using CRMBusiness.Middleware.Authorization.Interface;
using CRMData.Configurations.GlobalConfigurationVariables;
using CRMData.Models.Identity;
using CRMData.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace CRMData.Configurations.Middlewares.Authorizations
{
    public class UserRightsMiddleware
    {
        private readonly RequestDelegate _next;

        public UserRightsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserManager<ApplicationUser> userManager, IConfiguration Configuration, IMiddlewareAuthorization middlewareAuthorization)
        {
            IdentityDefaultOptions identityDefaultOptions = Configuration.Get<IdentityDefaultOptions>();
            SessionKeys sessionKeys = Configuration.Get<SessionKeys>();
            ISession session = context.Session;
            string RequestedPath = context.Request.Path;
            string LoginPath = identityDefaultOptions.LoginPath;
            string LogoutPath = identityDefaultOptions.LogoutPath;
            string AccessDeniedPath = identityDefaultOptions.AccessDeniedPath;

            if (RequestedPath != LoginPath && RequestedPath != LogoutPath && RequestedPath != AccessDeniedPath)
            {
                ClaimsPrincipal User = context.User;
                if (User != null && User.Identity.IsAuthenticated)
                {
                    string UserId = session.GetOrCreateSession<string>(sessionKeys.UserId, () =>
                    {
                        return userManager.GetUserId(User);
                    });
                    if (!string.IsNullOrEmpty(UserId))
                    {
                        Guid _UserId = Guid.Parse(UserId);
                        List<UserAccessibleClaims> Urls = session.GetOrCreateSession<List<UserAccessibleClaims>>(sessionKeys.UserAccessibleClaims, () =>
                        {
                            return middlewareAuthorization.GetAuthorizedURLClaims(_UserId);
                        });
                    }
                }
            }

            await _next(context);
        }
    }
}
