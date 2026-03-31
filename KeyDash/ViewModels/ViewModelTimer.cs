
using KeyDash.Models;
using KeyDash.MVVM;
using KeyDash.Signals;
using System.Diagnostics;
using System.Windows.Documents;
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
        public SysTimer Dispatchertimer { get;  set; }
       
        public int Seconds
        {
            get { return _seconds; }
            set
            {
                _seconds = value;
                OnPropertyChanged();
            }
        }

        public ViewModelTimer(EventBus eventBus)
        {
            Dispatchertimer = new SysTimer();
            Dispatchertimer.Interval = TimeSpan.FromSeconds(1);
            EventBus = eventBus;
            EventBus.Subcribe<StartGameEventSignal>(e => startTime(e));
        }
       

        public void startTime<T>(T startGameEvent)
        {
            if(startGameEvent is StartGameEventSignal startGame)
            {
                Dispatchertimer.Tick += Tick;
                timer = startGame.modeltimer;
                Seconds = timer.startTime;
                Dispatchertimer.Start();
            }
        }
        private void Tick(object? sender, EventArgs e)
        {
            if (Seconds == timer.endTime)
            {
                Stop();
            }
            else Seconds--;
        }

        public void Stop()
        {
            Dispatchertimer.Stop();
            EventBus.Publish(new EndTimerSignal());
            Dispatchertimer.Tick -= Tick;
        }
    }
}
