using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SignalRServer.Hubs
{
    public class ChatHubs : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> Connection Established " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task SendMessageAsync(string message)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(message);
            string toClient = routeOb.To;
            Console.WriteLine("Message Recieved on:" + Context.ConnectionId);

            if (string.IsNullOrEmpty(toClient))
            {
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
            else
            {
                await Clients.Client(toClient).SendAsync("ReceiveMessage", message);
            }
        }
    }
}