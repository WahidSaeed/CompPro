using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.ViewModels.Library
{
    public class SaveRegulationViewModel
    {
        public string ReferenceNumber { get; set; }
        public string Title { get; set; }
        public DateTime IssueDate { get; set; }
        public bool IsFinal { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int SourceID { get; set; }
        public int TypeID { get; set; }
    }
}
