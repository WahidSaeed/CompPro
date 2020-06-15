using CompData.Services.Regulation;
using CRMConfiguration.Configurations.Attributes.Identity;
using CRMData.Configurations.Attributes.Identity;
using CRMData.Configurations.Constants.Enums;
using CRMData.Configurations.Generics;
using CRMData.Models.Identity;
using CRMData.Services.Account.ProfileClaimConfiguration;
using CRMData.ViewModels;
using CRMWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRegulationService regulationService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ILogger<HomeController> logger, IRegulationService regulationService, UserManager<ApplicationUser> userManager)
        {
            this._logger = logger;
            this.regulationService = regulationService;
            this.userManager = userManager;
    }

        public async Task<IActionResult> Index(int? sourceId = null)
        {
            var user = await userManager.GetUserAsync(User);
            var sources = this.regulationService.GetSelectedRegulationSourcesByUserId(user.Id);
            if (!(sources.Count > 0))
            {
                return Redirect("Library/SelectSources");
            }

            var firstDefaultSource = sources.FirstOrDefault();
            int defaultSourceId = (sourceId == null ? firstDefaultSource.SourceId : sourceId.GetValueOrDefault());
            var model = this.regulationService.GetAllRegulationGroupBySource(defaultSourceId);

            ViewBag.Sources = sources;
            ViewBag.SourceSelected = defaultSourceId;
            ViewBag.UpdatedRegulation = this.regulationService.GetUpdatedRegulationsBySource(defaultSourceId);
            ViewBag.SubscribedRegType = this.regulationService.GetSubscribedRegulationTypeByUserId(user.Id, defaultSourceId);
            ViewBag.DefaultSourceId = defaultSourceId;

            return View(model);
        }
    }
}
