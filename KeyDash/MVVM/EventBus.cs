using KeyDash.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDash.MVVM
{
    public class EventBus : IEventBus
    {
        private Dictionary<Type, List<Action<object>>> subscribers  = new Dictionary<Type, List<Action<object>>>();
        public void Publish<T>(T message)
        {
            var type = typeof(T);
            if (subscribers.ContainsKey(type))
            {
                foreach (var subscriber in subscribers[type])
                {
                    subscriber.Invoke(message);
                }
            }
        }

        public void Subcribe<T>(Action<T?> action)
        {
            var type = typeof(T);
            if (!subscribers.ContainsKey(type))
            {
                subscribers[type] = new List<Action<object>>();
            }
            subscribers[type].Add(obj => action((T)obj));
        }
        public void UnSubscribe<T>(Action<T> action)
        {
            var type = typeof (T);
            if (subscribers.ContainsKey(type))
            {
                subscribers[type].Remove(obj => action((T)obj));
            }
        }
    }
}
