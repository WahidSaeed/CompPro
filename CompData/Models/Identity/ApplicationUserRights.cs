using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMData.Models.Identity
{
    [Table("ApplicationUserRights", Schema = "Security")]
    public class ApplicationUserRight
    {
        public Guid RoleId { get; set; }
        public int MenuId { get; set; }
        public int ClaimId { get; set; }

        [ForeignKey("RoleId")]
        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }

        [ForeignKey("MenuId")]
        public virtual ICollection<ApplicationMenu> ApplicationMenus { get; set; }

        [ForeignKey("ClaimId")]
        public virtual ICollection<ApplicationClaim> ApplicationClaims { get; set; }
    }
}
