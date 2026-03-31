using KeyDash.Abstractions;
using KeyDash.Controls;
using KeyDash.Models;
using KeyDash.MVVM;
using KeyDash.Signals;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using ITimer = KeyDash.Abstractions.ITimer;
using SysTimer = System.Windows.Threading.DispatcherTimer;
namespace KeyDash.ViewModels
{
    public class ViewModelStatisticPanel:ViewModelBase, ITimer
    {
        private EventBus EventBus { get; }
        private Double percentErrors;
        private int errors;
        private double velocity;
        private int _seconds;
        private string FullText;
        private int IndexText = 0;
        private Stopwatch stopwatch = new Stopwatch();
        private double accuracy;
        private StatisticModel statM = new StatisticModel();
        public double Accuracy { get{  return accuracy; } set {  accuracy = value; OnPropertyChanged(); }  }
        public Double PercentErrors { get { return percentErrors; } set { percentErrors = value*100; OnPropertyChanged(); }  }
        public int Errors { get { return errors; } set { errors =value; OnPropertyChanged(); }  }
        public double Velocity { get { return velocity; } set { velocity = value; OnPropertyChanged(); }  }
        public SysTimer Dispatchertimer { get; set; }
        public int Seconds => (int)stopwatch.Elapsed.TotalSeconds;
        public TimeSpan RightTime => TimeSpan.FromSeconds(Seconds);

        public ViewModelStatisticPanel(EventBus eventBus)
        {
            
            Dispatchertimer = new SysTimer();
            Dispatchertimer.Interval = TimeSpan.FromMilliseconds(100);
            EventBus = eventBus;
            PercentErrors = 0;
            velocity = 0;
            EventBus.Subcribe<StartTimerEventSignal>(_ => startTime<StartTimerEventSignal>(_));
            EventBus.Subcribe<EndGame>(_=> Stop());
            this.EventBus.Subcribe<FileTextModel>(param => setText(param));
            this.EventBus.Subcribe<InputChar>(ic => CheckForError(ic));

        }
        private void CheckForError(InputChar? ic)
        {
            if (ic.workKey == WorkKey.BackSpace) return;
            if(ic.index < FullText.Length)
            {
                statM.TotalTyped++;
                if (ic.Item != FullText[ic.index].ToString()) Errors++;
                else statM.CorrectCount++;
            }
        }
        private void AccuracyCount()
        {
            if(statM.TotalTyped > 0)
            Accuracy = (double)(statM.TotalTyped - Errors)/statM.TotalTyped * 100;
        }
        private void setText(FileTextModel? param)  
        {
            FullText = param.text.Replace(Environment.NewLine, "");
        }
        #region timer
        public void startTime<T>(T g)
        {
           
            Dispatchertimer.Tick += Tick;
            Dispatchertimer.Start();
        }
        private void Tick(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Seconds));
            OnPropertyChanged(nameof(RightTime));
            AccuracyCount();
        }
        public void Stop()
        {
            stopwatch.Stop();
            stopwatch.Reset();
            Dispatchertimer.Stop();
            Dispatchertimer.Tick -= Tick;
           
        }
        #endregion
        
    }
}
