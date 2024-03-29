﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompData.ViewModels.Procedure.Library
{
    [Table("SelectedRegulationProcedure", Schema = "ProcedureView")]
    public class SelectedRegulationProcedure
    {
        public int RegId { get; set; }
        public string RegTitle { get; set; }
        public int? SourceId { get; set; }
        public string FullName { get; set; }
        public int? SectionId { get; set; }
        public string SectionTitle { get; set; }
        public int? ParentId { get; set; }
        public int? Sequence { get; set; }
        public int? RegDescId { get; set; }
        public string RegDescription { get; set; }
        public int? DescSequence { get; set; }
        public string RegTypeName { get; set; }
        public int RegTypeId { get; set; }
        public string Summary { get; set; }
        public string CustomURL { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }
    }
}
