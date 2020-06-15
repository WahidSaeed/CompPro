using CRMData.Configurations.Constants.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRMData.Models.Identity
{
    [Table("ApplicationIP", Schema = "security")]
    public class ApplicationIP
    {
        [Key]
        public int Id { get; set; }

        public string IP { get; set; }

        public IPType IPType { get; set; }
    }
}
