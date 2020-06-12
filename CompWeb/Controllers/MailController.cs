using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMWeb.Controllers
{
    [AllowAnonymous]
    public class MailController : Controller
    {
        [HttpPost, HttpGet]
        public IActionResult ForgetPasswordToken([FromBody] string code) 
        {
            return View("ForgetPasswordToken", code);
        }

        [HttpPost, HttpGet]
        public IActionResult EmailConfirmationToken([FromBody] string code)
        {
            return View("EmailConfirmationToken", code);
        }
    }
}