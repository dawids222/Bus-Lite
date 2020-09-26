﻿using Bus_Lite.Exceptions;
using Bus_Lite.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bus_Lite
{
    public class EventBus
    {
        private List<IEventListener> listeners { get; } = new List<IEventListener>();
        public IEnumerable<IEventListener> Listeners { get => listeners; }

        private object lockObj { get; } = new object();

        public SubscriptionToken Subscribe<T>(object owner, Action<T> callback)
        {
            lock (lockObj)
                return SubscribeInner(owner, callback);
        }

        private SubscriptionToken SubscribeInner<T>(object owner, Action<T> callback)
        {
            if (owner is SubscriptionToken) { throw new SubscriptionTokenOwnerException(); }
            var token = new SubscriptionToken();
            var listener = new GenericEventListener<T>(owner, token, callback);
            listeners.Add(listener);
            return token;
        }

        public void Unsubscribe(object owner)
        {
            lock (lockObj)
                listeners.RemoveAll(x => x.Owner == owner);
        }

        public void Unsubscribe(SubscriptionToken token)
        {
            lock (lockObj)
                listeners.Remove(GetListener(token));
        }

        private IEventListener GetListener(SubscriptionToken token)
        {
            return listeners.FirstOrDefault(x => x.Token == token);
        }

        public void Push(object @event)
        {
            lock (lockObj)
                PushInner(@event);
        }

        private void PushInner(object @event)
        {
            listeners.ForEach(listener =>
            {
                if (listener.ShouldHandle(@event))
                {
                    listener.Handle(@event);
                }
            });
        }
    }
}
