using KeyDash.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using ITimer = KeyDash.Abstractions.ITimer;
namespace KeyDash.Models
{
    class Timer : ITimer
    {
        private int _SecondStart;
        private int _SecondStop;
        private Operation _operation;
        private DispatcherTimer _timer;

        public event Action<int>? TickChanged;
        public event Action? OnStart;
        public event Action? OnStop;

        public Timer(int secondStart, int SecondStop, Operation operation, DispatcherTimer timer, Action<int>? action, Action start, Action stop)
        {
            _SecondStart = secondStart;
            _SecondStop = SecondStop;
            _operation = operation;
            _timer = timer;
            TickChanged = action;
            OnStart = start;
            OnStop = stop;
        }

        public void startTime()
        {
            OnStart?.Invoke();
            TickChanged?.Invoke(_SecondStart);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Tick;
            _timer.Start();

        }

        private void Tick(object? sender, EventArgs e)
        {
            if (_operation == Operation.Minus)
            {
                _SecondStart--;
                TickChanged?.Invoke(_SecondStart);
                if (_SecondStart == _SecondStop)
                {
                    _timer.Stop();
                    OnStop?.Invoke();

                }
            }

        }
    }
}
