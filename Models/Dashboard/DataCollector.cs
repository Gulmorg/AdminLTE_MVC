﻿using AdminLTE_MVC.Helpers.Generators;
using AdminLTE_MVC.Snmp;

namespace AdminLTE_MVC.Models.Dashboard
{
    public class DataCollector
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

        public int CardCount => CardGenerator.CardCount;

        #region Public Dictionary Access
        public List<string> DeviceIds => targetData["DeviceId"];
        public List<string> Names => targetData["Name"];
        public List<string> Values => targetData["Value"];
        public List<string> Mins => targetData["Min"];
        public List<string> Maxes => targetData["Max"];
        public List<string> LowAlarms => targetData["LowAlarm"];
        public List<string> LowWarnings => targetData["LowWarning"];
        public List<string> HighWarnings => targetData["HighWarning"];
        public List<string> HighAlarms => targetData["HighAlarm"];
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