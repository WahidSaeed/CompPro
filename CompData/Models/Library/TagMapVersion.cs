using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("TagMapVersion", Schema = "Library")]
    public class TagMapVersion
    {
        [Key]
        public int Id { get; set; }
        public string Version { get; set; }
        public int TagId { get; set; }
        [MaxLength(250), Required]
        public string TagGroupKey { get; set; }
        [MaxLength(250), Required]
        public string Tag { get; set; }
        public int RegId { get; set; }
        public int SecId { get; set; }
        public int DescId { get; set; }
    }
}
