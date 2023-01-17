using Microsoft.AspNetCore.SignalR;

namespace FantasyDraftBlazor.Hubs
{
    public class DraftHub :Hub
    {
        public Task SendDraftListUpdate(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveDraftListUpdate", user, message);
        }
    }
}
