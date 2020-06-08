using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMData.Models.Identity
{
    [Table("ApplicationMenu", Schema = "Security")]
    public class ApplicationMenu
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string DisplayName { get; set; }
        [MaxLength(100)]
        public string Url { get; set; }
        [MaxLength(50)]
        public string Area { get; set; }
        [MaxLength(50)]
        public string Controller { get; set; }
        [MaxLength(50)]
        public string Action { get; set; } = "Index";
        public int Order { get; set; } = 0;
        [ForeignKey("ParentId")]
        public int? ParentId { get; set; }
        [ForeignKey("ClaimID")]
        public string ClaimID { get; set; }
        public bool isLocal { get; set; } = true;

        public virtual ApplicationMenu Parent { get; set; }
        public virtual ICollection<ApplicationMenu> Children { get; set; }
        public virtual ApplicationClaim ApplicationClaim { get; set; }
    }
}
