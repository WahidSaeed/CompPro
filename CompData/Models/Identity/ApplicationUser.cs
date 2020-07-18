using CompData.Models.Config;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string City { get; set; }
        [MaxLength(3)]
        public string CountryCode { get; set; }
        public string Region { get; set; }
        public bool IsAllowRemoteLogin { get; set; } = true;
        public bool IsActive { get; set; } = true;
        [MaxLength(200)]
        public string Designation { get; set; }
        public string About { get; set; }
        [MaxLength(200)]
        public string WebsiteURL { get; set; }
        [MaxLength(200)]
        public string FullName { get; set; }

        [ForeignKey("CountryCode")]
        public virtual Country Country { get; set; }


    }
}
