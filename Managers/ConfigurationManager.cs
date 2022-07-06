using System.Text.Json;
using AdminLTE_MVC.Models;

namespace AdminLTE_MVC.Managers
{
    public class ConfigurationManager
    {
        const string PATH = @"~/Data/Configuration/config.json";



        public static void SaveConfig(string data)
        {

            var jsonFile = JsonSerializer.Serialize("");

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
