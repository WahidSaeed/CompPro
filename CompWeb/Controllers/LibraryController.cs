using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompData.Services.Regulation;
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

        public IActionResult Regulation(int id)
        {
            var model = this.regulationService.GetSelectedRegulation(id);
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

    }
}