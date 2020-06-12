using CRMData.Configurations.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMData.Services.SystemAudit
{
    public interface ISystemAuditService
    {
        public void LoginAudit(Guid userId, AuditType auditType = AuditType.LogIn, string logInErrorReason = null);

        public void LogoutAudit();

        public Task<bool> IsPasswordExpirationDate(Guid userId, string hashPassword);

        public Task<bool> IsIpWhiteList();
    }
}
