using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("TagMap", Schema = "Library")]
    public class TagMap
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250), Required]
        public string TagGroupKey { get; set; }

        [MaxLength(250), Required]
        public string Tag { get; set; }

        [ForeignKey("RegId")]
        public int RegId { get; set; }

        [ForeignKey("SecId")]
        public int SecId { get; set; }

        [ForeignKey("DescId")]
        public int DescId { get; set; }

        public virtual Regulation Regulation { get; set; }
        public virtual RegulationSection RegulationSection { get; set; }
        public virtual RegulationDetail RegulationDetail { get; set; }
    }
}
