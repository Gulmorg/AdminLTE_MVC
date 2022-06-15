using AdminLTE_MVC.Snmp;

namespace AdminLTE_MVC.Models
{
    public class DataCollector  // bad name
    {
        public DataCollector(Target target)
        {
            // Loop for each element in dictionary
            foreach (var dataPair in targetData)
            {
                var oid = SnmpManager.SetOid(dataPair.Key); // Set the current OID via passing in the name of the agent
                var dataList = SnmpManager.WalkRequest(target.ChangeOid(oid));

                // Loop and add values to the list for each device
                foreach (var data in dataList)
                {
                    targetData[dataPair.Key].Add(data.Data.ToString());
                }
            }
        }

        #region Public Dictionary Access
        public List<string> GetDeviceIds() => targetData["DeviceId"];
        public List<string> GetNames() => targetData["Name"];
        public List<string> GetValues() => targetData["Value"];
        public List<string> GetMins() => targetData["Min"];
        public List<string> GetMaxes() => targetData["Max"];
        public List<string> GetLowAlarms() => targetData["LowAlarm"];
        public List<string> GetLowWarnings() => targetData["LowWarning"];
        public List<string> GetHighWarnings() => targetData["HighWarning"];
        public List<string> GetHighAlarms() => targetData["HighAlarm"];

        public int GetCardCount() => Card.CardCount;
        #endregion

        private readonly Dictionary<string, List<string>> targetData = new()
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
