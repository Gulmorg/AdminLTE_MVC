using AdminLTE_MVC.Models;
using AdminLTE_MVC.Snmp;
using Lextm.SharpSnmpLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminLTE_MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()    // TODO: Snmp walk instead of creating a list
        {
            var targetTemplate = new Target(ip: "192.168.2.11", port: 161, community: "public", oid: "1.3.6.1.4.1.39052.1.3.1.9", devId: "201003"); // 5.2.1 == Analogs

            var targetList = new List<Target>()
            {
                targetTemplate.ChangeDeviceId("201001"),
                targetTemplate.ChangeDeviceId("201002"),
                targetTemplate.ChangeDeviceId("201003"),
                targetTemplate.ChangeDeviceId("202001")
            };
            return View(targetList);
        }

        public IActionResult Dashboard()    // TODO: Snmp walk instead of creating a list
        {
            var targetTemplate = new Target(ip: "192.168.2.11", community: "public", oid: "1.3.6.1.4.1.39052.1.3.1.9");

            var targetList = new List<Target>()
            {
                targetTemplate.ChangeDeviceId("201001"),
                targetTemplate.ChangeDeviceId("201002"),
                targetTemplate.ChangeDeviceId("201003"),
                targetTemplate.ChangeDeviceId("202001")
            };
            return View(targetList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}