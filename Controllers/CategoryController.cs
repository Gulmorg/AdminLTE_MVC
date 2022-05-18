using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Controllers
{
    public class CategoryController : Controller
    {
        public void Index()
        {
            
        }
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
