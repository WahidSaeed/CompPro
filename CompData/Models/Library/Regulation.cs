using CRMData.Models.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("Regulation", Schema = "Library")]
    public class Regulation: DefaultBaseModel
    {
        [Key]
        public int RegId { get; set; }
        [MaxLength(250), Required]
        public string RegulationTitle { get; set; }
        public DateTime IssueDate { get; set; }
        public int SourceID { get; set; }
        [MaxLength(50)]
        public string ReferenceNumber { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? RegTypeID { get; set; }

        [ForeignKey("SourceID")]
        public virtual RegulationSource RegulationSource { get; set; }

        public virtual ICollection<RegulationSection> RegulationSections { get; set; }

    }
}
