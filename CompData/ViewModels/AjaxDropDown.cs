using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.ViewModels
{
    public class AjaxDropDown
    {
        public string term { get; set; }
        public string q { get; set; }
        public string _type { get; set; }
        public int page { get; set; } = 1;
    }
}
