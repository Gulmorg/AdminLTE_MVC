using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using AdminLTE_MVC.Helpers;
using System.Net;
using AdminLTE_MVC.Models;

namespace AdminLTE_MVC.Snmp
{
    internal static class SnmpManager
    {
        #region !!!TEMPORARY CONST VALUES, THESE ARE TO BE SET BY THE USER ONCE USER PREFERENCES/COMPANY SETTINGS ARE IMPLEMENTED!!!
        const string DEVID_OID = ".1.3.6.1.4.1.39052.5.2.1.1";
        const string TYPE_OID = ".1.3.6.1.4.1.39052.5.2.1.4";
        const string NAME_OID = ".1.3.6.1.4.1.39052.5.2.1.5";
        const string VALUE_OID = ".1.3.6.1.4.1.39052.5.2.1.7";
        const string MIN_OID = ".1.3.6.1.4.1.39052.5.2.1.8";
        const string MAX_OID = ".1.3.6.1.4.1.39052.5.2.1.9";
        const string LOW_ALARM_OID = ".1.3.6.1.4.1.39052.5.2.1.10";
        const string LOW_WARNING_OID = ".1.3.6.1.4.1.39052.5.2.1.11";
        const string HIGH_WARNING_OID = ".1.3.6.1.4.1.39052.5.2.1.12";
        const string HIGH_ALARM_OID = ".1.3.6.1.4.1.39052.5.2.1.13";
        #endregion

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
            "DeviceId" => DEVID_OID,
            "Type" => TYPE_OID,
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