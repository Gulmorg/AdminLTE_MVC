using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text;

namespace AdminLTE_MVC.Helpers
{
    public static class TagBuilderHelper
    {
        public static IHtmlContent SelfClosingTag(string tagType, string @class, string id = "")
        {
            // Create tag builder
            var builder = new TagBuilder(tagType);

            // Add class and id
            builder.AddCssClass(@class);
            builder.GenerateId(id, string.Empty);

            // Render tag
            builder.TagRenderMode = TagRenderMode.SelfClosing;
            builder.MergeAttribute("id", id);
            return builder.RenderSelfClosingTag();
        }

        public static IHtmlContent StartTag(string tagType, string @class, string id = "")
        {
            // Create tag builder
            var builder = new TagBuilder(tagType);

            // Add class and id
            builder.AddCssClass(@class);
            builder.GenerateId(id, string.Empty);

            // Render tag
            builder.TagRenderMode = TagRenderMode.StartTag;
            return builder.RenderStartTag();
        }

        public static IHtmlContent EndTag(string tagType)
        {
            var builder = new TagBuilder(tagType);
            builder.TagRenderMode = TagRenderMode.EndTag;
            return builder.RenderEndTag();
        }
    }
}
