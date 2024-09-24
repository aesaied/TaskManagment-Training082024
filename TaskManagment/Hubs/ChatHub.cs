using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Plugins;

namespace TaskManagment.Hubs
{
    [Authorize]
    public class ChatHub: Hub
    {

        // 
        public static List<Message>  history = new List<Message>();

        public override async Task OnConnectedAsync()
        {

           
         

            await base.OnConnectedAsync();

            Task t1= Clients.Caller.SendAsync("onReceiveHistory", history);
            Task t2= Clients.Others.SendAsync("onUserJoinGroup", 
                Context.User.Identity.IsAuthenticated?Context.User.Identity.Name: "Guest");

            await Task.WhenAll(t1, t2);

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
