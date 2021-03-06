using AdminLTE_MVC.Models;
using AdminLTE_MVC.Models.Dashboard;
using AdminLTE_MVC.Data.FakeDatabase;
using Lextm.SharpSnmpLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;
using AdminLTE_MVC.Managers;

namespace AdminLTE_MVC.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()    // TODO: Snmp walk instead of creating a list
        {
            var targetTemplate = new Target(ip: "192.168.2.11", port: 161, community: "public", oid: "1.3.6.1.4.1.39052.5.2.1.7", devId: "201003");

            var devIds = SnmpManager.WalkValue(targetTemplate.ChangeOid(FakeData.DEVID_OID));
            var targetList = new List<Target>();

            foreach (var devId in devIds)
            {
                targetList.Add(targetTemplate.ChangeDeviceId(devId));
            }

            return View(targetList);
        }

        public IActionResult Dashboard()
        {
            // temp
            var targetTemplate = new Target(ip: "192.168.2.11", port: 161, community: "public", oid: "1.3.6.1.4.1.39052.5.2.1.7", devId: "201003");
            var targetList = new List<Target>()
            {
                targetTemplate.ChangeDeviceId("201001"),
                //targetTemplate.ChangeDeviceId("201002"),
                //targetTemplate.ChangeDeviceId("201003"),
                targetTemplate.ChangeDeviceId("202001"),
                targetTemplate.ChangeDeviceId("203001"),
                //targetTemplate.ChangeDeviceId("202001"),
                //targetTemplate.ChangeDeviceId("201001"),
                //targetTemplate.ChangeDeviceId("201002"),
                //targetTemplate.ChangeDeviceId("201003"),

            };

            var viewModel = new DashboardViewModel
            {
                Targets = targetList,
                DataCollector = new DataCollector(targetList),  // Snmp population
                CardModel = new CardModel(),    // TODO: Get cards from DB
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Dashboard(CardModel cardModel)
        {
            FakeDatabaseManager.SaveConfig("");

            var title = cardModel.Title;    // TODO: send title to CardGenerator.cs
            var element = cardModel.Element;
            Console.WriteLine($"Title: {title}\nElement: {element}");

            // temp
            var targetTemplate = new Target(ip: FakeData.IP, port: FakeData.PORT, community: FakeData.COMMUNITY_NAME, oid: FakeData.VALUE_OID, devId: "201003");
            var targetList = new List<Target>() // TODO: Get from db
            {
                targetTemplate.ChangeDeviceId("201001"),
                targetTemplate.ChangeDeviceId("201002"),
                targetTemplate.ChangeDeviceId("201003"),
                targetTemplate.ChangeDeviceId("202001"),
                targetTemplate.ChangeDeviceId("201001"),

            };

            var newTarget = targetTemplate.ChangeDeviceId("203001");    // TODO: Get new target from cardModel.Element
            //newTarget = targetTemplate.ChangeDeviceId(element);
            targetList.Add(newTarget);

            var viewModel = new DashboardViewModel
            {
                Targets = targetList,
                DataCollector = new DataCollector(targetList),
                CardModel = cardModel,    // TODO: Get cards from DB
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}