﻿using System;
using System.Collections.Generic;

namespace Bus_Lite
{
    public class EventBus
    {
        private List<IEventListener> _listeners { get; } = new List<IEventListener>();
        public IEnumerable<IEventListener> Listeners { get => _listeners; }

        public SubscriptionToken Subscribe<T>(object owner, Action<T> callback)
        {
            var token = new SubscriptionToken();
            var listener = new GenericEventListener<T>(owner, token, callback);
            _listeners.Add(listener);
            return token;
        }

        public void Unsubscribe(object owner)
        {
            _listeners.RemoveAll(x => x.Owner == owner);
        }

        public void Unsubscribe(SubscriptionToken token)
        {
            _listeners.RemoveAll(x => x.Token == token);
        }

        public void Push(object @event)
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