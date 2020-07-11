namespace CRMData.Configurations.Constants.Enums
{
    public enum IPType
    {
        WhiteList = 1,
        BlackList = 2
    }

    public enum AuditType
    {
        LogIn,
        LogOut,
        BlockListIPLoginFailed,
        PasswordExpiredLoginFailed
    };

    public enum ResultStatus
    {
        Success = 200,
        Error = 500,
        NotFound = 400,
        Warning = 300
    };
}
