using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRTest.Hubs;

namespace SignalRTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<ChatHub> _hub;

        public HomeController(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        public IActionResult SendMessageToClients(string message)
        {
            _hub.Clients.All.sendMessageFromServer(message);
            return Content("<h1>OK</h1>");
        }

        //

        public IActionResult Index()
        {
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

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}
