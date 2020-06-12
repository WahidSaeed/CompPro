using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRMData.Models.Identity
{
    [Table("ApplicationUserNotification", Schema = "Security")]
    public class ApplicationUserNotification
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250), Required]
        public string Notification { get; set; }
        [Required]
        public Guid FK_UserId { get; set; }
        [Required]
        public string PageRedirect { get; set; }
        [Required]
        public DateTime NotificationDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }

        [ForeignKey("FK_UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
