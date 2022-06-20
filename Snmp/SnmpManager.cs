using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using AdminLTE_MVC.Helpers;
using System.Net;
using AdminLTE_MVC.Models.Dashboard;

namespace AdminLTE_MVC.Snmp
{
    internal static class SnmpManager
    {
        public static string GetValue(Target target) => GetRequest(target).Data.ToString();

        private static Variable GetRequest(Target target) => WalkRequest(target).GetDeviceById(target.DeviceId);

        public static IList<string> WalkValue(Target target)
        {
            List<Variable> variableList = new();
            Messenger.Walk(version: target.VersionCode,
                           endpoint: new IPEndPoint(IPAddress.Parse(target.Ip), target.Port),
                           community: new OctetString(target.CommunityName),
                           table: new ObjectIdentifier(target.Oid),
                           list: variableList,
                           timeout: 60000,
                           mode: WalkMode.WithinSubtree);

            List<string> stringList = new();
            foreach (var variable in variableList)
            {
                stringList.Add(variable.Data.ToString());
            }

            return stringList;
        }

        private static IList<Variable> WalkRequest(Target target)
        {
            var variableList = new List<Variable>();
            Messenger.Walk(version: target.VersionCode,
                           endpoint: new IPEndPoint(IPAddress.Parse(target.Ip), target.Port),
                           community: new OctetString(target.CommunityName),
                           table: new ObjectIdentifier(target.Oid),
                           list: variableList,
                           timeout: 60000,
                           mode: WalkMode.WithinSubtree);
            return variableList;
        }

        // Sets the OID according to the name of the agent passed in
        public static string GetOid(string dataKey) => dataKey switch
        {
            "DeviceId"      => Data.FakeDatabase.FakeData.DEVID_OID,
            "Type"          => Data.FakeDatabase.FakeData.TYPE_OID,
            "Name"          => Data.FakeDatabase.FakeData.NAME_OID,
            "Min"           => Data.FakeDatabase.FakeData.MIN_OID,
            "Max"           => Data.FakeDatabase.FakeData.MAX_OID,
            "LowAlarm"      => Data.FakeDatabase.FakeData.LOW_ALARM_OID,
            "LowWarning"    => Data.FakeDatabase.FakeData.LOW_WARNING_OID,
            "HighWarning"   => Data.FakeDatabase.FakeData.HIGH_WARNING_OID,
            "HighAlarm"     => Data.FakeDatabase.FakeData.HIGH_ALARM_OID,
            "Value"         => Data.FakeDatabase.FakeData.VALUE_OID,
            _ => "",
        };
    }
}