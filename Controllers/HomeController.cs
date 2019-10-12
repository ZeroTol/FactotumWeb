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

            ViewData["Guid"] = Guid.NewGuid();

            var model = await MessageRepository.GetDashBoardMessages(Consts.baseAPIURL + "/api/message/dashboard/byrating");
            foreach (FactotumModels.MDashBoardFact fact in model)
            {
                var url = fact.FactLink;
                int httpsi  = url.IndexOf("http://", StringComparison.CurrentCulture);
                int httpi = url.IndexOf("https://", StringComparison.CurrentCulture);

                if (httpsi == -1 && httpi == -1)
                {
                    url = "http://" + url;
                    fact.FactLink = url;
                }

                url = url.Replace("http://", "");
                url = url.Replace("https://", "");
                int n = url.IndexOf('/');
                if (n>0)
                {
                    url = url.Remove(n);
                }

                fact.Link = url;
            }

            return View(model);

        }

        public async Task<IActionResult> Fake()
        {


            //https://localhost:44360/api/message/dashboard/byrating
            ViewData["Guid"] = Guid.NewGuid();
            string DomainName = HttpContext.Request.Path;
            string Domain = HttpContext.Request.Host.Host;
            var model = await MessageRepository.GetDashBoardMessages(Consts.baseAPIURLssl + "/api/message/dashboard/byrating");
            //var model = await MessageRepository.GetDashBoardMessages("https://factotumwebapi.azurewebsites.net/api/message/dashboard/byrating");
            return View(model);
        }

        public IActionResult Member()
        {
            ViewData["Message"] = "Membership related page.";

            return View();
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
