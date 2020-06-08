using Microsoft.AspNetCore.Mvc;

namespace CRMWeb.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccessDeniedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}