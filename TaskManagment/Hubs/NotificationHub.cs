using Microsoft.AspNetCore.SignalR;

namespace TaskManagment.Hubs
{
    public class NotificationHub:Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            if (Context.User.IsInRole(SystemRoles.Admins))
            {
              await  Groups.AddToGroupAsync(Context.ConnectionId,SystemRoles.Admins);
            }
        }
    }
}
