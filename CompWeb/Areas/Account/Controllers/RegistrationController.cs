using CompData.ViewModels;
using CompWeb.Configurations.Email;
using CRMData.Configurations.Extensions;
using CRMData.Data;
using CRMData.Models.Identity;
using CRMWeb.Areas.Account.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CRMWeb.Areas.Account.Controllers
{
    [AllowAnonymous]
    [Area("Account")]
    public class RegistrationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<RegistrationController> _logger;
        private readonly IEmailService _emailService;
        private readonly Utility _utility;

        public RegistrationController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegistrationController> logger,
            IEmailService emailService,
            Utility utility,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailService = emailService;
            _utility = utility;
        }

        [ViewData]
        public string ReturnURL { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl, string page = null, string email = null, string token = null)
        {
            if (!string.IsNullOrEmpty(page) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(token))
            {
                var user = await _userManager.FindByEmailAsync(email);
                var changePasswordResult = await _userManager.ConfirmEmailAsync(user, token);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                return LocalRedirect(page);
            }

            ReturnURL = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                string publicIP = await _utility.GetRequestAsync("http://bot.whatismyipaddress.com");
                GeolocationIPViewModel geolocationIPView = await _utility.GetRequestAsync<GeolocationIPViewModel>($"https://api.ipfind.com/?ip={publicIP}&auth=f8b657f2-6ba1-41d0-87f3-340ecba1950d");

                if (!_utility.IsCountry(geolocationIPView.CountryCode))
                {
                    ModelState.AddModelError(string.Empty, "The service is not available in your country yet. Please try again later");
                    return View();
                }

                string domain = registerViewModel.UserName.Split("@")[1];
                if (!_utility.IsDomain(domain))
                {
                    ModelState.AddModelError(string.Empty, "Your email association with the domain is not registered in the system right now.");
                    return View();
                }

                var user = new ApplicationUser { 
                    UserName = registerViewModel.UserName, 
                    Email = registerViewModel.UserName, 
                    AddIP = HttpContext.Connection.RemoteIpAddress.ToString(),
                    CountryCode = geolocationIPView.CountryCode,
                    Region = geolocationIPView.Region,
                    City = geolocationIPView.City
                };
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Page(
                        "/Account/Login/EmailConfirmed",
                        pageHandler: null,
                        values: new { token = code, email = user.Email },
                        protocol: Request.Scheme);

                    _emailService.SendEmailConfirmationToken(user.Email, callbackUrl);

                    return LocalRedirect("/Account/Login/EmailConfirmationSent");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }
    }
}