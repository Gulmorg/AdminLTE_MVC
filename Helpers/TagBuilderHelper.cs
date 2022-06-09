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

        //public static IHtmlContent StringBuilderMethod()
        //{
        //    var sb = new StringBuilder();
        //    sb.AppendLine("<div class='control-group'>");
        //    sb.AppendFormat(" <label class='control-label' for='{0}_{1}'>{2}</label>", propObj.ModelType, propObj.ModelProperty, propObj.LabelCaption);
        //    sb.AppendLine("  <div class='controls'>");
        //    sb.AppendFormat("    <input id='{0}_{1}' name='{0}[{1}]' value='{2}' />", propObj.ModelType, propObj.ModelProperty, propObj.PropertyValue);
        //    sb.AppendLine("  </div>");
        //    sb.AppendLine("</div>");

        //    return new HtmlString(sb.ToString());
        //}
    }
}
