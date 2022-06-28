namespace AdminLTE_MVC.Models.ConfigurationModels
{
    public class DynamicHtmlSettings
    {
        public string? TemperatureSuffix { get; set; }
        public string? HumiditySuffix { get; set; }
        public string? VoltageSuffix { get; set; }

        public string? TemperatureIcon { get; set; }
        public string? HumidityIcon { get; set; }
        public string? VoltageIcon { get; set; }

        public string? TemperatureHeader { get; set; }
        public string? HumidityHeader { get; set; }
        public string? VoltageHeader { get; set; }

        public string? ValueBgAlarmColor { get; set; }
        public string? ValueBgWarningColor { get; set; }
        public string? ValueBgNormalColor { get; set; }
    }
}
