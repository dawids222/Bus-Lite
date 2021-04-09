using Bus_Lite.Buses;
using Bus_Lite.Events;
using Bus_Lite.Handlers;
using Bus_Lite.Listeners;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bus_Lite
{
    public class EventBus
    {
        private ListenerEventBus ListenerEventBus { get; } = new ListenerEventBus();
        private HandlerEventBus HandlerEventBus { get; } = new HandlerEventBus();

        public IEnumerable<IEventListener> Listeners { get => ListenerEventBus.Listeners; }
        public IEnumerable<IEventListener> Handlers { get => HandlerEventBus.Listeners; }

        public SubscriptionToken Subscribe<T>(object owner, Action<T> callback)
        {
            return ListenerEventBus.Subscribe(owner, callback);
        }

        public SubscriptionToken Subscribe<T>(object owner, IEventHandler<T> handler)
        {
            return ListenerEventBus.Subscribe(owner, handler);
        }

        public SubscriptionToken Register<TEvent, TResult>(object owner, Func<TEvent, Task<TResult>> callback) where TEvent : IEvent<TResult>
        {
            return HandlerEventBus.Register(owner, callback);
        }

        public SubscriptionToken Register<TEvent, TResult>(object owner, IEventHandler<TEvent, TResult> handler) where TEvent : IEvent<TResult>
        {
            return HandlerEventBus.Register(owner, handler);
        }

        public void Remove(object owner)
        {
            ListenerEventBus.Remove(owner);
            HandlerEventBus.Remove(owner);
        }

        public void Remove(SubscriptionToken token)
        {
            ListenerEventBus.Remove(token);
            HandlerEventBus.Remove(token);
        }

        public void Notify(object @event)
        {
            ListenerEventBus.Notify(@event);
        }

        public async Task<TResult> Handle<TResult>(IEvent<TResult> @event)
        {
            return await HandlerEventBus.Handle(@event);
        }
    }
}
