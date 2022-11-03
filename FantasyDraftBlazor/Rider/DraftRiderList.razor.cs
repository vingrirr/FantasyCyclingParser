using Microsoft.AspNetCore.Components;
using FantasyCyclingParser;

namespace FantasyDraftBlazor.Rider
{
    public partial class RiderList : ComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            Riders = new List<Rider>();

            //get data here?
           
        }

        public List<Rider> Riders { get; set; }
    }
}
