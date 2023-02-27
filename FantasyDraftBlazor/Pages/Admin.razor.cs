using FantasyCyclingParser;
using FantasyDraftBlazor.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;

namespace FantasyDraftBlazor.Pages
{
    public partial class Admin
    {
        protected override void OnInitialized()
        {
            Helper = new Helper();            
        }

        public async Task ClearAll()
        {
            foreach (PDCTeam t in Helper.Season.DraftTeams)
            {
                t.Riders.Clear();
            }

            Repository.PDCSeasonUpdate(Helper.Season);
        }

        public Helper Helper { get; set; }

    }
}
