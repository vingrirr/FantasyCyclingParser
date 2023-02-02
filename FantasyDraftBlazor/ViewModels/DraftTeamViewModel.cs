using FantasyCyclingParser;

namespace FantasyDraftBlazor.ViewModels
{
    public class DraftTeamViewModel
    {
        public DraftTeamViewModel()
        {
            Riders = new List<DraftRiderViewModel>();
        }
        public string ID { get; set; }

        public bool IsDraftPDCTeam { get; set; }

        public bool Is35Team { get; set; }

        public string PDCTeamName { get; set; }
        public PDCTeam Model { get; set; }
        public List<DraftRiderViewModel> Riders { get; set; }
        
    }
}
