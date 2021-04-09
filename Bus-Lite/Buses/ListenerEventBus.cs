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
        public ObserverToken Subscribe<TEvent>(object owner, Action<TEvent> callback)
        {
            if (callback is null) { throw new NullObserverException(); }
            return SubscribeListener(owner, callback);
        }

        public ObserverToken Subscribe<TEvent>(object owner, IEventListener<TEvent> listener)
        {
            if (listener is null) { throw new NullObserverException(); }
            return SubscribeListener<TEvent>(owner, listener.OnNotify);
        }

        private ObserverToken SubscribeListener<TEvent>(object owner, Action<TEvent> callback)
        {
            if (owner is ObserverToken) { throw new ObserverTokenOwnerException(); }
            var listener = new ActionEventObserver<TEvent>(owner, callback);
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
