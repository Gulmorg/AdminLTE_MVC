using AdminLTE_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Controllers
{
    public class PartialViewController : Controller
    {
        private readonly MainNavigationViewModel model;

        public PartialViewController(MainNavigationViewModel model)
        {
            this.model = model;
        }

        const string COLLAPSED = "left";
        const string EXPANDED = "down";

        public IActionResult MainNavigation()
        {
            if (model.ArrowDirection == EXPANDED)
            {
                Console.WriteLine("was exp, now col");
                model.ArrowDirection = COLLAPSED;
            }
            else
            {
                Console.WriteLine("was col, now exp");
                model.ArrowDirection = EXPANDED;
            }
            
            return PartialView("AdminLTE/_MainNavigation", model);
        }
    }
}
