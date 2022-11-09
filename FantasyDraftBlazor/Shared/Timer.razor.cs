using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Recurop;

namespace FantasyDraftBlazor.Shared
{
    public partial class Timer : ComponentBase
    {

        RecurringOperation _timerOperation;        
        TimeSpan _displayTime = default;        
        int _elapsedSeconds;

        protected override void OnInitialized()
        {
            _timerOperation = new("timer");
            _timerOperation.Operation = IncrementTimer;
            _timerOperation.StatusChanged += TimerOperationStatusChanged;
            _timerOperation.OperationFaulted += LogError;

            StartTimer();

        }
        void StartTimer()
        {
            Recurop.StartRecurring(_timerOperation, TimeSpan.FromSeconds(1));
        }
        void IncrementTimer()
        {
            _elapsedSeconds++;
            _displayTime = TimeSpan.FromSeconds(_elapsedSeconds);

            InvokeAsync(StateHasChanged);
            //StateHasChanged();
        }

        void TimerOperationStatusChanged()
        {
            int x = 0; 
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
