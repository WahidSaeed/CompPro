using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.ViewModels.Procedure.Library
{
    [Table("SelectedRegulationRequirement", Schema = "ProcedureView")]
    public class SelectedRegulationRequirement
    {
        public int CommentID { get; set; }
        public string Requirement { get; set; }
    }
}
