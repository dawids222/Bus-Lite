using LibLite.Bus.Lite.Listeners;
using System.Collections.Generic;
using System.Linq;

namespace LibLite.Bus.Lite.Buses
{
    internal class BaseEventBus
    {
        protected readonly List<IEventObserver> _observers = new List<IEventObserver>();
        public IEnumerable<IEventObserver> Observers { get => _observers; }

        protected object LockObj { get; } = new object();

        public void Remove(object owner)
        {
            lock (LockObj)
                _observers.RemoveAll(x => x.Owner == owner);
        }

        public void Remove(ObserverToken token)
        {
            lock (LockObj)
                _observers.Remove(GetListener(token));
        }

        private IEventObserver GetListener(ObserverToken token)
        {
            return _observers.FirstOrDefault(x => x.Token == token);
        }
    }
}
