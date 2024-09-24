using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Plugins;

namespace TaskManagment.Hubs
{
    [Authorize]
    public class ChatHub: Hub
    {

        public static List<Message>  history = new List<Message>();

        public override async Task OnConnectedAsync()
        {

           
           await  base.OnConnectedAsync();


        }


        // method can call  from client
        public async Task SendMessage(string msg , string name)
        {

            Message m = new Message { Body = msg, Name = name };
            history.Add(m);
            await this.Clients.All.SendAsync("onReceiveMessage", msg, name);



        }
    }

    public class Message
    {
        public required string Name { get; set; }
        public required string Body { get; set; }
    }
}
