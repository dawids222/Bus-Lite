using LibLite.Bus.Lite.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibLite.Bus.Lite.Buses
{
    internal class BaseEventBus
    {
        protected readonly IDictionary<Type, List<IEventObserver>> _observers = new Dictionary<Type, List<IEventObserver>>();
        public IEnumerable<IEventObserver> Observers { get => _observers.Values.SelectMany(x => x).ToList(); }

        protected object LockObj { get; } = new object();

        protected void Add<TEvent>(IEventObserver observer)
        {
            lock (LockObj)
            {
                var type = typeof(TEvent);
                if (!_observers.ContainsKey(type))
                    _observers.Add(type, new List<IEventObserver>());
                var observers = _observers[type];
                observers.Add(observer);
            }
        }

        public void Remove(object owner)
        {
            Remove(x => x.Owner == owner);
        }

        public void Remove(ObserverToken token)
        {
            Remove(x => x.Token == token);
        }

        private void Remove(Predicate<IEventObserver> predicate)
        {
            lock (LockObj)
            {
                var keysToRemove = new List<Type>();
                foreach (var observers in _observers)
                {
                    var values = observers.Value;
                    values.RemoveAll(predicate);
                    if (!values.Any())
                        keysToRemove.Add(observers.Key);
                }
                foreach (var key in keysToRemove)
                {
                    _observers.Remove(key);
                }
            }
        }
    }
}
