using Lextm.SharpSnmpLib;

namespace AdminLTE_MVC.Snmp
{
    internal struct Target
    {
        #region Contructors
        public Target(string ip, string community, string oid)
        {
            Ip = ip;
            CommunityName = community;
            Oid = oid;
        }

        // Ones
        public Target(string ip, ushort port, string community, string oid) : this(ip: ip, community: community, oid: oid) => Port = port;
        public Target(string ip, string community, string oid, string devId) : this(ip: ip, community: community, oid: oid) => DeviceId = devId;
        public Target(string ip, string community, string oid, VersionCode version) : this(ip: ip, community: community, oid: oid) => VersionCode = version;

        // Twos
        public Target(string ip, ushort port, string community, string oid, VersionCode version) : this(ip: ip, port: port, community: community, oid: oid) => VersionCode = version;
        public Target(string ip, ushort port, string community, string oid, string devId) : this(ip: ip, port: port, community: community, oid: oid) => DeviceId = devId;
        public Target(string ip, string community, string oid, string devId, VersionCode version) : this(ip: ip, community: community, oid: oid, devId: devId) => VersionCode = version;

        // Three
        public Target(string ip, ushort port, string community, string oid, string devId, VersionCode version) : this(ip: ip, port: port, community: community, oid: oid, devId: devId) => VersionCode = version;
        #endregion

        public string Ip { get; }
        public ushort Port { get; } = 161;
        public string CommunityName { get; }
        public string Oid { get; }
        public string DeviceId { get; } = string.Empty;
        public VersionCode VersionCode { get; } = VersionCode.V1;

        public Target ChangeOid(string newId) => new Target(ip: Ip, port: Port, community: CommunityName, oid: newId, version: VersionCode);
        public Target ChangeDeviceId(string newDeviceId) => new Target(ip: Ip, port: Port, community: CommunityName, oid: Oid, devId: newDeviceId, version: VersionCode);
    }
}