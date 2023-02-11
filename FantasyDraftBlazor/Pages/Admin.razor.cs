using FantasyCyclingParser;
using FantasyDraftBlazor.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;

namespace FantasyDraftBlazor.Pages
{
    public partial class Admin
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

            


            
            //note: Must load available riders after draft teams have been made so we filter out
            //any already selected riders
            LoadAvailableRiders();

            PDCTeam dana = Season.DraftTeams.Where(x => x.ID == "fc2e7a01-3a31-4aa2-bdcc-1203933932bc").First();
            PDCTeam allen = Season.DraftTeams.Where(x => x.ID == "3ab287a5-5a34-4dda-9203-a6bc2404ee15").First();
            PDCTeam alex = Season.DraftTeams.Where(x => x.ID == "7b8e450c-1079-4cc6-bc7a-42479657799d").First();
            PDCTeam tim = Season.DraftTeams.Where(x => x.ID == "0b90f656-e1f0-4a9b-af34-5724f126a13b").First();
            PDCTeam ryan = Season.DraftTeams.Where(x => x.ID == "c9c8d30e-6264-4455-b60a-d50b7bac983c").First();
            PDCTeam bill = Season.DraftTeams.Where(x => x.ID == "1ebb9ae7-0467-4522-b4dc-fe7fc7803806").First();
            
            Dana = new DraftTeamViewModel(dana);            
            Allen = new DraftTeamViewModel(allen);            
            Alex = new DraftTeamViewModel(alex);            
            Tim = new DraftTeamViewModel(tim);            
            Ryan = new DraftTeamViewModel(ryan);            
            Bill = new DraftTeamViewModel(bill);
            


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
