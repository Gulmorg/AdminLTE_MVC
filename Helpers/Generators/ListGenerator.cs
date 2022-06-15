using AdminLTE_MVC.Models.Dashboard;
using AdminLTE_MVC.Snmp;
using Microsoft.AspNetCore.Html;

namespace AdminLTE_MVC.Helpers.Generators
{
    public class ListGenerator
    {
        public ListGenerator(List<Target> targets)
        {
            foreach (var target in targets)
            {
                titles.Add(SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("Name"))).ToString());
            }
        }

        private List<string> titles = new();


        public IHtmlContent Generate()
        {
            var output = string.Empty;
            foreach (var title in titles)
            {
                output += $"<option>{title}</option>";
            }
            return new HtmlString(output);
        }
    }
}
