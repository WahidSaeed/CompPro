using CompData.Configurations.Constants.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.Models.Library
{

    [Table("TagMapType", Schema = "Library")]
    public class TagMapType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public TagType Id { get; set; }
        [MaxLength(25)]
        public string Type { get; set; }

        public virtual ICollection<TagMap> TagMaps { get; set; }
    }
}
