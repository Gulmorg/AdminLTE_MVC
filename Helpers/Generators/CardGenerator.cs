using AdminLTE_MVC.Helpers;
using AdminLTE_MVC.Models.Dashboard;
using AdminLTE_MVC.Snmp;
using Microsoft.AspNetCore.Html;

namespace AdminLTE_MVC.Helpers.Generators
{
    public class CardGenerator
    {
        public static byte CardCount { get; private set; }  // This should be in a dashboard model or database and manipulated by the CardGenerator.cs constructor

        public CardGenerator(Target target, string? title = null)
        {
            id = CardCount++;

            this.target = target;

            // Set title
            if (string.IsNullOrEmpty(title)) cardTitle = SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("Name"))).ToString();
            else cardTitle = title;

            var dataType = SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("Type"))).ToString();
            valueSuffix = GetValueSuffix(dataType);
            faIcon = GetFaIcon(dataType);
            headerColor = GetHeaderColor(dataType); // css class string (header background colour)
        }

        ~CardGenerator() => CardCount--;                                 // probably doesn't work on page reload
        public static void ResetCardCount() => CardCount = 0;   // therefore, ResetCardCount() is called on dashboard reloads
                                                                // this sometimes doesn't get called on page reloads with the modal submit, 
                                                                // likely won't be an issue since the page won't be reloaded 
                                                                // but rather the card will be added via ajax but just keep in mind

        private readonly Target target;
        private readonly byte id;
        private readonly string cardTitle;
        private readonly string headerColor;
        private readonly string valueSuffix;
        private readonly string faIcon;

        public IHtmlContent Generate() // TODO: fix value/breakpoints mismatch
        {
            var output = $"<!-- Card {id + 1} -->" +
                         $"<div class=\"card card-{headerColor}\" runat=\"server\">" +
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

        private static string GetValueSuffix(string type) => type switch
        {
            "temperature" => Data.FakeDatabase.Data.TEMPERATURE_SUFFIX,
            "humidity" => Data.FakeDatabase.Data.HUMIDITY_SUFFIX,
            "voltage" => Data.FakeDatabase.Data.VOLTAGE_SUFFIX,
            _ => "",
        };
        private static string GetFaIcon(string type) => type switch
        {
            "temperature" => Data.FakeDatabase.Data.TEMPERATURE_ICON,
            "humidity" => Data.FakeDatabase.Data.HUMIDITY_ICON,
            "voltage" => Data.FakeDatabase.Data.VOLTAGE_ICON,
            _ => "",
        };
        private static string GetHeaderColor(string type) => type switch
        {
            "temperature" => Data.FakeDatabase.Data.TEMPERATURE_HEADER,
            "humidity" => Data.FakeDatabase.Data.HUMIDITY_HEADER,
            "voltage" => Data.FakeDatabase.Data.VOLTAGE_HEADER,
            _ => "",
        };
    }
}