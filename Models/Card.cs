using AdminLTE_MVC.Helpers;
using AdminLTE_MVC.Snmp;
using Microsoft.AspNetCore.Html;

namespace AdminLTE_MVC.Models
{
    public class Card
    {
        private static byte CardCount;

        public Card(string type, string title)
        {
            Id = CardCount++;
            Position = Id;

            // Type of the card (header background colour)
            CardType = type;

            CardTitle = title;
        }

        ~Card() => CardCount--;

        public byte Id { get; }
        public byte Position { get; set; }
        public string CardType { get; set; }
        public string CardTitle { get; set; }

        public IHtmlContent Generate(Target target)    // temp
        {
            var output = $"<!-- Card 1 -->" +
                         $"<div class=\"card card-{CardType}\" runat=\"server\">" +
                         $"  <div class=\"card-header\">" +
                         $"    <h3 class=\"card-title\"><i class=\"fas fa-thermometer\"></i>  {CardTitle}</h3>" +
                         $"    <div class=\"card-tools\">" +
                         $"      <button type = \"button\" class=\"btn btn-tool\" data-card-widget=\"collapse\">" +
                         $"        <i class=\"fas fa-minus\"></i>" +
                         $"      </button>" +
                         $"    </div>" +
                         $"  </div>" +
                         $"  <div class=\"card-body gauge-parent\">" +
                         $"    <div class=\"text-center\">" +
                         $"        {SnmpManager.GetValue(target)} °C" +
                         $"    </div>" +
                         $"    <canvas id = \"gaugeThree\" style=\"min-height: 100%; height: 100%; max-height: 100%; max-width: 100%;\"></canvas>" +
                         $"  </div>" +

                         $"  <div class=\"card-footer text-center\">" +
                         $"    More info<i class=\"fas fa-arrow-circle-right\"></i>" +
                         $"  </div>" +
                         $"</div>";

            return new HtmlString(output);
        }
    }
}