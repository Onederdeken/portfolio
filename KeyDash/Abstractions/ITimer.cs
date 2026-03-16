using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using TheTime = System.Windows.Threading;
namespace KeyDash.Abstractions
{
    interface ITimer
    {
        public event Action<int>? TickChanged;
        public event Action? OnStart;
        public event Action? OnStop;

        public void startTime();

    }
    enum Operation
    {
        Plus,
        Minus
    }
}
