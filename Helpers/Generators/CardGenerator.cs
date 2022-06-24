using AdminLTE_MVC.Data.FakeDatabase;
using AdminLTE_MVC.Helpers;
using AdminLTE_MVC.Models.Dashboard;
using AdminLTE_MVC.Snmp;
using Microsoft.AspNetCore.Html;

namespace AdminLTE_MVC.Helpers.Generators
{
    public class CardGenerator
    {
        public CardGenerator(Target target, CardModel? model = null)
        {
            var devId = SnmpManager.GetValue(target.ChangeOid(SnmpManager.GetOid("DeviceId"))).ToString();
            Id = uint.Parse(devId + CardCount++);   // Appending CardCount at the end to generate unique IDs even if the user creates duplicate cards

            // Set value and breakpoints
            var value = float.Parse(SnmpManager.GetValue(target.ChangeOid(SnmpManager.GetOid("Value"))).ToString());
            //value = 150f; // For testing purposes. Values are saved in an integer where they are multiplied by 10 beforehand, hence 150f for testing with a value of 15f
            var lowAlarm = float.Parse(SnmpManager.GetValue(target.ChangeOid(SnmpManager.GetOid("LowAlarm"))).ToString());
            var lowWarning = float.Parse(SnmpManager.GetValue(target.ChangeOid(SnmpManager.GetOid("LowWarning"))).ToString());
            var highWarning = float.Parse(SnmpManager.GetValue(target.ChangeOid(SnmpManager.GetOid("HighWarning"))).ToString());
            var highAlarm = float.Parse(SnmpManager.GetValue(target.ChangeOid(SnmpManager.GetOid("HighAlarm"))).ToString());

            var nullCheck = model == null;

            
            if (!nullCheck)
            {
                // Set title 
                _cardTitle = string.IsNullOrEmpty(model.Title) ?
                    SnmpManager.GetValue(target.ChangeOid(SnmpManager.GetOid("Name"))).ToString() :
                    model.Title;

                // Set element
                _valuePrefixTitle = string.IsNullOrEmpty(model.Element) ?
                                    _valuePrefixTitle = SnmpManager.GetValue(target.ChangeOid(SnmpManager.GetOid("Name"))).ToString() :
                                    model.Element;
            }
            else
            {
                throw new NullReferenceException("'CardModel model' at 'CardGenerator.cs' is null!");
            }

            // Set Dynamic HTML values
            var dataType = SnmpManager.GetValue(target.ChangeOid(SnmpManager.GetOid("Type"))).ToString();
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

        public static byte CardCount { get; private set; }

        public static void ResetCardCount() => CardCount = 0;   // this doesn't get called on page reloads with the modal submit, 
                                                                // likely won't be an issue since the page won't be reloaded 
                                                                // but rather the card will be added via ajax but just keep in mind
        
        public uint Id { get; private set; }

        private readonly string _valuePrefixTitle;
        private readonly string _cardTitle;
        private readonly string _headerColor;
        private readonly string _valueSuffix;
        private readonly string _faIcon;
        private readonly string _valueBackground;

        public string GenerateCard()
        {
            var output =    $"  <!-- Card {Id} -->" +
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
                            $"          <span>{_valuePrefixTitle}: </span><span id=\"gauge-text-{Id}\">Value not found!</span><span> {_valueSuffix}</span>" +
                            $"      </div>" +
                            $"    </div>" +
                            $"    <a href = \"#\" class=\"card-footer card-link text-center\">" +
                            $"      More info<i class=\"fas fa-arrow-circle-right ml-2\"></i>" +
                            $"    </a>" +
                            $"  </div>";
            
            return output;
        }

        private static string GetValueSuffix(string type) => type switch
        {
            "temperature" => FakeData.TEMPERATURE_SUFFIX,
            "humidity" => FakeData.HUMIDITY_SUFFIX,
            "voltage" => FakeData.VOLTAGE_SUFFIX,
            _ => "",
        };
        private static string GetFaIcon(string type) => type switch
        {
            "temperature" => FakeData.TEMPERATURE_ICON,
            "humidity" => FakeData.HUMIDITY_ICON,
            "voltage" => FakeData.VOLTAGE_ICON,
            _ => "",
        };
        private static string GetHeaderColor(string type) => type switch
        {
            "temperature" => FakeData.TEMPERATURE_HEADER,
            "humidity" => FakeData.HUMIDITY_HEADER,
            "voltage" => FakeData.VOLTAGE_HEADER,
            _ => "",
        };
        private static string GetValueBackground(float value, float lowAlarm, float lowWarn, float highWarn, float highAlarm)
        {
            if (value < lowAlarm || value > highAlarm) return FakeData.VALUE_BACKGROUD_ALARM;
            if (value < lowWarn || value > highWarn) return FakeData.VALUE_BACKGROUND_WARNING;
            return FakeData.VALUE_BACKGROUND_NORMAL;
        }
    }
}