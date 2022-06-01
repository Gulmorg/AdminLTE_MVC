using Lextm.SharpSnmpLib;

namespace AdminLTE_MVC.Snmp
{
    internal struct Target
    {
        // Vutlan ctl units ".1.3.6.1.4.1.39052.1.3.1"

        public Target(string ip, string community, string oid) : this(ip, community, oid, VersionCode.V1) { }
        public Target(string ip, string community, string oid, VersionCode version)
        {
            Ip = ip;
            CommunityName = community;
            Oid = oid;
            VersionCode = version;
        }

        public string Ip { get; }
        public string CommunityName { get; }
        public string Oid { get; }
        public VersionCode VersionCode { get; }

        public Target SwitchOid(string oid) => new Target(Ip, CommunityName, oid, VersionCode); // TODO (test): Make sure GC gets rid of the old instance
    }
}