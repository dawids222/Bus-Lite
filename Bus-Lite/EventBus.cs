using LibLite.Bus.Lite.Buses;
using LibLite.Bus.Lite.Contract;
using LibLite.Bus.Lite.Listeners;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibLite.Bus.Lite
{
    public class EventBus
    {
        private ListenerEventBus ListenerEventBus { get; } = new ListenerEventBus();
        private HandlerEventBus HandlerEventBus { get; } = new HandlerEventBus();

        public IEnumerable<IEventObserver> Listeners { get => ListenerEventBus.Observers; }
        public IEnumerable<IEventObserver> Handlers { get => HandlerEventBus.Observers; }

        public ObserverToken Subscribe<TEvent>(object owner, Action<TEvent> callback)
        {
            return ListenerEventBus.Subscribe(owner, callback);
        }

        public ObserverToken Subscribe<TEvent>(object owner, IEventListener<TEvent> handler)
        {
            return ListenerEventBus.Subscribe(owner, handler);
        }

        public ObserverToken Register<TEvent, TResult>(object owner, Func<TEvent, Task<TResult>> callback) where TEvent : IEvent<TResult>
        {
            return HandlerEventBus.Register(owner, callback);
        }

        public ObserverToken Register<TEvent, TResult>(object owner, IEventHandler<TEvent, TResult> handler) where TEvent : IEvent<TResult>
        {
            return HandlerEventBus.Register(owner, handler);
        }

        public void Remove(object owner)
        {
            ListenerEventBus.Remove(owner);
            HandlerEventBus.Remove(owner);
        }

        public void Remove(ObserverToken token)
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
