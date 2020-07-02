using CRMData.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("LinkUserRegulationSubscription", Schema = "Library")]
    public class LinkUserRegulationSubscription
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int RegId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("RegId")]
        public virtual Regulation Regulation { get; set; }
    }
}
