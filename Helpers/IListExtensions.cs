using Lextm.SharpSnmpLib;

namespace AdminLTE_MVC.Helpers
{
    public static class IListExtensions
    {
        public static Variable GetDeviceById(this IList<Variable> list, string id)
        {
            foreach (var item in list)
            {
                if (item.Id.ToString().EndsWith(id)) return item;
            }

            return list.First();
        }
    }
}
