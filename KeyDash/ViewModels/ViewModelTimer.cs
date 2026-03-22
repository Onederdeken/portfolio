
using KeyDash.Models;
using KeyDash.MVVM;
using KeyDash.Signals;
using ITimer = KeyDash.Abstractions.ITimer;
using SysTimer =  System.Windows.Threading.DispatcherTimer;


namespace KeyDash.ViewModels
{
    public class ViewModelTimer : ViewModelBase, ITimer
    {
        private int _seconds;
        private EventBus EventBus { get;}
        public ModelTimer timer;
        private Game game;
        private readonly SysTimer Dispatchertimer = new SysTimer();
        public int Seconds
        {
            get { return _seconds; }
            set
            {
                _seconds=value;
                OnPropertyChanged();
            }
        }

        public ViewModelTimer(EventBus eventBus)
        {
            Dispatchertimer.Tick += Tick;
            Dispatchertimer.Interval = TimeSpan.FromSeconds(1);
            EventBus = eventBus;
            EventBus.Subcribe<StartGameEventSignal>(e => startTime(e));
        }

        public void startTime(StartGameEventSignal startGameEvent)
        {
            timer = startGameEvent.modeltimer;
            Seconds = timer.startTime;
            Dispatchertimer.Start();
            
        }

        private void Tick(object? sender, EventArgs e)
        {
            if (timer.operation == Operation.Minus) Seconds--;
            else if (timer.operation == Operation.Plus) Seconds++;

            if (Seconds == timer.endTime)
            {
                Dispatchertimer.Stop();
                EventBus.Publish(new EndTimerSignal());
            }
        }
    }
}
