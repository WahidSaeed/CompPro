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
        public Guid? AddBy { get; set; }
        [MaxLength(15), Column(Order = 995)]
        public string AddIP { get; set; }
        [Column(Order = 996)]
        public DateTime? AddOn { get; set; }
        [Column(Order = 997)]
        public Guid? EditBy { get; set; }
        [Column(Order = 998)]
        public DateTime? EditOn { get; set; }
        [MaxLength(15), Column(Order = 999)]
        public string EditIP { get; set; }


        [ForeignKey("AddBy")]
        public virtual ApplicationUser AddApplicationUser { get; set; }
        [ForeignKey("EditBy")]
        public virtual ApplicationUser EditApplicationUser { get; set; }
        
    }
}
