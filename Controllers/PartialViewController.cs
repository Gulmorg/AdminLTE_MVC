using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Controllers
{
    public class PartialViewController : Controller
    {
        const string COLLAPSED = "left";
        const string EXPANDED = "down";
        public IActionResult MainNavigation()
        {
            if ((string?)ViewData["ArrowDirection"] == null)
            {
                Console.WriteLine("was null, now collapsed");
                ViewData["ArrowDirection"] = COLLAPSED;
            }
            else
            {
                if ((string?)ViewData["ArrowDirection"] == EXPANDED)
                {
                    Console.WriteLine("was exp, now col");
                    ViewData["ArrowDirection"] = COLLAPSED;
                }
                else
                {
                    Console.WriteLine("was col, now exp");
                    ViewData["ArrowDirection"] = EXPANDED;
                }
            }
            return PartialView("AdminLTE/_MainNavigation");
        }
    }
}
