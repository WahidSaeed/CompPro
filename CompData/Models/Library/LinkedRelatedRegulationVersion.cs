using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("LinkedRelatedRegulationVersion", Schema = "Library")]
    public class LinkedRelatedRegulationVersion
    {
        [Key]
        public int Id { get; set; }
        public string Version { get; set; }
        public int LinkId { get; set; }
        public int RegId { get; set; }
        public int RelatedRegId { get; set; }
    }
}
