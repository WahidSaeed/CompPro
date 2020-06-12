using CRMData.Configurations.Constants.Enums;
using CRMData.Configurations.Extensions;
using CRMData.Data;
using CRMData.Generics;
using CRMData.Models.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMData.Dao.SystemAudit.Impl
{
    public class SystemAuditDao : ISystemAuditDao
    {
        private readonly Utility utility;
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContext;

        public SystemAuditDao(Utility utility, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContext) 
        {
            this.utility = utility;
            this.context = applicationDbContext;
            this.httpContext = httpContext;
        }

        public void LoginAudit(Guid userId, AuditType auditType = AuditType.LogIn, string logInErrorReason = null)
        {
            try
            {
                ApplicationLoginAudit loginAudit = new ApplicationLoginAudit()
                {
                    FK_User_ID = userId,
                    CurrentDateTime = DateTime.Now,
                    IPAddress = httpContext.HttpContext.Connection.RemoteIpAddress.ToString(),
                    AuditType = auditType,
                    LogInErrorReason = logInErrorReason
                };

                context.ApplicationLoginAudits.Add(loginAudit);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorHandle(ex);
            }
        }

        public void LogoutAudit()
        {
            try
            {
                ApplicationLoginAudit loginAudit = new ApplicationLoginAudit()
                {
                    FK_User_ID = this.utility.GetLoggedUserId(),
                    CurrentDateTime = DateTime.Now,
                    IPAddress = httpContext.HttpContext.Connection.RemoteIpAddress.ToString(),
                    AuditType = AuditType.LogOut
                };

                context.ApplicationLoginAudits.Add(loginAudit);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorHandle(ex);
            }
        }

        public async Task<bool> IsPasswordExpirationDate(Guid userId, string hashPassword)
        {
            DateTime dateTime = DateTime.Now;
            try
            {
                var logPass = this.context.ApplicationPasswordLogs.Where(x => x.FK_UserId.Equals(userId) && x.IsActive.Equals(true)).FirstOrDefault();
                if (logPass != null)
                {
                    dateTime = logPass.PasswordLogDate;
                }
                else
                {
                    dateTime = DateTime.Now;
                    ApplicationPasswordLog passwordLog = new ApplicationPasswordLog();
                    passwordLog.FK_UserId = userId;
                    passwordLog.Password = hashPassword;
                    passwordLog.PasswordLogDate = dateTime;
                    passwordLog.IsActive = true;

                    this.context.Entry<ApplicationPasswordLog>(passwordLog).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    await this.context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }

            if ((DateTime.Now - dateTime).TotalDays > 20)
            {
                ApplicationUserNotification userNotification = new ApplicationUserNotification();
                userNotification.FK_UserId = userId;
                userNotification.IsRead = false;
                userNotification.NotificationDate = DateTime.Now;
                userNotification.PageRedirect = "Account/Login/ChangePassword";
                userNotification.Notification = "Your password will be expired soon. Click to redirect.";

                this.context.Entry<ApplicationUserNotification>(userNotification).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await this.context.SaveChangesAsync();

                return false;
            }
            else if ((DateTime.Now - dateTime).TotalDays > 30)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsIpWhiteList()
        {
            try
            {
                string requestIP = httpContext.HttpContext.Connection.RemoteIpAddress.ToString();
                var appIP = this.context.ApplicationIPs.Where(x => x.IP == requestIP).FirstOrDefault();
                if (appIP == null)
                {
                    ApplicationIP applicationIP = new ApplicationIP();
                    applicationIP.IP = requestIP;
                    applicationIP.IPType = IPType.WhiteList;

                    this.context.Entry<ApplicationIP>(applicationIP).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    await this.context.SaveChangesAsync();

                    return true;
                }

                return appIP.IPType == IPType.WhiteList;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void ErrorHandle(Exception ex)
        {
            try
            {
                ApplicationLoginAudit loginAudit = new ApplicationLoginAudit()
                {
                    FK_User_ID = this.utility.GetLoggedUserId(),
                    CurrentDateTime = DateTime.Now,
                    IPAddress = httpContext.HttpContext.Connection.RemoteIpAddress.ToString(),
                    AuditType = AuditType.LogIn,
                    LogInErrorReason = ex.Message
                };

                context.ApplicationLoginAudits.Add(loginAudit);
                context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

    }
}
