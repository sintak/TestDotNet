using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTest.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string message)
        {
            var name = Context.Request.HttpContext.Session.GetString("name");
            Clients.All.addNewMessageToPage(name, message);
        }

        public void Join(string name)
        {
            Context.Request.HttpContext.Session.SetString("name", name);
        }
    }
}
