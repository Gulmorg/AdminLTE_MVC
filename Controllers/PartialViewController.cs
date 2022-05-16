using AdminLTE_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Controllers
{
    public class PartialViewController : Controller
    {
        const string COLLAPSED = "left";
        const string EXPANDED = "down";

        private readonly MainNavigationViewModel model = new();

        public IActionResult MainNavigation()
        {
            if (model.ArrowDirection == null)
            {
                Console.WriteLine("was null, now collapsed");
                model.ArrowDirection = COLLAPSED;
            }
            else
            {
                if ((string?)model.ArrowDirection == EXPANDED)
                {
                    Console.WriteLine("was exp, now col");
                    model.ArrowDirection = COLLAPSED;
                }
                else
                {
                    Console.WriteLine("was col, now exp");
                    model.ArrowDirection = EXPANDED;
                }
            }
            return PartialView("AdminLTE/_MainNavigation", model);
        }
    }
}
