using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("RegulationSection", Schema = "Library")]
    public class RegulationSection
    {
        [Key]
        public int SectionId { get; set; }
        [MaxLength(500), Required]
        public string SectionTitle { get; set; }
        [ForeignKey("ParentId")]
        public int? ParentId { get; set; }
        [ForeignKey("RegulationId")]
        public int RegulationId { get; set; }
        public int Sequence { get; set; }

        public virtual RegulationSection Parent { get; set; }
        public virtual List<RegulationSection> Children { get; set; }

        public virtual List<RegulationDetail> RegulationDetails { get; set; }
        public virtual List<TagMap> TagMaps { get; set; }
        public virtual Regulation Regulation { get; set; }
    }
}
