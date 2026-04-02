using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDash.Abstractions
{
    public interface IEventBus
    {

        public void Subcribe<T>(Action<T?> action);
        public void Publish<T>(T type);
        public void UnSubscribe<T>(Action<T> action);
    }
}
