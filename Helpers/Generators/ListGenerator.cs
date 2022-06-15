using Microsoft.AspNetCore.Html;

namespace AdminLTE_MVC.Helpers.Generators
{
    public class ListGenerator
    {
        public IHtmlContent Generate()
        {
            var test = "test";
            var output = $"<option>{test}</option>  " +
                         $"<option>{test}</option>  " +
                         $"<option>{test}</option>  " +
                         $"<option>{test}</option>  " +
                         $"<option>{test}</option>  ";
            return new HtmlString(output);
        }
    }
}
