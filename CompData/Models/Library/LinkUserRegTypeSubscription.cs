using CRMData.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("LinkUserRegTypeSubscription", Schema = "Library")]
    public class LinkUserRegTypeSubscription
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int RegTypeId { get; set; }
        public int RegSourceId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("RegTypeId")]
        public virtual RegulationType RegulationType { get; set; }
        [ForeignKey("RegSourceId")]
        public virtual RegulationSource RegulationSource { get; set; }
    }
}
