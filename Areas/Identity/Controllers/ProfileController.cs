using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Index() => View();
    }
}
