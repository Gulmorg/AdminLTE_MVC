using AdminLTE_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Controllers
{
    public class PartialViewController : Controller
    {
        private readonly MainNavigationViewModel mainNavigationViewModel;

        public PartialViewController(MainNavigationViewModel model)
        {
            this.mainNavigationViewModel = model;
        }

        const string COLLAPSED = "left";
        const string EXPANDED = "down";

        public IActionResult MainNavigation()
        {
            FlipArrow();

            return PartialView("AdminLTE/_MainNavigation", mainNavigationViewModel);
        }

        private void FlipArrow()
        {
            mainNavigationViewModel.ArrowDirection = mainNavigationViewModel.ArrowDirection == EXPANDED ? COLLAPSED : EXPANDED;
        }
    }
}
