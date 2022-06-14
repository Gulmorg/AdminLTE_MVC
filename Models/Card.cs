using AdminLTE_MVC.Helpers;
using AdminLTE_MVC.Snmp;
using Microsoft.AspNetCore.Html;

namespace AdminLTE_MVC.Models
{
    public class Card
    {
        public static byte CardCount { get; private set; }

        public Card(string type, string title)
        {
            id = CardCount++;

            cardType = type;    // css class string (header background colour)
            cardTitle = SnmpManager.GetValue(new Target("192.168.2.11", "public", "").ChangeOid(SnmpManager.SetOid("Name"))).ToString();    // HARDCODED VALUES
            SetValueSuffix();
            SetFasIcon();
        }

        ~Card() => CardCount--;                                 // probably doesn't work on page reload
        public static void ResetCardCount() => CardCount = 0;   // therefore, ResetCardCount() is called on dashboard reloads

        private readonly byte id;
        private readonly string cardType;
        private readonly string cardTitle;
        private string valueSuffix;
        private string faIcon;

        public IHtmlContent Generate(Target target) // TODO: fix value/breakpoints mismatch
        {
            var output = $"<!-- Card 1 -->" +
                         $"<div class=\"card card-{cardType}\" runat=\"server\">" +
                         $"  <div class=\"card-header\">" +
                         $"    <h3 class=\"card-title\"><i class=\"fas fa-{faIcon} mr-2\"></i>{cardTitle}</h3>" +
                         $"    <div class=\"card-tools\">" +
                         $"      <button type = \"button\" class=\"btn btn-tool\" data-card-widget=\"collapse\">" +
                         $"        <i class=\"fas fa-minus\"></i>" +
                         $"      </button>" +
                         $"    </div>" +
                         $"  </div>" +
                         $"  <div class=\"card-body gauge-parent\">" +
                         $"    <div class=\"text-center\">" +
                         $"        {SnmpManager.GetValue(target)} {valueSuffix}" +
                         $"    </div>" +
                         $"    <canvas id = \"gauge{id}\" style=\"min-height: 100%; height: 100%; max-height: 100%; max-width: 100%;\"></canvas>" +
                         $"  </div>" +

                         $"  <div class=\"card-footer text-center\">" +
                         $"    More info<i class=\"fas fa-arrow-circle-right ml-2\"></i>" +
                         $"  </div>" +
                         $"</div>";

            return new HtmlString(output);
        }

        private void SetValueSuffix()   // temp
        {
            var output = "°C";
            valueSuffix = output;
        }
        private void SetFasIcon()       // temp
        {
            var output = "thermometer";
            faIcon = output;
        }
    }
}