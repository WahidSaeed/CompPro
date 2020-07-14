using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompData.Configurations.Constants.Enums;
using CompData.Services.Regulation;
using CompData.ViewModels;
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

        #region Search Grid List
        public async Task<IActionResult> Index(string query)
        {
            var detailTag = await regulationService.GetAllTagFilters(null, null, CompData.Configurations.Constants.Enums.TagType.DetailTag);
            var bussinessLineTag = await regulationService.GetAllTagFilters(null, null, CompData.Configurations.Constants.Enums.TagType.BussinessLineTag);

            ViewBag.DetailTag = detailTag.Data;
            ViewBag.BussinessLineTag = bussinessLineTag.Data;
            ViewBag.Query = query;

            return View();
        }

        public async Task<IActionResult> Source(int id)
        {

            var detailTag = await regulationService.GetAllTagFilters(id, null, CompData.Configurations.Constants.Enums.TagType.DetailTag);
            var bussinessLineTag = await regulationService.GetAllTagFilters(id, null, CompData.Configurations.Constants.Enums.TagType.BussinessLineTag);

            ViewBag.SourceId = id;
            ViewBag.DetailTag = detailTag.Data;
            ViewBag.BussinessLineTag = bussinessLineTag.Data;

            return View();
        }

        [Route("/Library/Source/{sourceId}/Type/{typeId}")]
        public async Task<IActionResult> Type(int sourceId, int typeId)
        {
            var detailTag = await regulationService.GetAllTagFilters(sourceId, typeId, TagType.DetailTag);
            var bussinessLineTag = await regulationService.GetAllTagFilters(sourceId, typeId, TagType.BussinessLineTag);

            ViewBag.SourceId = sourceId;
            ViewBag.TypeId = typeId;
            ViewBag.DetailTag = detailTag.Data;
            ViewBag.BussinessLineTag = bussinessLineTag.Data;

            return View();
        }

        public async Task<JsonResult> SourceGrid(SourceGrid sourceGrid) 
        {
            var user = await userManager.GetUserAsync(User);
            var model = this.regulationService.GetAllRegulationFilteredBySourceID(sourceGrid, user.Id);
            return Json(model);
        }

        #endregion

        #region View Regulation
        public async Task<IActionResult> Regulation(int id)
        {
            var model = this.regulationService.GetSelectedRegulation(id);
            var detailTag = await regulationService.GetAllTagFiltersByRegId(id, TagType.DetailTag);

            ViewBag.Requirments = this.regulationService.GetSelectedRegRequirement(id);
            ViewBag.DetailTag = detailTag.Data;
            ViewBag.RegId = id;

            return View(model);
        }

        public PartialViewResult _GetFilteredDetails(int id, string searchTerm, List<string> detailTag)
        {
            var model = this.regulationService.GetSelectedRegulation(id, searchTerm, detailTag);
            return PartialView("_RegulationDetail", model);
        }

        [Route("/Library/Regulation/{id}/Edit")]
        public IActionResult RegulationEdit(int id)
        {
            var model = this.regulationService.GetSelectedRegulation(id);
            ViewBag.RegId = id;
            return View(model);
        }
        #endregion

        public async Task<IActionResult> SelectSources() 
        {
            var user = await userManager.GetUserAsync(User);
            var model = this.regulationService.GetRegulationSourcesByCountryCode(user.CountryCode);
            return View(model);
        }

        public async Task<JsonResult> GetAllRegulations(AjaxDropDown ajaxDropDown)
        {
            var result = await this.regulationService.GetAllRegulations(ajaxDropDown);
            return Json(result.Data);
        }

        [HttpPost]
        public async Task<JsonResult> SuggestRegulations(string searchTerm)
        {
            var user = await userManager.GetUserAsync(User);
            var result = await this.regulationService.GetSuggestedRegulationsByUserSource(user.Id, searchTerm);
            return Json(result);
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

        #region Subscription
        [HttpPost]
        public async Task<JsonResult> SubscribeByUser(int typeId, int sourceId)
        {
            var user = await userManager.GetUserAsync(User);
            var result = this.regulationService.SubscribeRegulationTypeByUser(user.Id, typeId, sourceId);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Subscribe(int regId)
        {
            var user = await userManager.GetUserAsync(User);
            var result = this.regulationService.SubscribeRegulationByUser(user.Id, regId);
            return Json(result);
        }
        #endregion

        #region Save Regulations
        [HttpPost]
        public async Task<JsonResult> SaveRegulation(SaveRegulationViewModel viewModel)
        {
            var result = await this.regulationService.SaveRegulation(viewModel);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> SaveRegulationDetail(SaveRegulationDetailViewModel viewModel)
        {
            var result = await this.regulationService.SaveRegulationDetail(viewModel);
            return Json(result);
        }
        #endregion

        #region Link Tags
        [Route("/Library/GetTagsGroup/{tagGroupId}")]
        public async Task<JsonResult> GetTagsGroup(string tagGroupId)
        {
            var result = await this.regulationService.GetTagsGroup(tagGroupId);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> SetTagsGroup(List<string> tags, string tagGroupId, int regId, int secId, int descId)
        {
            var result = await this.regulationService.SetTagsGroup(tags, tagGroupId, regId, secId, descId);
            return Json(result);
        }
        #endregion

        #region Meta Tags
        [HttpPost]
        public async Task<JsonResult> UpdateMetaDetails(UpdateMetaDataRegulationViewModel viewModel)
        {
            var result = await this.regulationService.UpdateMetaDetails(viewModel);
            return Json(result);
        }
        #endregion

        public PartialViewResult GetSummary(int id)
        {
            var model = this.regulationService.GetSelectedRegSummary(id);
            return PartialView("_GetSummary", model);
        }
    }
}