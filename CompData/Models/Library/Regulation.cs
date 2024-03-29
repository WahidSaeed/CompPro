﻿using CRMData.Models.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("Regulation", Schema = "Library")]
    public class Regulation : DefaultBaseModel
    {
        [Key]
        public int RegId { get; set; }
        [MaxLength, Required]
        public string RegulationTitle { get; set; }
        public DateTime IssueDate { get; set; }
        public int SourceID { get; set; }
        [MaxLength(50)]
        public string ReferenceNumber { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? RegTypeID { get; set; }

        [ForeignKey("SourceID")]
        public virtual RegulationSource RegulationSource { get; set; }

        [ForeignKey("RegTypeID")]
        public virtual RegulationType RegulationType { get; set; }
        public long Views { get; set; } = 0;

        public string Summary { get; set; }
        [MaxLength(500)]
        public string CustomURL { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }

        public virtual ICollection<RegulationSection> RegulationSections { get; set; }
        public virtual ICollection<TagMap> TagMaps { get; set; }
        public virtual ICollection<LinkedRelatedRegulation> LinkedRegulations { get; set; }
        public virtual ICollection<LinkedRelatedRegulation> RelatedRegulations { get; set; }

    }
}
