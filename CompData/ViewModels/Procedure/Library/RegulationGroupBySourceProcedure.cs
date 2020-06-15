using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.ViewModels.Procedure.Library
{
    [Table("RegulationGroupBySourceProcedure", Schema = "ProcedureView")]
    public class RegulationGroupBySourceProcedure
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int RegId { get; set; }
        public string RegTitle { get; set; }
        public int Regcount { get; set; }
        public long Views { get; set; }
    }
}
