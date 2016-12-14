using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ArasServiceObject.Hubs
{   
    //连接的Hub一定要与Hub Class中的名字一样    
    public class ChatHub : Hub
    {        
        //public void Send(string name,string pi_message)
        //{
        //    Clients.All.addNewMessageToPage(name, DateTime.Now.ToString() + "--" + pi_message);            
        //}

        static IHubContext signalRHub;

        public static void Send(string name, string pi_message)
        {
            if (signalRHub == null)
            {
                signalRHub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            }
            if (signalRHub != null)
            {
                signalRHub.Clients.All.addNewMessageToPage(name, DateTime.Now.ToString() + "--" + pi_message);
            }
        }
    }
}