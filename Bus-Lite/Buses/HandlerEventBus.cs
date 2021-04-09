using Bus_Lite.Contract;
using Bus_Lite.Exceptions;
using Bus_Lite.Observers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bus_Lite.Buses
{
    internal class HandlerEventBus : BaseEventBus
    {
        public ObserverToken Register<TEvent, TResult>(object owner, Func<TEvent, Task<TResult>> callback) where TEvent : IEvent<TResult>
        {
            if (callback is null) { throw new NullObserverException(); }
            return RegisterHandler(owner, callback);
        }

        public ObserverToken Register<TEvent, TResult>(object owner, IEventHandler<TEvent, TResult> handler) where TEvent : IEvent<TResult>
        {
            if (handler is null) { throw new NullObserverException(); }
            return RegisterHandler<TEvent, TResult>(owner, handler.Handle);
        }

        private ObserverToken RegisterHandler<TEvent, TResult>(object owner, Func<TEvent, Task<TResult>> callback)
        {
            if (owner is ObserverToken) { throw new ObserverTokenOwnerException(); }
            var handler = new FuncEventObserver<TEvent, Task<TResult>>(owner, callback);
            lock (LockObj) { _observers.Add(handler); }
            return handler.Token;
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
            var task = _observers.FirstOrDefault(listener =>
                listener.ShouldInvoke(@event)
            );
            if (task is null) { throw new HandlerNotRegisteredException(); }
            return (Task<TResult>)task.Invoke(@event);
        }
    }
}
