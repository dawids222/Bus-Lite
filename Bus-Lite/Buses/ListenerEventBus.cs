using LibLite.Bus.Lite.Contract;
using LibLite.Bus.Lite.Exceptions;
using LibLite.Bus.Lite.Listeners;
using LibLite.Bus.Lite.Observers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibLite.Bus.Lite.Buses
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
            Add<TEvent>(listener);
            return listener.Token;
        }

        public void Notify(object @event)
        {
            lock (LockObj)
            {
                var exists = _observers.TryGetValue(@event.GetType(), out var observers);
                var listeners = exists
                    ? observers.ToList()
                    : new List<IEventObserver>();
                listeners.ForEach(listener => listener.Invoke(@event));
            }
        }
    }
}
