using Bus_Lite.Contract;
using Bus_Lite.Exceptions;
using Bus_Lite.Listeners;
using Bus_Lite.Observers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bus_Lite.Buses
{
    internal class ListenerEventBus : BaseEventBus
    {
        public ObserverToken Subscribe<T>(object owner, Action<T> callback)
        {
            if (callback is null) { throw new NullObserverException(); }
            return SubscribeListener(owner, callback);
        }

        public ObserverToken Subscribe<T>(object owner, IEventListener<T> listener)
        {
            if (listener is null) { throw new NullObserverException(); }
            return SubscribeListener<T>(owner, listener.OnNotify);
        }

        private ObserverToken SubscribeListener<T>(object owner, Action<T> callback)
        {
            if (owner is ObserverToken) { throw new ObserverTokenOwnerException(); }
            var listener = new ActionEventObserver<T>(owner, callback);
            lock (LockObj) { _observers.Add(listener); }
            return listener.Token;
        }

        public void Notify(object @event)
        {
            var listeners = GetListenersForEvent(@event);
            listeners.ForEach(listener => listener.Invoke(@event));
        }

        private List<IEventObserver> GetListenersForEvent(object @event)
        {
            lock (LockObj)
                return _observers
                    .Where(listener => listener.ShouldInvoke(@event))
                    .ToList();
        }
    }
}
