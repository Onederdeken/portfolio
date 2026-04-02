
using KeyDash.Signals;
using System.Diagnostics;
using SysTimer = System.Windows.Threading.DispatcherTimer;
namespace KeyDash.Abstractions
{
    public interface ITimer
    {
       
        public SysTimer Dispatchertimer { get; set; }

        public void startTime<T>(T startGameEvent = default);
        public void Stop();
    }
    
}
