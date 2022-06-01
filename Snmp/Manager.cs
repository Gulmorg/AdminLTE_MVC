using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using System.Net;

namespace AdminLTE_MVC.Snmp
{
    internal class SnmpManager
    {
        public IList<Variable> GetRequest(Target target)
        {
            return Messenger.Get(version: target.VersionCode,
                                 endpoint: new IPEndPoint(IPAddress.Parse(target.Ip), 161),
                                 community: new OctetString(target.CommunityName),
                                 variables: new List<Variable> { new Variable(new ObjectIdentifier(target.Oid)) },
                                 timeout: 60000);
        }

        public IList<Variable> WalkRequest(Target target)
        {
            var result = new List<Variable>();
            Messenger.Walk(version: target.VersionCode,
                           endpoint: new IPEndPoint(IPAddress.Parse(target.Ip), 161),
                           community: new OctetString(target.CommunityName),
                           table: new ObjectIdentifier(target.Oid),
                           list: result,
                           timeout: 60000,
                           mode: WalkMode.WithinSubtree);
            return result;
        }

        public IList<Variable> GetNextRequest(Target target)
        {
            GetNextRequestMessage message = new GetNextRequestMessage(0,
                                                          VersionCode.V1,
                                                          new OctetString(target.CommunityName),
                                                          new List<Variable> { new Variable(new ObjectIdentifier(target.Oid)) });

            ISnmpMessage response = message.GetResponse(60000, new IPEndPoint(IPAddress.Parse(target.Ip), 161));
            if (response.Pdu().ErrorStatus.ToInt32() != 0)
            {
                throw ErrorException.Create(
                    "error in response",
                    IPAddress.Parse(target.Ip),
                    response);
            }

            var result = response.Pdu().Variables;

            return result;
        }
    }
}