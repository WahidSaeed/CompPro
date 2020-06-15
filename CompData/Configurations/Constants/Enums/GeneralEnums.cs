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
    public enum CurrentStatus
    {
        None = 0,
        Active = 1,
        InActive = 2
    }
    public enum InstallmentMode
    {
        Yearly = 1,
        HalfYearly = 2,
        Quarterly = 3,
        Monthly = 4
    }
    public enum BusinessType
    {
        None = 0,
        Corporate = 1,
        Retail = 2,
        Banca = 3,
        Micro = 4,
        MassRetail = 5,
        Digital = 6,
        CallCenter = 7,
        AI = 8

    }
    public enum ApplicationProduct
    {
        HISCRM = 0,
        GLASCRM = 1
    }
    public enum Relations
    {
        None,
        Self,
        Spouse,
        Son,
        Daughter,
        Father,
        Mother,
        Brother,
        Sister
    }
    public enum Gender
    {
        None = 0,
        Other = 1,
        Male = 2,
        Female = 3
    };
    public enum ClubType
    {
        None = 0,
        Silver = 1,
        Gold = 2,
        Platinum = 3
    }
}
