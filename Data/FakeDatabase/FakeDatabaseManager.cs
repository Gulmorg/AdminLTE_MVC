namespace AdminLTE_MVC.Data.FakeDatabase
{
    public static class FakeDatabaseManager
    {
        const string path = @"~/Data/FakeDatabase/Json/FakeDatabase.json";

        public static void Write(string data)
        {


            File.WriteAllText(path, data);
        }

        public static string Read()
        {
            var text = File.ReadAllText(path);
            return string.Empty;
        }
    }
}
