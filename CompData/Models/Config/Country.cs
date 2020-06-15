using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Config
{
    [Table("Country", Schema = "Config")]
    public class Country
    {
        [Key]
        [MaxLength(3)]
        public string CountryId { get; set; }
        [MaxLength(250), Required]
        public string CountryName { get; set; }
    }
}
