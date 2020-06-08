using CRMData.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace CRMData
{
    public static class UserAccessibleURLClaimExtensions
    {
        public static bool ContainsRequestedURL(this List<UserAccessibleClaims> userAccessibleURLClaims, string RequestedURL)
        {
            var urls = from claims in userAccessibleURLClaims
                       where claims.URL != null
                       select new
                       {
                           claims.URL
                       };
            bool exists = urls.Where(x => x.URL.ToString().Equals(RequestedURL)).Count() > 0;
            return exists;
        }
    }
}
