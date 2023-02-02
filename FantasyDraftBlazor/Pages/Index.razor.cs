using FantasyCyclingParser;
using Microsoft.AspNetCore.Http;

namespace FantasyDraftBlazor.Pages
{
    public partial class Index
    {
        protected override void OnInitialized()
        {             
            Config = Repository.FantasyYearConfigGetDefaultDraft();
            Season = Repository.PDCSeasonGet(Config.Year);
            

            if (Season.DraftTeams.Count() == 0)
            {
                //build initial draft teams
                foreach (PDCTeamYear t in Config.TeamUIDS)
                {
                    PDCTeam temp = new PDCTeam(t, true);
                    Season.DraftTeams.Add(temp);                    
                }
                Repository.PDCSeasonUpdate(Season);
            }
            
            DraftTeams = new List<PDCTeam>(Season.DraftTeams);
            //CurrentTeam = config.TeamUIDS.FirstOrDefault(); 
            CurrentTeam = DraftTeams.Where(x => x.ID == "d194d906-c138-47a5-8084-3545eca36e28").First();
            LoadAvailableRiders();

        }



        void HandleStatusUpdated(Rider updatedRider)
        {
            AvailableRiders.Remove(updatedRider);
        }
        void HandleRiderUndo(Rider addRider)
        {
            AvailableRiders.Add(addRider);
        }
        void HandleSaveChanges(PDCTeam team)
        {
            int x = 0;
            //CurrentTeam = DanasTeam;

            if (Season.DraftTeams.Where(x => x.ID == team.ID).Count() > 0)
            {
                PDCTeam temp = Season.DraftTeams.First(x => x.ID == team.ID);
                Season.DraftTeams.Remove(temp);
                
                Season.DraftTeams.Add(team);
            }
            else
            {
                Season.DraftTeams.Add(team);
            }
            
            Repository.PDCSeasonUpdate(Season);


        }

        void HandleTimerUpdated(int timer)
        {
            CurrentTeam = DanasTeam;
            
        }

        private void LoadAvailableRiders()
        {
            AvailableRiders = new List<Rider>(Season.Riders);

            //remove any riders which may have already been drafted
            foreach (PDCTeam team in DraftTeams)
            {
                foreach (Rider r in team.Riders)
                {
                    Rider temp = AvailableRiders.Where(x => x.PDC_RiderID == r.PDC_RiderID).First();
                    AvailableRiders.Remove(temp);
                }
            }
        }

        public FantasyYearConfig Config { get; set; }

        public PDC_Season Season { get; set; }

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
