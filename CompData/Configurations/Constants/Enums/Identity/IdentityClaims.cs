using CRMConfiguration.Configurations.Attributes.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace CRMConfiguration.Configurations.Constants.Enums.Identity
{
    public static class Extension
    {
        public static bool HasPermission(this Enum enumProperty, IHttpContextAccessor context)
        {
            int propertyId = Convert.ToInt32(enumProperty);
            var hasClaim = context.HttpContext.User.IsInRole("Administrator") ? true: context.HttpContext.User.Claims.Any(c => c.Type.Equals(propertyId.ToString()));
            return hasClaim;
        }
    }

    //[ModuleClaims(ApplicationModule.Document, ApplicationModuleSection.Cisco)]
    public enum ApplicationClaim 
    {
        CallCenter_CaseAssignment_View = 1,
        CallCenter_CasesList_View = 2,
       
        Customer360_Customer_Search = 100,

        Wallboard_ComplaintWallBoardController_View = 200,

        SSRS_Reports = 900
    }
}
