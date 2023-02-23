using FantasyCyclingParser;
using FantasyDraftBlazor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace FantasyDraftBlazor.Pages
{
    public partial class DraftContainer : ComponentBase
    {
        [Parameter] public List<Rider> RiderList { get; set; }
        [Parameter] public DraftTeamViewModel CurrentDraftTeam { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<Rider> OnStatusUpdated { get; set; }
        [Parameter] public EventCallback<Rider> OnRiderDrafted { get; set; }

        [Parameter] public EventCallback<Rider> OnRiderUndo { get; set; }
        

        [Parameter] public EventCallback<int> OnTimerUpdated { get; set; }

        [Parameter] public EventCallback<DraftTeamViewModel> OnTeamSave { get; set; }
        [Parameter] public EventCallback<DraftTeamViewModel> OnExistingRiderUndo { get; set; }
        public Rider Payload { get; set; }

        public async Task DraftRiderAsync()
        {
            RiderList.Remove(Payload);
            CurrentDraftTeam.RiderToDraft = Payload;  
            await OnRiderDrafted.InvokeAsync(Payload);
        }

        public async Task UndoRiderAsync()
        {
           // RiderList.Add(Payload);
            await OnRiderUndo.InvokeAsync(Payload);
        }
        public async Task AddRiderAsync(Rider rider)
        {
            // RiderList.Add(Payload);
            await OnRiderUndo.InvokeAsync(rider);
        }

        public async Task RemoveExistingRider(DraftTeamViewModel team)
        {
            // RiderList.Add(Payload);
            await OnExistingRiderUndo.InvokeAsync(team);
        }

        public async Task UpdateTimer()
        {            
            await OnTimerUpdated.InvokeAsync(0);
        }

        public async Task SaveChangesAsync(DraftTeamViewModel team)
        {
            await OnTeamSave.InvokeAsync(team);
        }


    }
}
