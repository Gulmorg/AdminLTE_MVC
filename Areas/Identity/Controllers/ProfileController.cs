using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Controllers
{
    [Authorize]
    [Area("Identity")]
    public class ProfileController : Controller
    {
        public IActionResult Index() => View();
    }
}
