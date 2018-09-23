using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.SignalRHub.Hubs
{
    public class DietHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            //await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            await base.OnConnectedAsync();

            await this.Clients.All.SendAsync("UserConnected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            await base.OnDisconnectedAsync(exception);

            await this.Clients.All.SendAsync("UserDisConnected");
        }
    }
}