using CRMData.Configurations.Constants.Enums;
using CRMData.Configurations.Extensions;
using CRMData.Dao.SystemAudit;
using CRMData.Data;
using CRMData.Models.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMData.Services.SystemAudit.Impl
{
    public class SystemAuditService : ISystemAuditService
    {
        private readonly Utility utility;
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContext;
        private readonly ISystemAuditDao systemAuditDao;

        public SystemAuditService(Utility utility, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContext, ISystemAuditDao systemAuditDao)
        {
            this.utility = utility;
            this.context = applicationDbContext;
            this.httpContext = httpContext;
            this.systemAuditDao = systemAuditDao;
        }

        #region Login
        public void LoginAudit(Guid userId, AuditType auditType = AuditType.LogIn, string logInErrorReason = null)
        {
            this.systemAuditDao.LoginAudit(userId, auditType, logInErrorReason);
        }

        public void LogoutAudit()
        {
            this.systemAuditDao.LogoutAudit();
        }
        #endregion

        public async Task<bool> IsPasswordExpirationDate(Guid userId, string hashPassword) 
        {
            return await this.systemAuditDao.IsPasswordExpirationDate(userId, hashPassword); 
        }

        public async Task<bool> IsIpWhiteList()
        {
            return await this.systemAuditDao.IsIpWhiteList();
        }
    }
}
