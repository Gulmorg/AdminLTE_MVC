using Lextm.SharpSnmpLib;

namespace AdminLTE_MVC.Models.Dashboard
{
    // TODO: split into 2 parts; one part would have ip/community/security info, other would have oid/devId info
    public struct Target
    {
        #region Contructors
        public Target(string ip, string community, string oid)
        {
            Ip = ip;
            CommunityName = community;
            Oid = oid;
        }

        // One optional parameters
        public Target(string ip, ushort port, string community, string oid) : this(ip: ip, community: community, oid: oid) => Port = port;
        public Target(string ip, string community, string oid, string devId) : this(ip: ip, community: community, oid: oid) => DeviceId = devId;
        public Target(string ip, string community, string oid, VersionCode version) : this(ip: ip, community: community, oid: oid) => VersionCode = version;

        // Two optional parameters
        public Target(string ip, ushort port, string community, string oid, VersionCode version) : this(ip: ip, port: port, community: community, oid: oid) => VersionCode = version;
        public Target(string ip, ushort port, string community, string oid, string devId) : this(ip: ip, port: port, community: community, oid: oid) => DeviceId = devId;
        public Target(string ip, string community, string oid, string devId, VersionCode version) : this(ip: ip, community: community, oid: oid, devId: devId) => VersionCode = version;

        // Three optional parameters
        public Target(string ip, ushort port, string community, string oid, string devId, VersionCode version) : this(ip: ip, port: port, community: community, oid: oid, devId: devId) => VersionCode = version;
        #endregion

        public readonly string Ip { get; }
        public readonly ushort Port { get; } = 161;
        public readonly string CommunityName { get; }
        public readonly string Oid { get; }
        public readonly string DeviceId { get; } = string.Empty;
        public readonly VersionCode VersionCode { get; } = VersionCode.V1;

        public Target ChangeOid(string newId) => new(ip: Ip, port: Port, community: CommunityName, oid: newId, devId: DeviceId, version: VersionCode);
        public Target ChangeDeviceId(string newDeviceId) => new(ip: Ip, port: Port, community: CommunityName, oid: Oid, devId: newDeviceId, version: VersionCode);
    }
}