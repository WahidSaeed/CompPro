using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompData.Services.Regulation;
using Microsoft.AspNetCore.Mvc;

namespace CompWeb.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IRegulationService regulationService;
        public LibraryController(IRegulationService regulationService) 
        {
            this.regulationService = regulationService;
        }

        public IActionResult Source(int id)
        {
            var model = this.regulationService.GetAllRegulationFilteredBySourceID(id);
            return View(model);
        }

        public IActionResult Regulation(int id)
        {
            var model = this.regulationService.GetSelectedRegulation(id);
            return View(model);
        }
    }
}