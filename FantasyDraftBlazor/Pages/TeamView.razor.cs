using FantasyCyclingParser;
using FantasyDraftBlazor.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace FantasyDraftBlazor.Pages
{
    public partial class TeamView
    {
        protected override void OnInitialized()
        {

            AvailableRiders = new List<Rider>();

            string id = "";

            switch(Name.ToLower())
            {
                case "dana":
                    id = "fc2e7a01-3a31-4aa2-bdcc-1203933932bc";
                    break;
                case "ryan":
                    id = "c9c8d30e-6264-4455-b60a-d50b7bac983c";
                    break;
                case "tim":
                    id = "0b90f656-e1f0-4a9b-af34-5724f126a13b";
                    break;
                case "alex":
                    id = "7b8e450c-1079-4cc6-bc7a-42479657799d";
                    break;
                case "allen":
                    id = "3ab287a5-5a34-4dda-9203-a6bc2404ee15";
                    break;
                case "bill":
                    id = "1ebb9ae7-0467-4522-b4dc-fe7fc7803806";
                    break;


            }

            Config = Repository.FantasyYearConfigGetDefaultDraft();
            Season = Repository.PDCSeasonGet(Config.Year);
            DraftTeams = new List<DraftTeamViewModel>(); 

            PDCTeam temp = Season.DraftTeams.Where(x => x.ID == id).First();

            DraftTeamViewModel d = new DraftTeamViewModel(temp);
            TheView = d;

            LoadAvailableRiders();
        }


        private void LoadAvailableRiders()
        {

            PDCTeam dana = Season.DraftTeams.Where(x => x.ID == "fc2e7a01-3a31-4aa2-bdcc-1203933932bc").First();
            PDCTeam allen = Season.DraftTeams.Where(x => x.ID == "3ab287a5-5a34-4dda-9203-a6bc2404ee15").First();
            PDCTeam alex = Season.DraftTeams.Where(x => x.ID == "7b8e450c-1079-4cc6-bc7a-42479657799d").First();
            PDCTeam tim = Season.DraftTeams.Where(x => x.ID == "0b90f656-e1f0-4a9b-af34-5724f126a13b").First();
            PDCTeam ryan = Season.DraftTeams.Where(x => x.ID == "c9c8d30e-6264-4455-b60a-d50b7bac983c").First();
            PDCTeam bill = Season.DraftTeams.Where(x => x.ID == "1ebb9ae7-0467-4522-b4dc-fe7fc7803806").First();


            DraftTeamViewModel d = new DraftTeamViewModel(dana);
            DraftTeams.Add(d);

            DraftTeamViewModel al = new DraftTeamViewModel(allen);
            DraftTeams.Add(al);

            DraftTeamViewModel ax = new DraftTeamViewModel(alex);
            DraftTeams.Add(ax);

            DraftTeamViewModel t = new DraftTeamViewModel(tim);
            DraftTeams.Add(t);

            DraftTeamViewModel r = new DraftTeamViewModel(ryan);
            DraftTeams.Add(r);

            DraftTeamViewModel b = new DraftTeamViewModel(bill);
            DraftTeams.Add(b);
            AvailableRiders = new List<Rider>(Season.Riders);

            //remove any riders which may have already been drafted
            foreach (DraftTeamViewModel team in DraftTeams)
            {
                foreach (Rider rd in team.Model.Riders)
                {
                    Rider temp = AvailableRiders.Where(x => x.PDC_RiderID == rd.PDC_RiderID).First();
                    AvailableRiders.Remove(temp);
                }
            }
        }
        public DraftTeamViewModel TheView { get; set; }

        [Parameter]
        public string? Name { get; set; }

        public List<Rider> AvailableRiders { get; set; }

        public FantasyYearConfig Config { get; set; }

        public PDC_Season Season { get; set; }

        public List<DraftTeamViewModel> DraftTeams { get; set; }
    }
}
