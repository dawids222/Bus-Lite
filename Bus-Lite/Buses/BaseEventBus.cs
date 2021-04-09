using Bus_Lite.Listeners;
using System.Collections.Generic;
using System.Linq;

namespace Bus_Lite.Buses
{
    internal class BaseEventBus
    {
        protected readonly List<IEventListener> _listeners = new List<IEventListener>();
        public IEnumerable<IEventListener> Listeners { get => _listeners; }

        protected object LockObj { get; } = new object();

        public void Remove(object owner)
        {
            lock (LockObj)
                _listeners.RemoveAll(x => x.Owner == owner);
        }

        public void Remove(SubscriptionToken token)
        {
            lock (LockObj)
                _listeners.Remove(GetListener(token));
        }

        private IEventListener GetListener(SubscriptionToken token)
        {
            return _listeners.FirstOrDefault(x => x.Token == token);
        }
    }
}
