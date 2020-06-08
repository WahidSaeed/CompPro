using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRMData.ViewModels
{
    [NotMapped]
    public class XMLViewModel
    {
        public int Value { get; set; }
        public string XMLBody { get; set; }
    }
}
