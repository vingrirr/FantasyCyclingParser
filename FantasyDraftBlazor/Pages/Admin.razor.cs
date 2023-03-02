using FantasyCyclingParser;
using FantasyDraftBlazor.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.ComponentModel;
using System.Configuration;

namespace FantasyDraftBlazor.Pages
{
    public partial class Admin : ComponentBase
    {
        [CascadingParameter] DraftContainer Container { get; set; }
        protected override void OnInitialized()
        {
            Helper = new Helper();
            draftContainer = new DraftContainer(); 
        }

        public async Task ClearAll()
        {
            foreach (PDCTeam t in Helper.Season.DraftTeams)
            {
                t.Riders.Clear();
            }

            Repository.PDCSeasonUpdate(Helper.Season);
        }

        public async Task TabClick(int index)
        {
            switch(index)
            {
                case 0:
                    Helper.CurrentTeam = Helper.Dana;                    
                    break;
                case 1:
                    Helper.CurrentTeam = Helper.Ryan;
                    break;
                case 2:
                    Helper.CurrentTeam = Helper.Tim;
                    break;
                case 3:
                    Helper.CurrentTeam = Helper.Alex;
                    break;
                case 4:
                    Helper.CurrentTeam = Helper.Allen;
                    break;
                case 5:
                    Helper.CurrentTeam = Helper.Bill;
                    break;

            }
            draftContainer.CurrentDraftTeam = Helper.CurrentTeam; 
        }

        public Helper Helper { get; set; }

        public DraftContainer draftContainer { get; set; }   

    }
}
