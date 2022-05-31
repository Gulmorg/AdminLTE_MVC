using Lextm.SharpSnmpLib;

namespace AdminLTE_MVC.Snmp
{
    internal struct Target
    {
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
    }
}