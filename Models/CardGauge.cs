namespace AdminLTE_MVC.Models
{
    public class CardGauge
    {
        // !!!TEMPORARY CONST VALUES, THESE ARE TO BE SET BY THE USER ONCE USER PREFERENCES/COMPANY SETTINGS ARE IMPLEMENTED!!!
        const string NAME_OID = ".1.3.6.1.4.1.39052.5.2.1.5";
        const string VALUE_OID = ".1.3.6.1.4.1.39052.5.2.1.7";
        const string MIN_OID = ".1.3.6.1.4.1.39052.5.2.1.8";
        const string MAX_OID = ".1.3.6.1.4.1.39052.5.2.1.9";
        const string LOW_ALARM_OID = ".1.3.6.1.4.1.39052.5.2.1.10";
        const string LOW_WARNING_OID = ".1.3.6.1.4.1.39052.5.2.1.11";
        const string HIGH_WARNING_OID = ".1.3.6.1.4.1.39052.5.2.1.12";
        const string HIGH_ALARM_OID = ".1.3.6.1.4.1.39052.5.2.1.13";

        public string Title { get; set; } = string.Empty;
        public Dictionary<string, List<string>> TargetData { get; private set; } = new Dictionary<string, List<string>>()
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

        public CardGauge(Target target)
        {
            // Loop for each element in dictionary
            foreach (var dataPair in TargetData)
            {
                var oid = SetOid(dataPair.Key);
                var dataList = Snmp.SnmpManager.WalkRequest(target.ChangeOid(oid));

                // Loop and add values to the list for each device
                foreach (var data in dataList)
                {
                    Console.WriteLine(data.Data.ToString());
                    TargetData[dataPair.Key].Add(data.Data.ToString());
                }
            }
        }

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
