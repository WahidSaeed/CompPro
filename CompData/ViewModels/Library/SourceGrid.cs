using CRMData.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.ViewModels.Library
{
    public class SourceGrid: jQueryDataTableInput
    {
        public int? SourceId { get; set; }
        public int? TypeId { get; set; }
        public string SearchTerm { get; set; }
        public List<int> Years { get; set; } = new List<int>();
        public List<string> DetailTags { get; set; } = new List<string>();
        public List<string> BussinessLineTags { get; set; } = new List<string>();
    }
}
