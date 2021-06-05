using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("RegulationSectionVersion", Schema = "Library")]
    public class RegulationSectionVersion
    {
        [Key]
        public int Id { get; set; }
        public string Version { get; set; }
        public int SectionId { get; set; }
        [MaxLength(500), Required]
        public string SectionTitle { get; set; }
        public int? ParentId { get; set; }
        public int RegulationId { get; set; }
        public int Sequence { get; set; }
        public DateTime VersionDate { get; set; }
    }
}
