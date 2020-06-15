using CompData.Models.Library;
using CRMData.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMData.Models.BaseClass
{
    public class DefaultBaseModel
    {
        [Column(Order = 994)]
        public Guid? CreatedBy { get; set; }
        [Column(Order = 996)]
        public DateTime? CreatedAt { get; set; }
        [Column(Order = 997)]
        public Guid? UpdatedBy { get; set; }
        [Column(Order = 998)]
        public DateTime? UpdateAt { get; set; }


        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser AddApplicationUser { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual ApplicationUser EditApplicationUser { get; set; }
        
    }
}
