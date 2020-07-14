using CompWeb.Configurations.Email;
using CRMData.Configurations.Constants.Enums;
using CRMData.Configurations.Extensions;
using CRMData.Models.Identity;
using CRMData.Services.SystemAudit;
using CRMWeb.Areas.Account.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRMWeb.Areas.Account.Controllers
{
    [AllowAnonymous]
    [Area("Account")]
    public class LoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginController> _logger;
        private readonly IEmailService _emailService;
        private readonly ISystemAuditService _systemAuditService;

        public LoginController(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginController> logger,
            UserManager<ApplicationUser> userManager,
            IEmailService emailService,
            ISystemAuditService systemAuditService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailService = emailService;
            _systemAuditService = systemAuditService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            string returnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.UserName);
                //if (user.IsActive)
                //{
                //    ModelState.AddModelError(string.Empty, "YOUR ACCOUNT IS TEMPORARY DISABLED");
                //    return View();
                //}
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "INVALID LOGIN ATTEMPT.");
                    return View();
                }
                
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError(string.Empty, "EMAIL NOT CONFIRMED. PLEASE CONFIRM THE PROVIDED EMAIL TO LOGIN.");
                    return View();
                }

                bool isPassExpired = await _systemAuditService.IsPasswordExpirationDate(user.Id, user.PasswordHash);
                if (isPassExpired)
                {
                    _systemAuditService.LoginAudit(user.Id, AuditType.PasswordExpiredLoginFailed, "YOUR PASSWORD HAS BEEN EXPIRED.");
                    ModelState.AddModelError(string.Empty, "YOUR PASSWORD HAS BEEN EXPIRED.");
                    return View();
                }

                bool isIPWhiteListed = await _systemAuditService.IsIpWhiteList();
                if (!user.IsAllowRemoteLogin)
                {
                    if (!isIPWhiteListed)
                    {
                        _systemAuditService.LoginAudit(user.Id, AuditType.BlockListIPLoginFailed, "YOU ARE NOT AUTHORIZED TO LOGIN FROM THIS DOMAIN.");
                        ModelState.AddModelError(string.Empty, "YOU ARE NOT AUTHORIZED TO LOGIN FROM THIS DOMAIN.");
                        return View();
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _systemAuditService.LoginAudit(user.Id);
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    //return RedirectToPage("./Lockout");
                    ModelState.AddModelError(string.Empty, "USER ACCOUNT LOCKED OUT.");
                    return View();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "INVALID LOGIN ATTEMPT.");
                    return View();
                }

            }

            return View();
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            _systemAuditService.LogoutAudit();
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string password, string Npassword, string Cpassword)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(Npassword) || string.IsNullOrEmpty(Cpassword))
            {
                ModelState.AddModelError(string.Empty, "Fields cannot be empty.");
                return View();
            }

            if (!Npassword.Equals(Cpassword))
            {
                ModelState.AddModelError(string.Empty, "Password does not match with confirm password.");
                return View();
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, password, Npassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            //StatusMessage = "Your password has been changed.";

            return LocalRedirect("/");
        }

        [HttpGet]
        public IActionResult ForgetPassword(string page, string token, string email)
        {
            if (!string.IsNullOrEmpty(page))
            {
                return LocalRedirect($"{page}?token={token}&email={email}");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return LocalRedirect("/Account/Login/ForgotPasswordConfirmation");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


            var callbackUrl = Url.Page(
                "/Account/Login/ResetPassword",
                pageHandler: null,
                values: new { token = code, email = email },
                protocol: Request.Scheme);

            _emailService.SendForgetPasswordToken(email, callbackUrl);

            return LocalRedirect("/Account/Login/ForgotPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token = null)
        {
            if (token == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                ViewBag.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
                ViewBag.Email = email;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string token, string email, string Npassword, string Cpassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(Npassword) || string.IsNullOrEmpty(Cpassword))
            {
                ModelState.AddModelError(string.Empty, "Fields cannot be empty.");
                return View();
            }

            if (!Npassword.Equals(Cpassword))
            {
                ModelState.AddModelError(string.Empty, "Password does not match with confirm password.");
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return LocalRedirect("/");
            }

            var result = await _userManager.ResetPasswordAsync(user, token, Npassword);
            if (result.Succeeded)
            {
                return LocalRedirect("/");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

        public IActionResult EmailConfirmed() 
        {
            return View();
        }

        public IActionResult EmailConfirmationSent()
        {
            return View();
        }
    }
}