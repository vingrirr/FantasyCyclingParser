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

       
        
        private void HandleDragStart(Rider selectedRider)
        {
            int x = 0; 
            Container.Payload = selectedRider;
        }

        [Parameter] public Rider Rider { get; set; }
        [CascadingParameter] DraftContainer Container { get; set; }
    }
}
