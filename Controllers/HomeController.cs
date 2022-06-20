using AdminLTE_MVC.Models;
using AdminLTE_MVC.Models.Dashboard;
using AdminLTE_MVC.Data.FakeDatabase;
using AdminLTE_MVC.Snmp;
using Lextm.SharpSnmpLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;

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
            var targetTemplate = new Target(ip: "192.168.2.11", port: 161, community: "public", oid: "1.3.6.1.4.1.39052.5.2.1.7", devId: "201003"); // 5.2.1 == Analogs

            var targetList = new List<Target>()
            {
                targetTemplate.ChangeDeviceId("201001"),
                targetTemplate.ChangeDeviceId("201002"),
                targetTemplate.ChangeDeviceId("201003"),
                targetTemplate.ChangeDeviceId("202001")
            };
            return View(targetList);
        }

        public IActionResult Dashboard()
        {
            // Get device IDs by walking the device id agent
            var target = new Target(ip: FakeData.IP, community: FakeData.COMMUNITY_NAME, oid: FakeData.DEVID_OID);
            var devIdList = SnmpManager.WalkValue(target);

            var targetList = new List<Target>();
            foreach (var devId in devIdList)
            {
                targetList.Add(target.ChangeDeviceId(devId));
            }

            // temp
            var targetTemplate = new Target(ip: "192.168.2.11", port: 161, community: "public", oid: "1.3.6.1.4.1.39052.5.2.1.7", devId: "201003"); // 5.2.1 == Analogs
            targetList = new List<Target>()
            {
                targetTemplate.ChangeDeviceId("201001"),
                targetTemplate.ChangeDeviceId("201002"),
                targetTemplate.ChangeDeviceId("201003"),
                targetTemplate.ChangeDeviceId("202001"),
                targetTemplate.ChangeDeviceId("201001"),
                targetTemplate.ChangeDeviceId("201002"),
                targetTemplate.ChangeDeviceId("201003"),
                targetTemplate.ChangeDeviceId("202001"),
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
            var title = cardModel.Title;
            var element = cardModel.Element;
            // Add new card

            Console.WriteLine($"Title: {title}\nElement: {element}");

            // Get device IDs by walking the device id agent
            var target = new Target(ip: FakeData.IP, community: FakeData.COMMUNITY_NAME, oid: FakeData.DEVID_OID);
            var devIdList = SnmpManager.WalkValue(target);

            var targetList = new List<Target>();
            foreach (var devId in devIdList)
            {
                targetList.Add(target.ChangeDeviceId(devId));
            }

            var viewModel = new DashboardViewModel
            {
                Targets = targetList,
                DataCollector = new DataCollector(targetList),
                CardModel = new CardModel(),    // TODO: Get cards from DB
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