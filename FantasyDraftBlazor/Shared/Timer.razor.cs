using FantasyCyclingParser;
using FantasyDraftBlazor.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.SignalR.Client;
using Recurop;

namespace FantasyDraftBlazor.Shared
{
    public partial class Timer : ComponentBase
    {

        [CascadingParameter] DraftContainer Container { get; set; }

        RecurringOperation _timerOperation;        
        TimeSpan _displayTime = default;        
        int _elapsedSeconds = 60;
        private HubConnection? hubConnection;

        protected override async Task OnInitializedAsync()
        {
            _timerOperation = new("timer");
            _timerOperation.Operation = IncrementTimer;
            _timerOperation.StatusChanged += TimerOperationStatusChanged;
            _timerOperation.OperationFaulted += LogError;

            hubConnection = new HubConnectionBuilder()
                      .WithUrl(NavManager.ToAbsoluteUri("/timerhub"))
                      .WithAutomaticReconnect()
            .Build();

            hubConnection.On<string, string>("UpdateTimer", (user, message) =>
            {
                _elapsedSeconds--;
                _displayTime = TimeSpan.FromSeconds(_elapsedSeconds);

                InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();

        }
        void StartTimer()
        {
            Recurop.StartRecurring(_timerOperation, TimeSpan.FromSeconds(1));
        }
        void IncrementTimer()
        {
            Send();    
            //StateHasChanged();
        }

        private async Task Send()
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("UpdateTimer", "", "");
            }
        }

        void TimerOperationStatusChanged()
        {
            if (_elapsedSeconds == 50)
            {
                Container.UpdateTimer();
                _elapsedSeconds = 60;
            }
        }

        private async Task ResetTimer()
        {                        
            await Container.UpdateRiderAsync();
        }

        void LogError(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        public void Dispose()
        {
            Recurop.CancelRecurring(_timerOperation);            
        }
    }
}
