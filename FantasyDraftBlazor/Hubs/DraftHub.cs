using Microsoft.AspNetCore.SignalR;

namespace FantasyDraftBlazor.Hubs
{
    public class DraftHub :Hub
    {
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
