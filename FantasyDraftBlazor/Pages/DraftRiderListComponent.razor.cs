using Microsoft.AspNetCore.Components;
using FantasyCyclingParser;
using FantasyDraftBlazor.Data;
using static System.Net.WebRequestMethods;

namespace FantasyDraftBlazor.Pages
{
    public partial class DraftRiderListComponent : ComponentBase
    {

        protected override async Task OnInitializedAsync()
        {
            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            PDC_Season season = Repository.PDCSeasonGet(config.Year);

            RiderList = season.Riders;
        }
        public List<Rider> RiderList { get; set; }
    }
}
