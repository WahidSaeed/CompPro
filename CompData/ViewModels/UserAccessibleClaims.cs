using System.ComponentModel.DataAnnotations.Schema;

namespace CRMData.ViewModels
{
    [Table("UserAccessibleClaims", Schema = "ProcedureView")]
    public class UserAccessibleClaims
    {
        public string DisplayName { get; set; }
        public string URL { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; } = "Index";
        public string ClaimType { get; set; }
        public int? MenuID { get; set; }
        public int? ParentID { get; set; }
        public bool IsLocal { get; set; } = true;
        public int Order { get; set; }
    }
}
