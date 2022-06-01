using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using AdminLTE_MVC.Extensions;
using System.Net;

namespace AdminLTE_MVC.Snmp
{
    internal class SnmpManager
    {
        public ISnmpData GetValue(Target target) => GetRequest(target).Data;

        private Variable GetRequest(Target target) => WalkRequest(target).GetDeviceById(target.DeviceId);

        private IList<Variable> WalkRequest(Target target)
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
    }
}