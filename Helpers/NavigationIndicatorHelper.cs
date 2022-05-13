using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Helpers
{
    public static class NavigationIndicatorHelper
    {
        public static string MakeActiveClass(this IUrlHelper urlHelper, string controller, string action)
        {
            try
            {
                string controllerName = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
                string methodName = urlHelper.ActionContext.RouteData.Values["action"].ToString();

                if (string.IsNullOrEmpty(controllerName)) return null;

                if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                {
                    if (methodName.Equals(action, StringComparison.OrdinalIgnoreCase))
                    {
                        return "active";
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
