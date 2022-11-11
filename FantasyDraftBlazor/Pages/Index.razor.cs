using FantasyCyclingParser;
using Microsoft.AspNetCore.Http;

namespace FantasyDraftBlazor.Pages
{
    public partial class Index
    {
        protected override void OnInitialized()
        {
            DraftTeam = new PDCTeam();
            DraftTeam.PDCTeamName = "Fake Draft Team";

            DraftTeam2 = new PDCTeam();
            DraftTeam2.PDCTeamName = "Other Draft Team";

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
    


        public List<Rider> AvailableRiders { get; set; }

        public PDCTeam DraftTeam { get; set; }
        
        public PDCTeam DraftTeam2 { get; set; }
    }
}
