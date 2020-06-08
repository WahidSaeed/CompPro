using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("RegulationDetail", Schema = "Library")]
    public class RegulationDetail
    {
        [Key]
        public int RegDetailId { get; set; }
        public string RegDescription { get; set; }
        public int RegulationId { get; set; }
        public int SectionId { get; set; }

        [ForeignKey("SectionId")]
        public virtual RegulationSection RegulationSection { get; set; }
        [ForeignKey("RegulationId")]
        public virtual Regulation Regulation { get; set; }
    }
}
