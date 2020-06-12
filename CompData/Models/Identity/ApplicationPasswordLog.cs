using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRMData.Models.Identity
{
    [Table("ApplicationPasswordLog", Schema = "Security")]
    public class ApplicationPasswordLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Guid FK_UserId { get; set; }
        [Required]
        public DateTime PasswordLogDate { get; set; } = DateTime.Now;
        [Required]
        public bool IsActive { get; set; }

        [ForeignKey("FK_UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
