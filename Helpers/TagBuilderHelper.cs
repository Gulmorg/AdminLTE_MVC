using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AdminLTE_MVC.Helpers
{
    public static class TagBuilderHelper
    {
        public static IHtmlContent CreateTag(string tagType, string @class, string? id = null)
        {
            if (string.IsNullOrEmpty(@class)) @class = string.Empty;
            return Div(tagType, @class, id);
        }

        public static IHtmlContent Div(string tagType, string @class, string? id)
        {
            // Create tag builder
            var builder = new TagBuilder(tagType);

            // Add class and id
            builder.AddCssClass(@class);
            builder.GenerateId(id, string.Empty);

            // Render tag
            builder.TagRenderMode = TagRenderMode.SelfClosing;
            return builder.RenderSelfClosingTag();
        }
    }
}
