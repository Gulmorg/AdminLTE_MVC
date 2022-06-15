using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Controllers
{
    //[Authorize]
    public class CategoryController : Controller
    {
        public void Index() { }

        public IActionResult PageOne()
        {
            return View();
        }

        public IActionResult PageTwo()
        {
            return View();
        }
    }
}
