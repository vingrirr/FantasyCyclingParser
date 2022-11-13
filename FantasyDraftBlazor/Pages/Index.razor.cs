using FantasyCyclingParser;
using Microsoft.AspNetCore.Http;

namespace FantasyDraftBlazor.Pages
{
    public partial class Index
    {
        protected override void OnInitialized()
        {
            RyansTeam = new PDCTeam();
            RyansTeam.PDCTeamName = "Ryan's Draft Team";

            DanasTeam = new PDCTeam();
            DanasTeam.PDCTeamName = "Dana's Draft Team";

            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            PDC_Season season = Repository.PDCSeasonGet(config.Year);
            AvailableRiders = season.Riders;

            //foreach (PDCTeamYear ty in config.TeamUIDS)
            //{

            //    PDCTeam team = season.PDCTeams.FirstOrDefault(m => m.PDC_ID == ty.TeamUID);
            //    if (team != null && ty.Is35Team == false)
            //    {                                        
            //        PDCTeams.Add(team);
            //    }
            //}
            int z = 0;
        }

        void HandleStatusUpdated(Rider updatedRider)
        {
            AvailableRiders.Remove(updatedRider);
        }

        void HandleTimerUpdated(int timer)
        {
            int x = 0;
        }



        public List<Rider> AvailableRiders { get; set; }

        public PDCTeam RyansTeam { get; set; }
        
        public PDCTeam DanasTeam { get; set; }
    }
}
