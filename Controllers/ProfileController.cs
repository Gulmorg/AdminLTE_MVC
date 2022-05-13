using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
