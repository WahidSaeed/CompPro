using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.ViewModels.Procedure.Library
{
    [Table("RegulationFilteredBySource", Schema = "ProcedureView")]
    public class RegulationFilteredBySource
    {
        public int RegId { get; set; }
        public string RegulationTitle { get; set; }
        public int SourceId { get; set; }
        public string FullName { get; set; }
    }
}
