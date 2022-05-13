using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminLTE_MVC.Controllers
{
    public class MessageDropdownController : Controller, IDropdownController
    {
        public IActionResult Index()
        {//Creating the List of SelectListItem, this list you can bind from the database.
            List<SelectListItem> cities = new()
            {
                new SelectListItem { Value = "1", Text = "Latur" },
                new SelectListItem { Value = "2", Text = "Solapur" },
                new SelectListItem { Value = "3", Text = "Nanded" },
                new SelectListItem { Value = "4", Text = "Nashik" },
                new SelectListItem { Value = "5", Text = "Nagpur" },
                new SelectListItem { Value = "6", Text = "Kolhapur" },
                new SelectListItem { Value = "7", Text = "Pune" },
                new SelectListItem { Value = "8", Text = "Mumbai" },
                new SelectListItem { Value = "9", Text = "Delhi" },
                new SelectListItem { Value = "10", Text = "Noida" }
            };

            //assigning SelectListItem to view Bag
            ViewBag.cities = cities;
            return View();
        }
    }
}