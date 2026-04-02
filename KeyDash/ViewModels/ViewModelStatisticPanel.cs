using KeyDash.Abstractions;
using KeyDash.Controls;
using KeyDash.Models;
using KeyDash.MVVM;
using KeyDash.Signals;
using System.Data;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using ITimer = KeyDash.Abstractions.ITimer;
using SysTimer = System.Windows.Threading.DispatcherTimer;
namespace KeyDash.ViewModels
{
    public class ViewModelStatisticPanel:ViewModelBase, ITimer
    {
        private EventBus EventBus { get; }
        private StatisticModel statM = new StatisticModel();
        private Stopwatch stopwatch = new Stopwatch();
        private int errors;
        private double wpm;
        private double cpm;
        private string FullText;
        private double accuracy;
        private int totalerrors;
        private double netaccuracy;
        private double netwpm;
        private double netcpm;

        public double NetAccuracy { get { return netaccuracy; }  set { netaccuracy = value; OnPropertyChanged(); }   }
        public int TotalErrors { get {  return totalerrors; } set { totalerrors = value; OnPropertyChanged(); } }
        public double Accuracy { get{  return accuracy; } set {  accuracy = value; OnPropertyChanged(); }  }
        public double CPM { get { return cpm; } set { cpm = value; OnPropertyChanged(); } }
        public double NETCPM { get { return netcpm; } set { netcpm = value; OnPropertyChanged(); } }
        public double NETWPM { get { return netwpm; } set { netwpm = value; OnPropertyChanged(); } }
        public int Errors { get { return errors; } set { errors =value; OnPropertyChanged(); }  }
        public double WPM { get { return wpm; } set { wpm = value; OnPropertyChanged(); }  }
        public SysTimer Dispatchertimer { get; set; }
        public Double Seconds => (Double)stopwatch.Elapsed.TotalSeconds;
        public TimeSpan RightTime => TimeSpan.FromSeconds(Seconds);

        public ViewModelStatisticPanel(EventBus eventBus)
        {
           
            Dispatchertimer = new SysTimer();
            Dispatchertimer.Interval = TimeSpan.FromMilliseconds(500);
            EventBus = eventBus;
            EventBus.Subcribe<StartTimerEventSignal>(_ => startTime<StartTimerEventSignal>(_));
            EventBus.Subcribe<EndGame>(_=> Stop());
            this.EventBus.Subcribe<FileTextModel>(param => setText(param));
            this.EventBus.Subcribe<InputChar>(ic => CheckForError(ic));

        }
        private void CheckForError(InputChar? ic)
        {
            if (ic.workKey == WorkKey.BackSpace)
            {
                statM.CurrentTyped--;
                if (statM.Characters[ic.index].IsCorrect)
                {
                    statM.CorrectCount--;
                }
                else
                {
                    Errors--;
                }
                statM.Characters.Remove(statM.Characters[ic.index]);
            }
            else if(ic.index < FullText.Length)
            {
                statM.TotalTyped++;
                statM.CurrentTyped++;
                var character = new InputEntry()
                {
                    index = ic.index,
                    Char = ic.Item[0]
                };
                if (ic.Item != FullText[ic.index].ToString())
                {
                    TotalErrors++;
                    Errors++;
                    character.IsCorrect = false;
                }
                else
                {
                    statM.CorrectCount++;
                    character.IsCorrect = true;
                }
                statM.Characters.Add(character); 
            }
        }
        private void UpdateStat()
        {
            if (statM.TotalTyped > 0)
            {
                Accuracy = (double)(statM.TotalTyped - TotalErrors) / statM.TotalTyped * 100;
                NetAccuracy = (double)statM.CorrectCount / statM.CurrentTyped*100;
            }
            WPM = (double)((statM.TotalTyped / 5.0) / (Seconds / 60.0));
            CPM = (double)(statM.TotalTyped / (Seconds / 60.0));
            NETWPM = (double)((statM.CorrectCount / 5.0) / (Seconds / 60.0));
            NETCPM = (double)(statM.CorrectCount / (Seconds / 60.0));
        }
       
        private void setText(FileTextModel? param)  
        {
            FullText = param.text.Replace(Environment.NewLine, "");
        }
        #region timer
        public void startTime<T>(T g)
        {
            stopwatch.Start();
            Dispatchertimer.Tick += Tick;
            Dispatchertimer.Start();
        }
        private void Tick(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Seconds));
            OnPropertyChanged(nameof(RightTime));
            UpdateStat();

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
