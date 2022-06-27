using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using AdminLTE_MVC.Helpers;
using System.Net;
using AdminLTE_MVC.Models.Dashboard;

namespace AdminLTE_MVC.Snmp
{
    internal static class SnmpManager
    {
        public static string GetFirstValue(Target target) => GetFirstRequest(target).Data.ToString();

        private static Variable GetFirstRequest(Target target) => WalkRequest(target).GetDeviceById(target.DeviceId);

        public static IList<string> WalkValue(Target target) => new List<string>(from variable in WalkRequest(target) select variable.Data.ToString());

        private static IList<Variable> WalkRequest(Target target)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Walking target: " + target.Oid);

            var variableList = new List<Variable>();

            Messenger.Walk(version: target.VersionCode,
                           endpoint: new IPEndPoint(IPAddress.Parse(target.Ip), target.Port),
                           community: new OctetString(target.CommunityName),
                           table: new ObjectIdentifier(target.Oid),
                           list: variableList,
                           timeout: 60000,
                           mode: WalkMode.WithinSubtree);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Complete");
            Console.ResetColor();

            return variableList;
        }
    }
}