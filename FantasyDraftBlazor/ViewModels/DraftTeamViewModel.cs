using FantasyCyclingParser;

namespace FantasyDraftBlazor.ViewModels
{
    public class DraftTeamViewModel
    {
        public DraftTeamViewModel()
        {
           // Riders = new List<DraftRiderViewModel>();
        }
        public DraftTeamViewModel(PDCTeam team)
        {
            Model = team;
            ID = team.ID;
            TeamName = team.PDCTeamName;
            HasUsedOverride = false; 

        }
        public string ID { get; set; }

        public string TeamName { get; set; }
        public PDCTeam Model { get; set; }
        public Rider RiderToDraft { get; set; }

        public bool HasUsedOverride { get; set; }
        
    }
}
