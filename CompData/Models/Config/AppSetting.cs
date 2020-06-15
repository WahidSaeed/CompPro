using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Config
{
    [Table("AppSetting", Schema = "Config")]
    public class AppSetting
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
