using Bus_Lite.Exceptions;
using Bus_Lite.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bus_Lite
{
    public class EventBus
    {
        private readonly List<IEventListener> _listeners = new List<IEventListener>();
        public IEnumerable<IEventListener> Listeners { get => _listeners; }

        private object LockObj { get; } = new object();

        public SubscriptionToken Subscribe<T>(object owner, Action<T> callback)
        {
            lock (LockObj)
                return SubscribeInner(owner, callback);
        }

        private SubscriptionToken SubscribeInner<T>(object owner, Action<T> callback)
        {
            if (owner is SubscriptionToken) { throw new SubscriptionTokenOwnerException(); }
            var listener = new GenericEventListener<T>(owner, callback);
            _listeners.Add(listener);
            return listener.Token;
        }

        public void Unsubscribe(object owner)
        {
            lock (LockObj)
                _listeners.RemoveAll(x => x.Owner == owner);
        }

        public void Unsubscribe(SubscriptionToken token)
        {
            lock (LockObj)
                _listeners.Remove(GetListener(token));
        }

        private IEventListener GetListener(SubscriptionToken token)
        {
            return _listeners.FirstOrDefault(x => x.Token == token);
        }

        public void Push(object @event)
        {
            lock (LockObj)
                PushInner(@event);
        }

        private void PushInner(object @event)
        {
            _listeners.ForEach(listener =>
            {
                if (listener.ShouldHandle(@event))
                {
                    listener.Handle(@event);
                }
            });
        }
    }
}
