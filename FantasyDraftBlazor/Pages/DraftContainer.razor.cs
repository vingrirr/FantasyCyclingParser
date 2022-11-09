﻿using FantasyCyclingParser;
using Microsoft.AspNetCore.Components;

namespace FantasyDraftBlazor.Pages
{
    public partial class DraftContainer : ComponentBase
    {
        [Parameter] public List<Rider> RiderList { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<Rider> OnStatusUpdated { get; set; }
        public Rider Payload { get; set; }

        public async Task UpdateRiderAsync()
        {
            RiderList.Remove(Payload);
            await OnStatusUpdated.InvokeAsync(Payload);
        }


    }
}