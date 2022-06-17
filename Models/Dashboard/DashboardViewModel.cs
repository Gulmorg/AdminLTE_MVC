namespace AdminLTE_MVC.Models.Dashboard
{
    public class DashboardViewModel
    {
        public DataCollector? DataCollector { get; set; }
        public IEnumerable<Target>? Target { get; set; }
        public CardModel? CardModel { get; set; }
    }
}
