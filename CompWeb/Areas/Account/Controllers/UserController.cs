using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CompData.Services.Account;
using CompData.Services.Regulation;
using CRMData.Configurations.Generics;
using CRMData.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompWeb.Areas.Account.Controllers
{
    [Area("Account")]
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IRegulationService _regulationService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IAccountService accountService, UserManager<ApplicationUser> userManager, IRegulationService regulationService)
        {
            this._accountService = accountService;
            this._userManager = userManager;
            this._regulationService = regulationService;
        }

        public async Task<IActionResult> Index(string userId)
        {
            var user = await this._userManager.FindByEmailAsync(userId);
            ViewBag.UserId = userId;
            return View(user);
        }

        public async Task<IActionResult> Settings(string userId)
        {
            var user = await this._userManager.FindByEmailAsync(userId);
            var regulationSources = this._regulationService.GetRegulationSourcesByCountryCode(user.CountryCode);
            var selectedRegulationSources = this._regulationService.GetSelectedRegulationSourcesByUserId(user.Id);

            ViewBag.UserId = userId;
            ViewBag.RegulationSources = regulationSources;
            ViewBag.SelectedRegulationSources = selectedRegulationSources;

            return View(user);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateUserProfileData(string userEmail, string fullName, string phone, string designation, string about, string websiteURL, bool isActive) {
            var result = await this._accountService.UpdateUserProfileData(userEmail, fullName, phone, designation, about, websiteURL, isActive);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateSelectedSource(string userEmail, List<int> SourceIds) 
        {
            var user = await this._userManager.FindByEmailAsync(userEmail);
            var result = this._regulationService.LinkUserByRegulationSource(user.Id, SourceIds);
            return Json(result);
        }

    }
}