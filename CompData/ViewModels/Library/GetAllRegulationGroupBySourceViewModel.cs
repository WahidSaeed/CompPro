using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.ViewModels.Library
{
    public class GetAllRegulationGroupBySourceViewModel
    {
        public int TypeId { get; set; }
        public string TypeTitle { get; set; }
        public int TotalRegulation { get; set; }
        public List<RegulationListItem> RegulationsList { get; set; }
    }

    public class RegulationListItem 
    {
        public int RegulationId { get; set; }
        public string RegulationTitle { get; set; }
        public long Views { get; set; }
    }


}
