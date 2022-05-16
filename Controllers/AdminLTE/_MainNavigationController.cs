using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Controllers
{
    public class _MainNavigationController : Controller
    {
        [Route("{controller=AdminLTE/_MainNavigation}/{action=Index}/{id?}")]
        public IActionResult Index()
        {
            return View("AdminLTE/_MainNavigation");
        }
    }
}
