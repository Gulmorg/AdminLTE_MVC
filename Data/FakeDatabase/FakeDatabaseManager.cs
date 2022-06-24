using System.Text.Json;

namespace AdminLTE_MVC.Data.FakeDatabase
{
    public static class FakeDatabaseManager
    {
        const string PATH = @"~/Data/FakeDatabase/Json/FakeDatabase.json";

        public static void SaveConfig(string data)
        {

            var jsonFile = JsonSerializer.Serialize(new FakeData());

            Console.WriteLine(jsonFile);
            //File.WriteAllText(PATH, jsonFile);
        }

        public static string GetConfig()
        {
            var text = File.ReadAllText(PATH);
            return text;
        }
    }
}
