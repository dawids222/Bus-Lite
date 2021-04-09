using Bus_Lite.Exceptions;
using Bus_Lite.Handlers;
using Bus_Lite.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bus_Lite.Buses
{
    internal class ListenerEventBus : BaseEventBus
    {
        public SubscriptionToken Subscribe<T>(object owner, Action<T> callback)
        {
            if (callback is null) { throw new NullHandlerException(); }
            return SubscribeListener(owner, callback);
        }

        public SubscriptionToken Subscribe<T>(object owner, IEventHandler<T> handler)
        {
            if (handler is null) { throw new NullHandlerException(); }
            return SubscribeListener<T>(owner, handler.Handle);
        }

        private SubscriptionToken SubscribeListener<T>(object owner, Action<T> callback)
        {
            if (owner is SubscriptionToken) { throw new SubscriptionTokenOwnerException(); }
            var listener = new ActionEventListener<T>(owner, callback);
            lock (LockObj) { _listeners.Add(listener); }
            return listener.Token;
        }

        public void Notify(object @event)
        {
            var listeners = GetListenersForEvent(@event);
            listeners.ForEach(listener => listener.Handle(@event));
        }

        private List<IEventListener> GetListenersForEvent(object @event)
        {
            lock (LockObj)
                return _listeners
                    .Where(listener => listener.ShouldHandle(@event))
                    .ToList();
        }
    }
}
