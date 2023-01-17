using Microsoft.AspNetCore.SignalR;

namespace FantasyDraftBlazor.Hubs
{
    public class TimerHub : Hub
    {
        public Task UpdateTimer(string user, string message)
        {
            return Clients.All.SendAsync("UpdateTimer", user, message);
        }
    }
}
