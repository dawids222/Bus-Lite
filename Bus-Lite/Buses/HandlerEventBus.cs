using Bus_Lite.Events;
using Bus_Lite.Exceptions;
using Bus_Lite.Handlers;
using Bus_Lite.Listeners;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bus_Lite.Buses
{
    internal class HandlerEventBus : BaseEventBus
    {
        public SubscriptionToken Register<TEvent, TResult>(object owner, Func<TEvent, Task<TResult>> callback) where TEvent : IEvent<TResult>
        {
            if (callback is null) { throw new NullHandlerException(); }
            return RegisterHandler(owner, callback);
        }

        public SubscriptionToken Register<TEvent, TResult>(object owner, IEventHandler<TEvent, TResult> handler) where TEvent : IEvent<TResult>
        {
            if (handler is null) { throw new NullHandlerException(); }
            return RegisterHandler<TEvent, TResult>(owner, handler.Handle);
        }

        private SubscriptionToken RegisterHandler<TEvent, TResult>(object owner, Func<TEvent, Task<TResult>> callback)
        {
            if (owner is SubscriptionToken) { throw new SubscriptionTokenOwnerException(); }
            var listener = new FuncEventListener<TEvent, Task<TResult>>(owner, callback);
            lock (LockObj) { _listeners.Add(listener); }
            return listener.Token;
        }

        public async Task<TResult> Handle<TResult>(IEvent<TResult> @event)
        {
            Task<TResult> task;
            lock (LockObj)
                task = GetHandlersTask(@event);
            return await task;
        }

        private Task<TResult> GetHandlersTask<TResult>(IEvent<TResult> @event)
        {
            var task = _listeners.FirstOrDefault(listener =>
                listener.ShouldHandle(@event)
            );
            if (task is null) { throw new HandlerNotRegisteredException(); }
            return (Task<TResult>)task.Handle(@event);
        }
    }
}
