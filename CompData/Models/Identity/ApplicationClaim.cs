using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMData.Models.Identity
{
    [Table("ApplicationClaim", Schema = "Security")]
    public class ApplicationClaim
    {
        [Required]
        [Key]
        [MaxLength(100)]
        public string Claim { get; set; }
        public int EnumValue { get; set; }
        public virtual IEnumerable<ApplicationMenu> ApplicationMenu { get; set; }
        public virtual IEnumerable<ApplicationRoleClaim> ApplicationRoleClaim { get; set; }
    }
}
