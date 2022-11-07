using FantasyCyclingParser;

namespace FantasyDraftBlazor.Pages
{
    public partial class Index
    {
        protected override void OnInitialized()
        {
            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            PDC_Season season = Repository.PDCSeasonGet(config.Year);
            AvailableRiders = season.Riders;
        }

        void HandleStatusUpdated(Rider updatedRider)
        {
            int x = 0;
        }

        public List<Rider> AvailableRiders { get; set; }
    }
}
