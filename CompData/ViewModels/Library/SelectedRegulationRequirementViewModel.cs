using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.ViewModels.Library
{
    public class SelectedRegulationRequirementViewModel
    {
        public int RegID { get; set; }
        public int? CommentTypeID { get; set; }
        public List<SectionItemRequirement> SectionItemRequirement { get; set; }

    }
    public class SectionItemRequirement
    {
        public Int64? CommentID { get; set; }
        public string Requirement { get; set; }
    }
}
