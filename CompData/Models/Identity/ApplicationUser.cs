using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMData.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        public Guid AddBy { get; set; }
        [Required]
        public string AddIP { get; set; }
        [Required]
        public DateTime AddOn { get; set; }
        public Guid EditBy { get; set; }
        public DateTime EditOn { get; set; }
        public string EditIP { get; set; }
    }
}
