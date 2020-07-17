using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMWeb.Areas.Account.Controllers
{
    [AllowAnonymous]
    [Area("Account")]
    public class AccessDeniedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ApplicationDown() 
        {
            return View();
        }
    }
}