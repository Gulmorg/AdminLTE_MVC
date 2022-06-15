namespace AdminLTE_MVC.Models
{
    public class DashboardViewModel
    {
        public DataCollector? DataCollector { get; set; }
        public IEnumerable<Target>? Target { get; set; }
    }
}
