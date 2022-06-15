using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using AdminLTE_MVC.Helpers;
using System.Net;
using AdminLTE_MVC.Models;

namespace AdminLTE_MVC.Snmp
{
    internal static class SnmpManager
    {
        public static ISnmpData GetValue(Target target) => GetRequest(target).Data;

        private static Variable GetRequest(Target target) => WalkRequest(target).GetDeviceById(target.DeviceId);

        public static IList<Variable> WalkRequest(Target target)
        {
            var result = new List<Variable>();
            Messenger.Walk(version: target.VersionCode,
                           endpoint: new IPEndPoint(IPAddress.Parse(target.Ip), target.Port),
                           community: new OctetString(target.CommunityName),
                           table: new ObjectIdentifier(target.Oid),
                           list: result,
                           timeout: 60000,
                           mode: WalkMode.WithinSubtree);
            return result;
        }

        // Sets the OID according to the name of the agent passed in    TODO: move to snmp manager
        public static string SetOid(string dataKey) => dataKey switch
        {
            "DeviceId"      => Data.FakeDatabase.Data.DEVID_OID,
            "Type"          => Data.FakeDatabase.Data.TYPE_OID,
            "Name"          => Data.FakeDatabase.Data.NAME_OID,
            "Min"           => Data.FakeDatabase.Data.MIN_OID,
            "Max"           => Data.FakeDatabase.Data.MAX_OID,
            "LowAlarm"      => Data.FakeDatabase.Data.LOW_ALARM_OID,
            "LowWarning"    => Data.FakeDatabase.Data.LOW_WARNING_OID,
            "HighWarning"   => Data.FakeDatabase.Data.HIGH_WARNING_OID,
            "HighAlarm"     => Data.FakeDatabase.Data.HIGH_ALARM_OID,
            "Value"         => Data.FakeDatabase.Data.VALUE_OID,
            _ => "",
        };
    }
}