using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("RegulationDetailVersion", Schema = "Library")]
    public class RegulationDetailVersion
    {
        [Key]
        public int Id { get; set; }
        public string Version { get; set; }
        public int RegDetailId { get; set; }
        public string RegDescription { get; set; }
        public string RegDescriptionClean { get; set; }
        public int RegulationId { get; set; }
        public int SectionId { get; set; }
        public int Sequence { get; set; }
        public DateTime VersionDate { get; set; }
    }
}
