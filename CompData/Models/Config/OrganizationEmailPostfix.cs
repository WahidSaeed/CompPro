using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Config
{
    [Table("OrganizationDomain", Schema = "Config")]
    public class OrganizationDomain
    {
        [Key]
        public int Id { get; set; }
        public string Domain { get; set; }
    }
}
