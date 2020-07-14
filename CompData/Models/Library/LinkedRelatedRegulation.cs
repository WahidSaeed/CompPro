using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("LinkedRelatedRegulation", Schema = "Library")]
    public class LinkedRelatedRegulation
    {
        [Key]
        public int Id { get; set; }
        public int RegId { get; set; }
        public int RelatedRegId { get; set; }

        [ForeignKey("RegId")]
        public virtual Regulation Regulation { get; set; }
        [ForeignKey("RelatedRegId")]
        public virtual Regulation RelatedRegulation { get; set; }
    }
}
