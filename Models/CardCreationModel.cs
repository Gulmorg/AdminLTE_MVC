namespace AdminLTE_MVC.Models
{
    public class CardCreationModel
    {
        public int Title { get; set; }
        public List<string> ElementList { get; set; } = new List<string>(); 

        public CardCreationModel(Target target)
        {
            var variableList = Snmp.SnmpManager.WalkRequest(target);
            foreach (var item in variableList)
            {
                ElementList.Add(item.Data.ToString());
            }
        }
    }
}
