using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.ViewModels.Library
{
    public class SelectedRegulationViewModel
    {
        public int RegId { get; set; }
        public string RegTitle { get; set; }
        public int SourceId { get; set; }
        public string SourceTitle { get; set; }
        public List<SectionItem> SectionItems { get; set; }
    }

    public class SectionItem
    {
        public int SectionId { get; set; }
        public string SectionTitle { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public int Sequence { get; set; }
    }
}
