using FantasyCyclingParser;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace FantasyDraftBlazor.Pages
{
    public partial class DraftTeam : ComponentBase
    {
        string dropClass = "";
        [CascadingParameter] DraftContainer Container { get; set; }
        [Parameter] public PDCTeam Team { get; set; }
        protected override async Task OnInitializedAsync()
        {

        }

        protected override void OnParametersSet()
        {
            int x = 0; 
        }

        private void HandleDragEnter()
        {

            dropClass = "can-drop";
        }

        private void HandleDragLeave()
        {
            dropClass = "";
        }

        private async Task HandleDrop()
        {
            dropClass = "";
            Team.Riders.Add(Container.Payload);

            await Container.UpdateRiderAsync();
        }
        private async Task RemoveRider()
        {
            Team.Riders.Remove(Container.Payload);
            
            await Container.UndoRiderAsync();
        }
        private async Task SaveChanges()
        {
            int x = 0; 
            await Container.SaveChangesAsync(Team);
            
        }

    }

    
}
