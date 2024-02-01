using Microsoft.AspNetCore.Components;

namespace FantasyDraftBlazor.Pages
{
    public partial class UndoLastRiderPicked : ComponentBase
    {
        [CascadingParameter] DraftContainer Container { get; set; }

        protected override async Task OnInitializedAsync()
        {


        }

        protected override void OnParametersSet()
        {

        }
        private async Task UndoLastPick()
        {

            await Container.UndoLastPickAsync();

        }
    }
}
