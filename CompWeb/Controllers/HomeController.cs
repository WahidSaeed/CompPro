using CompData.Services.Regulation;
using CRMConfiguration.Configurations.Attributes.Identity;
using CRMData.Configurations.Attributes.Identity;
using CRMData.Services.Account.ProfileClaimConfiguration;
using CRMData.ViewModels;
using CRMWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace CRMWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRegulationService regulationService;
        //private readonly IProfileClaimConfiguration profileClaimConfiguration;

        //public HomeController(ILogger<HomeController> logger, IProfileClaimConfiguration profileClaimConfiguration)
        //{
        //    this._logger = logger;
        //    this.profileClaimConfiguration = profileClaimConfiguration;
        //}

        public HomeController(ILogger<HomeController> logger, IRegulationService regulationService)
        {
            this._logger = logger;
            this.regulationService = regulationService;
        }

        public IActionResult Index()
        {
            var model = this.regulationService.GetAllRegulationGroupBySource();
            return View(model);
        }
    }
}
