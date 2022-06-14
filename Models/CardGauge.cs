namespace AdminLTE_MVC.Models
{
    public class CardGauge
    {
        #region !!!TEMPORARY CONST VALUES, THESE ARE TO BE SET BY THE USER ONCE USER PREFERENCES/COMPANY SETTINGS ARE IMPLEMENTED!!!
        const string NAME_OID = ".1.3.6.1.4.1.39052.5.2.1.5";
        const string VALUE_OID = ".1.3.6.1.4.1.39052.5.2.1.7";
        const string MIN_OID = ".1.3.6.1.4.1.39052.5.2.1.8";
        const string MAX_OID = ".1.3.6.1.4.1.39052.5.2.1.9";
        const string LOW_ALARM_OID = ".1.3.6.1.4.1.39052.5.2.1.10";
        const string LOW_WARNING_OID = ".1.3.6.1.4.1.39052.5.2.1.11";
        const string HIGH_WARNING_OID = ".1.3.6.1.4.1.39052.5.2.1.12";
        const string HIGH_ALARM_OID = ".1.3.6.1.4.1.39052.5.2.1.13";
        #endregion
        public CardGauge(Target target)
        {
            // Loop for each element in dictionary
            foreach (var dataPair in targetData)
            {
                var oid = SetOid(dataPair.Key);
                var dataList = Snmp.SnmpManager.WalkRequest(target.ChangeOid(oid));

                // Loop and add values to the list for each device
                foreach (var data in dataList)
                {
                    Console.WriteLine(data.Data.ToString());
                    targetData[dataPair.Key].Add(data.Data.ToString());
                }
            }
        }

        public string Title { get; set; } = string.Empty;

        #region Public Dictionary Access
        public List<string> Name => targetData["Name"];
        public List<string> Value => targetData["Value"];
        public List<string> Min => targetData["Min"];
        public List<string> Max => targetData["Max"];
        public List<string> LowAlarm => targetData["LowAlarm"];
        public List<string> LowWarning => targetData["LowWarning"];
        public List<string> HighWarning => targetData["HighWarning"];
        public List<string> HighAlarm => targetData["HighAlarm"];
        #endregion

        private readonly Dictionary<string, List<string>> targetData = new Dictionary<string, List<string>>()
        {
            { "Name", new List<string>() },
            { "Value", new List<string>() },
            { "Min", new List<string>() },
            { "Max", new List<string>() },
            { "LowAlarm", new List<string>() },
            { "LowWarning", new List<string>() },
            { "HighWarning", new List<string>() },
            { "HighAlarm", new List<string>() }
        };

        private static string SetOid(string dataKey) => dataKey switch
        {
            "Name" => NAME_OID,
            "Min" => MIN_OID,
            "Max" => MAX_OID,
            "LowAlarm" => LOW_ALARM_OID,
            "LowWarning" => LOW_WARNING_OID,
            "HighWarning" => HIGH_WARNING_OID,
            "HighAlarm" => HIGH_ALARM_OID,
            "Value" => VALUE_OID,
            _ => "",
        };
    }
}
