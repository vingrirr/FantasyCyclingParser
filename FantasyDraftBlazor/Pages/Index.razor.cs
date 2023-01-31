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

            TimsTeam = new PDCTeam();
            TimsTeam.PDCTeamName = "Tim's Draft Team";

            BillsTeam = new PDCTeam();
            BillsTeam.PDCTeamName = "Bill's Draft Team";

            AlexsTeam = new PDCTeam();
            AlexsTeam.PDCTeamName = "Alex's Draft Team";

            AllensTeam = new PDCTeam();
            AllensTeam.PDCTeamName = "Allen's Draft Team";

            DraftTeams = new List<PDCTeam>();
            DraftTeams.Add(RyansTeam);
            DraftTeams.Add(DanasTeam);
            DraftTeams.Add(TimsTeam);
            DraftTeams.Add(BillsTeam);
            DraftTeams.Add(AlexsTeam);
            DraftTeams.Add(AllensTeam);

            
            
            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            PDC_Season season = Repository.PDCSeasonGet(config.Year);
            AvailableRiders = season.Riders;

            CurrentTeam = RyansTeam; 

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
