﻿using FantasyCyclingParser;
using FantasyDraftBlazor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace FantasyDraftBlazor.Pages
{
    public partial class DraftTeam : ComponentBase
    {
        string dropClass = "";
        string animateClass = "";
        [CascadingParameter] DraftContainer Container { get; set; }
        [Parameter] public DraftTeamViewModel Team { get; set; }
        [Parameter] public Rider RiderToDraft { get; set; }
        [Parameter] public bool IsAdmin { get; set; }
        protected override async Task OnInitializedAsync()
        {

            //animateClass = "animate__animated animate__backInRight animate__delay-2s";
        }

        protected override void OnParametersSet()
        {
            Team.Calculate();
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


            Team.RiderToDraft = Container.Payload;
            Team.Calculate();
            await Container.DraftRiderAsync();
        }
        private async Task RemoveRider()
        {
            //Team.Model.Riders.Remove(Container.Payload);
            Team.RiderToDraft = null;
            Team.Calculate();
            await Container.UndoRiderAsync();

        }
        private async Task OverrideTeam()
        {
            Team.IsUsingOverride = true;
        }
        private async Task RemoveExistingRider(string riderId)
        {

            Rider r = Team.Model.Riders.Where(x => x.PDC_RiderID == riderId).First();
            Team.RiderToAddBackToDraft = r;
            Team.Model.Riders.Remove(r);
            await Container.RemoveExistingRider(Team);
            Team.IsUsingOverride = false;
            Team.CanUseOverride = false;
            Team.Calculate();
        }
        private async Task SaveChanges()
        {

            if (Team.RiderToDraft != null)
            {

                Team.Model.Riders.Add(Team.RiderToDraft);
                await Container.SaveChangesAsync(Team);

            }
        }

    }


}
