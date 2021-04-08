using Bus_Lite.Events;
using Bus_Lite.Exceptions;
using Bus_Lite.Handlers;
using Bus_Lite.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bus_Lite
{
    public class EventBus
    {
        private readonly List<IEventListener> _listeners = new List<IEventListener>();
        public IEnumerable<IEventListener> Listeners { get => _listeners; }

        private object LockObj { get; } = new object();

        public SubscriptionToken Subscribe<T>(object owner, Action<T> callback)
        {
            if (callback is null) { throw new NullHandlerException(); }
            return SubscribeActionEventListener(owner, callback);
        }

        public SubscriptionToken Subscribe<T>(object owner, IEventHandler<T> handler)
        {
            if (handler is null) { throw new NullHandlerException(); }
            return SubscribeActionEventListener<T>(owner, handler.Handle);
        }

        private SubscriptionToken SubscribeActionEventListener<T>(object owner, Action<T> callback)
        {
            if (owner is SubscriptionToken) { throw new SubscriptionTokenOwnerException(); }
            var listener = new ActionEventListener<T>(owner, callback);
            lock (LockObj) { _listeners.Add(listener); }
            return listener.Token;
        }

        public SubscriptionToken Subscribe<TEvent, TResult>(object owner, Func<TEvent, Task<TResult>> callback) where TEvent : IEvent<TResult>
        {
            if (callback is null) { throw new NullHandlerException(); }
            return SubscribeFuncEventListener(owner, callback);
        }

        public SubscriptionToken Subscribe<TEvent, TResult>(object owner, IEventHandler<TEvent, TResult> handler) where TEvent : IEvent<TResult>
        {
            if (handler is null) { throw new NullHandlerException(); }
            return SubscribeFuncEventListener<TEvent, TResult>(owner, handler.Handle);
        }

        private SubscriptionToken SubscribeFuncEventListener<TEvent, TResult>(object owner, Func<TEvent, Task<TResult>> callback)
        {
            if (owner is SubscriptionToken) { throw new SubscriptionTokenOwnerException(); }
            var listener = new FuncEventListener<TEvent, Task<TResult>>(owner, callback);
            lock (LockObj) { _listeners.Add(listener); }
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

        public async Task<TResult> Send<TResult>(IEvent<TResult> @event)
        {
            Task<TResult> task;
            lock (LockObj)
                task = (Task<TResult>)_listeners
                    .FirstOrDefault(listener =>
                        listener.ShouldHandle(@event)
                    )
                    ?.Handle(@event)
                    ?? throw new Exception("Listener for this event could not be found");
            return await task;
        }
    }
}
