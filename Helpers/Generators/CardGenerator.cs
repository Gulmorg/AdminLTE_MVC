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
            var devId = SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("DeviceId"))).ToString();
            Id = uint.Parse(devId + CardCount++);   // Appending CardCount at the end to generate unique IDs even if the user creates duplicate cards
            _target = target;

            // Set title
            if (string.IsNullOrEmpty(title))
            {
                string nameOid = SnmpManager.SetOid("Name");
                _cardTitle = SnmpManager.GetValue(target.ChangeOid(nameOid)).ToString();
            }
            else 
                _cardTitle = title;

            var dataType = SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("Type"))).ToString();

            // what to append after the value ("°C", "%" etc.)
            _valueSuffix = GetValueSuffix(dataType);
            // Icon to display in the header
            _faIcon = GetFaIcon(dataType);
            // CSS class string (header background colour)
            _headerColor = GetHeaderColor(dataType);
        }

        ~CardGenerator() => CardCount--;                        // probably doesn't work on page reloadtherefore, ResetCardCount() is called on dashboard reloads.
        public static void ResetCardCount() => CardCount = 0;   // this doesn't get called on page reloads with the modal submit, 
                                                                // likely won't be an issue since the page won't be reloaded 
                                                                // but rather the card will be added via ajax but just keep in mind
        
        public uint Id { get; private set; }

        private readonly Target _target;
        private readonly string _cardTitle;
        private readonly string _headerColor;
        private readonly string _valueSuffix;
        private readonly string _faIcon;

        public IHtmlContent Generate()
        {
            var output = $"<!-- Card {Id} -->" +
                         $"<div class=\"card card-{_headerColor}\" runat=\"server\">" +
                         $"  <div class=\"card-header\">" +
                         $"    <h3 class=\"card-title\"><i class=\"fas fa-{_faIcon} mr-2\"></i>{_cardTitle}</h3>" +
                         $"    <div class=\"card-tools\">" +
                         $"      <button type = \"button\" class=\"btn btn-tool\" data-card-widget=\"collapse\">" +
                         $"        <i class=\"fas fa-minus\"></i>" +
                         $"      </button>" +
                         $"    </div>" +
                         $"  </div>" +
                         $"  <div class=\"card-body gauge-parent\">" +
                         $"    <canvas id = \"gauge{Id}\" style=\"min-height: 100%; height: 100%; max-height: 100%; max-width: 100%;\"></canvas>" +
                         $"    <div class=\"text-center\">" +
                         $"        {SnmpManager.GetValue(_target)} {_valueSuffix}" +
                         $"    </div>" +
                         $"  </div>" +
                         $"  <a href = \"#\" class=\"card-footer card-link text-center\">" +
                         $"    More info<i class=\"fas fa-arrow-circle-right ml-2\"></i>" +
                         $"  </a>" +
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