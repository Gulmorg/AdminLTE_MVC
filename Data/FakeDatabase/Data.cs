namespace AdminLTE_MVC.Data.FakeDatabase
{
    public static class FakeData
    {
        #region Credentials
        public static string Username { get; } = "test@gmail.com";
        public static string Password { get; } = "123123";
        #endregion

        #region Server Settings
        public const string IP = "192.168.2.11";
        public const ushort PORT = 161;
        public const string COMMUNITY_NAME = "public";

        #endregion

        #region OIDs
        public const string DEVID_OID           = ".1.3.6.1.4.1.39052.5.2.1.1";
        public const string TYPE_OID            = ".1.3.6.1.4.1.39052.5.2.1.4";
        public const string NAME_OID            = ".1.3.6.1.4.1.39052.5.2.1.5";
        public const string VALUE_OID           = ".1.3.6.1.4.1.39052.5.2.1.7";
        public const string MIN_OID             = ".1.3.6.1.4.1.39052.5.2.1.8";
        public const string MAX_OID             = ".1.3.6.1.4.1.39052.5.2.1.9";
        public const string LOW_ALARM_OID       = ".1.3.6.1.4.1.39052.5.2.1.10";
        public const string LOW_WARNING_OID     = ".1.3.6.1.4.1.39052.5.2.1.11";
        public const string HIGH_WARNING_OID    = ".1.3.6.1.4.1.39052.5.2.1.12";
        public const string HIGH_ALARM_OID      = ".1.3.6.1.4.1.39052.5.2.1.13";
        #endregion

        #region Dynamic HTML
        public const string TEMPERATURE_SUFFIX = "°C";
        public const string HUMIDITY_SUFFIX = "%";
        public const string VOLTAGE_SUFFIX = "V";

        public const string TEMPERATURE_ICON = "thermometer-full";
        public const string HUMIDITY_ICON = "droplet";
        public const string VOLTAGE_ICON = "bolt";

        public const string TEMPERATURE_HEADER = "danger";
        public const string HUMIDITY_HEADER = "primary";
        public const string VOLTAGE_HEADER = "secondary";

        public const string VALUE_BACKGROUD_ALARM = "danger";
        public const string VALUE_BACKGROUND_WARNING = "warning";
        public const string VALUE_BACKGROUND_NORMAL = "success";
        #endregion
    }
}
