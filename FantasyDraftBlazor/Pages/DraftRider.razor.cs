using FantasyCyclingParser;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace FantasyDraftBlazor.Pages
{
    public partial class DraftRider : ComponentBase
    {
        protected override async Task OnInitializedAsync()
        {

        }

       
        
        private async Task HandleDraftRider()
        {
            
            Container.Payload = Rider;
            await Container.DraftRiderAsync();
        }

        [Parameter] public Rider Rider { get; set; }
        [CascadingParameter] DraftContainer Container { get; set; }
    }
}
