using Microsoft.AspNetCore.Builder;

namespace CRMData.Configurations.Middlewares.Authorizations
{
    public static class UserRightsMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserRights(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserRightsMiddleware>();
        }
    }
}
