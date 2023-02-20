using FantasyCyclingParser;
using FantasyDraftBlazor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace FantasyDraftBlazor.Pages
{
    public partial class SelectedRiderList : ComponentBase
    {

        [CascadingParameter] DraftContainer Container { get; set; }
        [Parameter] public List<Rider> SelectedRiders { get; set; }

        protected override async Task OnInitializedAsync()
        {

        }

        protected override void OnParametersSet()
        {
            //Jobs.Clear();
            // Jobs.AddRange(Container.Jobs.Where(x => x.Status == ListStatus));
            
        }
    }
}
