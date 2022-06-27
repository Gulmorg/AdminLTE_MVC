using AdminLTE_MVC.Helpers.Generators;
using AdminLTE_MVC.Snmp;

namespace AdminLTE_MVC.Models.Dashboard
{
    public class DataCollector
    {
        public DataCollector(List<Target> targets)
        {
            foreach (var target in targets)
            {
                // Loop for each element in dictionary
                foreach (var dataPair in _targetData)
                {
                    var oid = GetOid(dataPair.Key); // Set the current OID via passing in the name of the agent
                    var data = SnmpManager.GetFirstValue(target.ChangeOid(oid));
                    _targetData[dataPair.Key].Add(data);
                }
            }
        }
        public int CardCount => CardGenerator.CardCount;

        #region Dictionary Access from JavaScript
        public List<string> DeviceIds => _targetData["DeviceId"]; 
        public List<string> Types => _targetData["Type"];
        public List<string> Names => _targetData["Name"];
        public List<string> Values => _targetData["Value"];
        public List<string> Mins => _targetData["Min"];
        public List<string> Maxes => _targetData["Max"];
        public List<string> LowAlarms => _targetData["LowAlarm"];
        public List<string> LowWarnings => _targetData["LowWarning"];
        public List<string> HighWarnings => _targetData["HighWarning"];
        public List<string> HighAlarms => _targetData["HighAlarm"];
        #endregion

        // Sets the OID according to the name of the agent passed in
        private static string GetOid(string dataKey) => dataKey switch {
            "DeviceId" => Data.FakeDatabase.FakeData.DEVID_OID,
            "Type" => Data.FakeDatabase.FakeData.TYPE_OID,
            "Name" => Data.FakeDatabase.FakeData.NAME_OID,
            "Min" => Data.FakeDatabase.FakeData.MIN_OID,
            "Max" => Data.FakeDatabase.FakeData.MAX_OID,
            "LowAlarm" => Data.FakeDatabase.FakeData.LOW_ALARM_OID,
            "LowWarning" => Data.FakeDatabase.FakeData.LOW_WARNING_OID,
            "HighWarning" => Data.FakeDatabase.FakeData.HIGH_WARNING_OID,
            "HighAlarm" => Data.FakeDatabase.FakeData.HIGH_ALARM_OID,
            "Value" => Data.FakeDatabase.FakeData.VALUE_OID,
            _ => "",
        };

        private readonly Dictionary<string, List<string>> _targetData = new()
        {
            { "DeviceId", new List<string>() },
            { "Type", new List<string>() },
            { "Name", new List<string>() },
            { "Value", new List<string>() },
            { "Min", new List<string>() },
            { "Max", new List<string>() },
            { "LowAlarm", new List<string>() },
            { "LowWarning", new List<string>() },
            { "HighWarning", new List<string>() },
            { "HighAlarm", new List<string>() },
        };
    }
}
