using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("RegulationSource", Schema = "Library")]
    public class RegulationSource
    {
        [Key]
        public int SourceId { get; set; }
        [MaxLength(250), Required]
        public string FullName { get; set; }
        [MaxLength(10), Required]
        public string ShortName { get; set; }
    }
}
