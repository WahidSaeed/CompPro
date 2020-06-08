using CRMBusiness.Middleware.Authorization.Interface;
using CRMData.Data;
using CRMData.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRMBusiness.Middleware.Authorization
{
    public class MiddlewareAuthorization : IMiddlewareAuthorization
    {
        private readonly ApplicationDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public MiddlewareAuthorization(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.db = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        public List<UserAccessibleClaims> GetAuthorizedURLClaims(Guid UserGuid)
        {
            var UserId = UserGuid;
            List<UserAccessibleClaims> userAccessibleURLClaims = db.Set<UserAccessibleClaims>().FromSqlRaw($"EXEC Security.GetUserClaims '{UserId}'").ToList();

            return userAccessibleURLClaims;
        }
    }
}
