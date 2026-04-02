using KeyDash.Abstractions;
using KeyDash.Models;

namespace KeyDash.Signals
{
    public class ChangedModesSignal
    {
        public BaseLeftPanel LeftPanel { get; set; }
        public Modes mode { get; set; }
    }
}
