using FantasyCyclingParser;
using Microsoft.AspNetCore.Components;

namespace FantasyDraftBlazor.Pages
{
    public partial class DraftContainer : ComponentBase
    {
        [Parameter] public List<Rider> RiderList { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<Rider> OnStatusUpdated { get; set; }

        [Parameter] public EventCallback<Rider> OnRiderUndo { get; set; }

        [Parameter] public EventCallback<int> OnTimerUpdated { get; set; }

        [Parameter] public EventCallback<PDCTeam> OnTeamSave { get; set; }
        public Rider Payload { get; set; }

        public async Task UpdateRiderAsync()
        {
            RiderList.Remove(Payload);
            await OnStatusUpdated.InvokeAsync(Payload);
        }

        public async Task UndoRiderAsync()
        {
           // RiderList.Add(Payload);
            await OnRiderUndo.InvokeAsync(Payload);
        }

        public async Task UpdateTimer()
        {            
            await OnTimerUpdated.InvokeAsync(0);
        }

        public async Task SaveChangesAsync(PDCTeam team)
        {
            await OnTeamSave.InvokeAsync(team);
        }


    }
}
