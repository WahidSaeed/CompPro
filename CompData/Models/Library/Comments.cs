using CRMData.Models.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{
    [Table("Comments", Schema = "Library")]
    public class Comments: DefaultBaseModel
    {
        [Key]
        public int CommentID { get; set; }
        public string CommentText { get; set; }
        public int CommentTypeID { get; set; }
        public int? ParentID { get; set; }
        public int RegID { get; set; }
        public int? RegDetailID { get; set; }

        [ForeignKey("RegID")]
        public virtual Regulation Regulation { get; set; }

        [ForeignKey("RegDetailID")]
        public virtual RegulationDetail RegulationSource { get; set; }
    }
}
