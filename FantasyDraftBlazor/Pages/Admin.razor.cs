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

        private void LoadAvailableRiders()
        {
            AvailableRiders = new List<Rider>(Season.Riders);

            //remove any riders which may have already been drafted
            //foreach (DraftTeamViewModel team in DraftTeams)
            //{
            //    foreach (Rider r in team.Model.Riders)
            //    {
            //        Rider temp = AvailableRiders.Where(x => x.PDC_RiderID == r.PDC_RiderID).First();
            //        AvailableRiders.Remove(temp);
            //    }
            //}
        }


        public async Task ClearAll()
        {
            foreach (PDCTeam t in Season.DraftTeams)
            {
                t.Riders.Clear();
            }

            Repository.PDCSeasonUpdate(Season);
        }

        public Helper Helper { get; set; }
        public FantasyYearConfig Config { get; set; }

        public PDC_Season Season { get; set; }

        public List<Rider> AvailableRiders { get; set; }

        public DraftTeamViewModel Dana { get; set; }
        public DraftTeamViewModel Ryan { get; set; }
        public DraftTeamViewModel Tim { get; set; }
        public DraftTeamViewModel Alex { get; set; }
        public DraftTeamViewModel Allen { get; set; }
        public DraftTeamViewModel Bill { get; set; }
    }
}
