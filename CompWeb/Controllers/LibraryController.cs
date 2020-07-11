using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompData.Services.Regulation;
using CompData.ViewModels.Library;
using CRMData.Configurations.Constants.Enums;
using CRMData.Configurations.Generics;
using CRMData.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompWeb.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IRegulationService regulationService;
        private readonly UserManager<ApplicationUser> userManager;
        public LibraryController(IRegulationService regulationService, UserManager<ApplicationUser> userManager) 
        {
            this.regulationService = regulationService;
            this.userManager = userManager;
        }

        public IActionResult Source(int id)
        {
            var model = this.regulationService.GetAllRegulationFilteredBySourceID(id);
            return View(model);
        }
        
        [Route("/Library/Type/{sourceId}-{typeId}")]
        public IActionResult Type(int sourceId, int typeId)
        {
            var model = this.regulationService.GetAllRegulationFilteredBySourceID(sourceId, typeId);
            return View(model);
        }

        public IActionResult Regulation(int id)
        {
            var model = this.regulationService.GetSelectedRegulation(id);
            ViewBag.Requirments = this.regulationService.GetSelectedRegRequirement(id);
            return View(model);
        }


        public async Task<IActionResult> SelectSources() 
        {
            var user = await userManager.GetUserAsync(User);
            var model = this.regulationService.GetRegulationSourcesByCountryCode(user.CountryCode);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SelectSources(List<int> SourceIds)
        {
            var user = await userManager.GetUserAsync(User);
            var result = this.regulationService.LinkUserByRegulationSource(user.Id, SourceIds);
            if (result.Status == ResultStatus.Error)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            return LocalRedirect("/");
        }

        [HttpPost]
        public async Task<JsonResult> Subscribe(int typeId, int sourceId)
        {
            var user = await userManager.GetUserAsync(User);
            var result = this.regulationService.SubscribeRegulationTypeByUser(user.Id, typeId, sourceId);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> SaveRegulation(SaveRegulationViewModel viewModel)
        {
            var result = await this.regulationService.SaveRegulation(viewModel);
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetSummary(int id)
        {
            var model = this.regulationService.GetSelectedRegSummary(id);
            return View(model);
        }
    }
}