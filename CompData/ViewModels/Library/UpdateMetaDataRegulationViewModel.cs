using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.ViewModels.Library
{
    public class UpdateMetaDataRegulationViewModel
    {
        public int RegId { get; set; }
        public string CustomURL { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }
        public string Summary { get; set; }
    }
}
