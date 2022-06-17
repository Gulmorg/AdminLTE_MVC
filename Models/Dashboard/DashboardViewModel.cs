namespace AdminLTE_MVC.Models.Dashboard
{
    public class DashboardViewModel
    {
        public DataCollector? DataCollector { get; set; }
        public List<Target> Target { get; set; } = new List<Target>();
        public CardModel? CardModel { get; set; }   // Should probably be a List<CardModel> since we'll be generating multiple cards per dashboard page
    }
}
