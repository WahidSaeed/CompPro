
namespace CRMData.Configurations.GlobalConfigurationVariables
{
    public class IdentityDefaultOptions
    {
        public bool PasswordRequireDigit { get; } = false;
        public int PasswordRequiredLength { get; set; } = 6;
        public bool PasswordRequireNonAlphanumeric { get; set; } = false;
        public bool MyPropertPasswordRequireUppercase { get; set; } = false;
        public bool PasswordRequireLowercase { get; set; } = false;
        public int PasswordRequiredUniqueChars { get; set; } = 0;
        public int LockoutDefaultLockoutTimeSpanInMinutes { get; set; } = 30;
        public int LockoutMaxFailedAccessAttempts { get; set; } = 5;
        public bool LockoutAllowedForNewUsers { get; set; } = false;
        public bool UserRequireUniqueEmail { get; set; } = true;
        public bool SignInRequireConfirmedEmail { get; set; } = false;
        public bool CookieHttpOnly { get; set; } = true;
        public int CookieExpiration { get; set; } = 150;
        public string LoginPath { get; set; } = "/Account/Login";
        public string LogoutPath { get; set; } = "/Account/Logout";
        public string AccessDeniedPath { get; set; } = "/Account/AccessDenied";
        public bool SlidingExpiration { get; set; } = true;
        public bool SessionCookieIsEssential { get; set; } = true;
    }
}
