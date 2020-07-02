using CRMData.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("LinkedUserRegulationSource", Schema = "Library")]
    public class LinkedUserRegulationSource
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int SourceId { get; set; }
        public int IsDefault { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("SourceId")]
        public virtual RegulationSource RegulationSource { get; set; }
    }
}
