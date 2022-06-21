using AdminLTE_MVC.Models.Dashboard;
using AdminLTE_MVC.Snmp;
using Microsoft.AspNetCore.Html;

namespace AdminLTE_MVC.Helpers.Generators
{
    public class ListGenerator
    {
        public ListGenerator(Target targetTemplate) => _elementNames = new List<string>(SnmpManager.WalkValue(targetTemplate.ChangeOid(SnmpManager.GetOid("Name"))));

        private readonly List<string> _elementNames;

        public IHtmlContent Generate()
        {
            var output = string.Empty;
            foreach (var elementName in _elementNames)
            {
                output += $"<option>{elementName}</option>";
            }
            if (string.IsNullOrEmpty(output)) output = $"<option>!!!TEMP!!! SNMP UNREACHABLE ERROR MESSAGE</option>";
            return new HtmlString(output);
        }
    }
}
