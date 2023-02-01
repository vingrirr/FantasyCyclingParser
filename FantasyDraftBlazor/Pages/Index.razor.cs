using FantasyCyclingParser;
using Microsoft.AspNetCore.Http;

namespace FantasyDraftBlazor.Pages
{
    public partial class Index
    {
        protected override void OnInitialized()
        {
            DraftTeams = new List<PDCTeam>();

 
            FantasyYearConfig config = Repository.FantasyYearConfigGetDefaultDraft();
            PDC_Season season = Repository.PDCSeasonGet(config.Year);
            AvailableRiders = season.Riders;

            foreach(PDCTeamYear t in config.TeamUIDS)
            {
                PDCTeam temp = new PDCTeam(t, true);
                DraftTeams.Add(temp);
            }
            //CurrentTeam = config.TeamUIDS.FirstOrDefault(); 
            CurrentTeam = DraftTeams.First();

            int z = 0;
        }

        void HandleStatusUpdated(Rider updatedRider)
        {
            AvailableRiders.Remove(updatedRider);
        }
        void HandleRiderUndo(Rider addRider)
        {
            AvailableRiders.Add(addRider);
        }

        void HandleTimerUpdated(int timer)
        {
            CurrentTeam = DanasTeam;
            
        }



        public List<Rider> AvailableRiders { get; set; }

        public List<PDCTeam> DraftTeams { get; set; }
        public PDCTeam CurrentTeam { get; set; }
        public PDCTeam RyansTeam { get; set; }
        
        public PDCTeam DanasTeam { get; set; }

        public PDCTeam TimsTeam { get; set; }

        public PDCTeam BillsTeam { get; set; }

        public PDCTeam AlexsTeam { get; set; }

        public PDCTeam AllensTeam { get; set; }


    }
}
