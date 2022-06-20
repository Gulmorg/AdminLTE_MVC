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
            Console.WriteLine("test");
            var devId = SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("DeviceId"))).ToString();
            Id = uint.Parse(devId + CardCount++);   // Appending CardCount at the end to generate unique IDs even if the user creates duplicate cards

            // Set value and breakpoints
            var value = float.Parse(SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("Value"))).ToString());
            //value = 150f; // For testing purposes. Values are saved in an integer where they are multiplied by 10 beforehand, hence 150f for testing with a value of 15f
            var lowAlarm = float.Parse(SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("LowAlarm"))).ToString());
            var lowWarning = float.Parse(SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("LowWarning"))).ToString());
            var highWarning = float.Parse(SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("HighWarning"))).ToString());
            var highAlarm = float.Parse(SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("HighAlarm"))).ToString());

            // Set title
            _defaultTitle = SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("Name"))).ToString();
            _cardTitle = string.IsNullOrEmpty(title) ? _defaultTitle : title;

            var dataType = SnmpManager.GetValue(target.ChangeOid(SnmpManager.SetOid("Type"))).ToString();
            // what to append after the value ("°C", "%" etc.)
            _valueSuffix = GetValueSuffix(dataType);
            // Icon to display in the header
            _faIcon = GetFaIcon(dataType);
            // CSS class string (header background color)
            _headerColor = GetHeaderColor(dataType);
            // CSS class string (value background color)
            _valueBackground = GetValueBackground(value, lowAlarm, lowWarning, highWarning, highAlarm);
        }

        ~CardGenerator() => CardCount--;                        // probably doesn't work on page reloadtherefore, ResetCardCount() is called on dashboard reloads.
        public static void ResetCardCount() => CardCount = 0;   // this doesn't get called on page reloads with the modal submit, 
                                                                // likely won't be an issue since the page won't be reloaded 
                                                                // but rather the card will be added via ajax but just keep in mind
        
        public uint Id { get; private set; }

        private readonly string _defaultTitle;
        private readonly string _cardTitle;
        private readonly string _headerColor;
        private readonly string _valueSuffix;
        private readonly string _faIcon;
        private readonly string _valueBackground;

        public IHtmlContent Generate()
        {
            var output =    $"<!-- Card {Id} -->" +
                            $"<section class=\"col-lg-3 connectedSortable ui-sortable\"> "+
                            $"  <div class=\"card card-{_headerColor}\" runat=\"server\">" +
                            $"    <div class=\"card-header\">" +
                            $"      <h3 class=\"card-title\"><i class=\"fas fa-{_faIcon} mr-2\"></i>{_cardTitle}</h3>" +
                            $"      <div class=\"card-tools\">" +
                            $"        <button type=\"button\" class=\"btn btn-tool\" data-card-widget=\"collapse\">" +
                            $"          <i class=\"fas fa-minus\"></i>" +
                            $"        </button>" +
                            $"      </div>" +
                            $"    </div>" +
                            $"    <div class=\"card-body gauge-parent\" style=\"display: flex; !important\">" +
                            $"      <canvas id = \"gauge-{Id}\" style=\"min-height: 100%; height: 100%; max-height: 100%; max-width: 100%;\"></canvas>" +
                            $"      <div class=\"badge bg-{_valueBackground} gauge-text\">" +
                            $"          <span>{_defaultTitle}: </span><span id=\"gauge-text-{Id}\">Value not found!</span><span> {_valueSuffix}</span>" +
                            $"      </div>" +
                            $"    </div>" +
                            $"    <a href = \"#\" class=\"card-footer card-link text-center\">" +
                            $"      More info<i class=\"fas fa-arrow-circle-right ml-2\"></i>" +
                            $"    </a>" +
                            $"  </div>" +
                            $"</section>";

            return new HtmlString(output);
        }

        private static string GetValueSuffix(string type) => type switch
        {
            "temperature" => Data.FakeDatabase.FakeData.TEMPERATURE_SUFFIX,
            "humidity" => Data.FakeDatabase.FakeData.HUMIDITY_SUFFIX,
            "voltage" => Data.FakeDatabase.FakeData.VOLTAGE_SUFFIX,
            _ => "",
        };
        private static string GetFaIcon(string type) => type switch
        {
            "temperature" => Data.FakeDatabase.FakeData.TEMPERATURE_ICON,
            "humidity" => Data.FakeDatabase.FakeData.HUMIDITY_ICON,
            "voltage" => Data.FakeDatabase.FakeData.VOLTAGE_ICON,
            _ => "",
        };
        private static string GetHeaderColor(string type) => type switch
        {
            "temperature" => Data.FakeDatabase.FakeData.TEMPERATURE_HEADER,
            "humidity" => Data.FakeDatabase.FakeData.HUMIDITY_HEADER,
            "voltage" => Data.FakeDatabase.FakeData.VOLTAGE_HEADER,
            _ => "",
        };
        private static string GetValueBackground(float value, float lowAlarm, float lowWarn, float highWarn, float highAlarm)
        {
            if (value < lowAlarm || value > highAlarm) return Data.FakeDatabase.FakeData.VALUE_BACKGROUD_ALARM;
            if (value < lowWarn || value > highWarn) return Data.FakeDatabase.FakeData.VALUE_BACKGROUND_WARNING;
            return Data.FakeDatabase.FakeData.VALUE_BACKGROUND_NORMAL;
        }
    }
}