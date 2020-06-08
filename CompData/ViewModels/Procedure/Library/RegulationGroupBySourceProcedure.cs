using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.ViewModels.Procedure.Library
{
    [Table("RegulationGroupBySourceProcedure", Schema = "ProcedureView")]
    public class RegulationGroupBySourceProcedure
    {
        public int SourceId { get; set; }
        public string FullName { get; set; }
        public int RegId { get; set; }
        public string RegTitle { get; set; }
        public int Regcount { get; set; }
    }
}
