using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("RegulationType", Schema = "Library")]
    public class RegulationType
    {
        [Key]
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        [MaxLength(3)]
        public string TypeCode { get; set; }
    }
}
