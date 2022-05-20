using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Areas.Identity.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [Route("Identity/Account/Login")]
        public IActionResult Login()
        {
            Console.WriteLine("login works");
            return View();
        }

        [AllowAnonymous]
        [Route("Identity/Account/Register")]
        public IActionResult Register()
        {
            Console.WriteLine("register works");
            return View();
        }
    }
}
