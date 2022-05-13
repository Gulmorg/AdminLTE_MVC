using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
