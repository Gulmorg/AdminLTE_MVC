using Microsoft.AspNetCore.Mvc;

namespace AdminLTE_MVC.Helpers
{
    public static class NavigationIndicatorHelper
    {
        private const string BYPASS = "_any_";
        public static string MakeActiveClass(this IUrlHelper urlHelper, string controller, string action)
        {
            try
            {
                string controllerName = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
                string methodName = urlHelper.ActionContext.RouteData.Values["action"].ToString();

                if (string.IsNullOrEmpty(controllerName)) return string.Empty;

                if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                {
                    if (methodName == null) return string.Empty;
                    if (methodName.Equals(action, StringComparison.OrdinalIgnoreCase) || action.Equals(BYPASS, StringComparison.OrdinalIgnoreCase))
                    {
                        return "active";
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string MakeMenuOpenClass(this IUrlHelper urlHelper, string controller)
        {
            try
            {
                if (urlHelper.ActionContext.RouteData.Values["controller"] == null)
                {
                    return string.Empty;
                }

                string result = "menu-is-opening menu-open";
                string controllerName = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
                if (string.IsNullOrEmpty(controllerName))
                {
                    return string.Empty;
                }

                if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                {
                    return result;
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string MakeAreaActiveClass(this IUrlHelper urlHelper, string area, string page)
        {
            try
            {
                if (urlHelper.ActionContext.RouteData.Values["area"] == null)
                    return string.Empty;

                string result = "active";
                string areaName = urlHelper.ActionContext.RouteData.Values["area"].ToString();
                string pageName = urlHelper.ActionContext.RouteData.Values["page"]?.ToString();
                if (string.IsNullOrEmpty(areaName)) return string.Empty;
                if (areaName.Equals(area, StringComparison.OrdinalIgnoreCase))
                    if (pageName.Equals(page, StringComparison.OrdinalIgnoreCase) || pageName.Equals(BYPASS, StringComparison.OrdinalIgnoreCase))
                        return result;

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
