using CRMData.ViewModels;
using System;
using System.Collections.Generic;

namespace CRMBusiness.Middleware.Authorization.Interface
{
    public interface IMiddlewareAuthorization
    {
        public List<UserAccessibleClaims> GetAuthorizedURLClaims(Guid UserId);

    }
}
