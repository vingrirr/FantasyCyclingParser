using FantasyCyclingParser;
using FantasyDraftBlazor.ViewModels;
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
            
            DraftTeams = new List<DraftTeamViewModel>();

            foreach (PDCTeam tm in Season.DraftTeams)
            {
                DraftTeamViewModel d = new DraftTeamViewModel(tm);
                DraftTeams.Add(d);
            }
            
            
            LoadAvailableRiders();
            
            Draft = new SnakeDraft(Season.DraftTeams, 25);
            
            DraftPick firstPick = Draft.DraftOrder[0];
            CurrentTeam = DraftTeams.Where(x => x.ID == firstPick.ID).First();

        }



        void HandleStatusUpdated(Rider updatedRider)
        {
            AvailableRiders.Remove(updatedRider);
        }
        void HandleRiderUndo(Rider addRider)
        {
            AvailableRiders.Add(addRider);
        }
        void HandleSaveChanges(DraftTeamViewModel team)
        {

            if (Season.DraftTeams.Where(x => x.ID == team.ID).Count() > 0)
            {
                PDCTeam temp = Season.DraftTeams.First(x => x.ID == team.ID);
                Season.DraftTeams.Remove(temp);

                Season.DraftTeams.Add(team.Model);
            }
            else
            {
                Season.DraftTeams.Add(team.Model);
            }

            Repository.PDCSeasonUpdate(Season);

            Draft.DraftOrder.RemoveAt(0); //basically this is pop
            DraftPick nextPick = Draft.DraftOrder[0];
            CurrentTeam = DraftTeams.Where(x => x.ID == nextPick.ID).First();
        }

        void HandleTimerUpdated(int timer)
        {
           // CurrentTeam = DanasTeam;
            
        }

        private void LoadAvailableRiders()
        {
            AvailableRiders = new List<Rider>(Season.Riders);

            //remove any riders which may have already been drafted
            foreach (DraftTeamViewModel team in DraftTeams)
            {
                foreach (Rider r in team.Model.Riders)
                {
                    Rider temp = AvailableRiders.Where(x => x.PDC_RiderID == r.PDC_RiderID).First();
                    AvailableRiders.Remove(temp);
                }
            }
        }

        public FantasyYearConfig Config { get; set; }

        public PDC_Season Season { get; set; }

        public List<Rider> AvailableRiders { get; set; }

        public List<DraftTeamViewModel> DraftTeams { get; set; }
        public DraftTeamViewModel CurrentTeam { get; set; }

        public SnakeDraft Draft { get; set; }

    }
}
