using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FactotumWeb.Models;
using FactotumWeb.Repository;

namespace FactotumWeb.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string DomainName = HttpContext.Request.Path;
            string Domain = HttpContext.Request.Host.Host;
            //var model = await MessageRepository.Get("http://factotumapi.azurewebsites.net/api/message");
            //var model = await MessageRepository.GetDashBoardMessages("https://localhost:5001/api/message/dashboardmessages");
            var model = await MessageRepository.GetDashBoardMessages("http://factotumapi.azurewebsites.net/api/message/dashboardmessages");
            return View(model);
            //return View();
        }

        public async Task<IActionResult> Fake()
        {


            //https://localhost:44360/api/message/dashboard/byrating

            string DomainName = HttpContext.Request.Path;
            string Domain = HttpContext.Request.Host.Host;
            //var model = await MessageRepository.GetDashBoardMessages("https://localhost:5001/api/message/dashboard/byrating");
            var model = await MessageRepository.GetDashBoardMessages("http://factotumapi.azurewebsites.net/api/message/dashboard/byrating");
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
