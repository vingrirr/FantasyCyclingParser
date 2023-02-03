using Microsoft.AspNetCore.Components;
using FantasyCyclingParser;
using FantasyDraftBlazor.Data;
using static System.Net.WebRequestMethods;
using System.ComponentModel;

namespace FantasyDraftBlazor.Pages
{
    public partial class DraftRiderList : ComponentBase
    {
        string dropClass = "";
        public List<Rider> RiderList { get; set; }
        [CascadingParameter] DraftContainer Container { get; set; }


        protected override async Task OnInitializedAsync()
        {
           
        }

        protected override void OnParametersSet()
        {
            //Jobs.Clear();
            // Jobs.AddRange(Container.Jobs.Where(x => x.Status == ListStatus));
            RiderList = Container.RiderList.OrderByDescending(x => x.CurrentYearCost).ThenBy(x => x.Name).ToList();
        }

        private void HandleDragEnter()
        {
            //if (ListStatus == Container.Payload) return;

            //if (AllowedStatuses != null && !AllowedStatuses.Contains(Container.Payload.Status))
            //{
              //  dropClass = "no-drop";
            //}
            //else
            //{
                dropClass = "can-drop";
            //}
        }

        private void HandleDragLeave()
        {
            dropClass = "";
        }

        private async Task HandleDrop()
        {
              dropClass = "";

            //if (AllowedStatuses != null && !AllowedStatuses.Contains(Container.Payload.Status)) return;

           // await Container.UpdateRiderAsync(ListStatus);
        }

        
    }
}
