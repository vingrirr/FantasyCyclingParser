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

        [Parameter] public Rider Rider { get; set; }
        //[CascadingParameter] JobsContainer Container { get; set; }
        private void HandleDragStart(Rider selectedRider)
        {
           // Container.Payload = selectedRider;
        }
    }
}
