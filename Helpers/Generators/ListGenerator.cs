using AdminLTE_MVC.Data.FakeDatabase;
using AdminLTE_MVC.Managers;
using AdminLTE_MVC.Models.Dashboard;
using Microsoft.AspNetCore.Html;

namespace AdminLTE_MVC.Helpers.Generators
{
    public class ListGenerator
    {
        public ListGenerator(Target targetTemplate) => _elementNames = new List<string>(SnmpManager.WalkValue(targetTemplate.ChangeOid(FakeData.NAME_OID)));

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
