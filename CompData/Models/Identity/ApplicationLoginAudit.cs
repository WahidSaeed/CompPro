using CRMData.Configurations.Constants.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRMData.Models.Identity
{
    [Table("ApplicationLoginAudit", Schema = "Security")]
    public class ApplicationLoginAudit
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("FK_User_ID")]
        public Guid FK_User_ID { get; set; }
        [Required]
        public DateTime CurrentDateTime { get; set; } = DateTime.Now;
        [Required]
        public string IPAddress { get; set; }
        public AuditType AuditType { get; set; }
        public string LogInErrorReason { get; set; }

        public virtual ApplicationUser AddApplicationUser { get; set; }
    }
}
