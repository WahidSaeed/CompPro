using CRMData.Models.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("Requirements", Schema = "Library")]
    public class Requirements : DefaultBaseModel
    {
        [Key]
        public int CommentID { get; set; }
        public string CommentText { get; set; }
        public int RegID { get; set; }

        [ForeignKey("RegID")]
        public virtual Regulation Regulation { get; set; }
    }
}
