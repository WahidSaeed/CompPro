using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.ViewModels.Library
{
    public class SaveRegulationDetailViewModel
    {
        public int RegId { get; set; }
        public List<SectionDetailViewModel> Sections { get; set; }
    }

    public class SectionDetailViewModel 
    {
        public int? SecId { get; set; }
        public string Title { get; set; }
        public int Seq { get; set; }
        public int? parentId { get; set; }
        public List<DescriptionViewModel> Descriptions { get; set; }
        public List<SectionDetailViewModel> Children { get; set; }
    }

    public class DescriptionViewModel
    {
        public int? DescId { get; set; }
        public string Description { get; set; }
        public int Seq { get; set; }
    }
}
