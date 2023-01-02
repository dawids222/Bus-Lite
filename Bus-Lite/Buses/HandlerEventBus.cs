using LibLite.Bus.Lite.Contract;
using LibLite.Bus.Lite.Exceptions;
using LibLite.Bus.Lite.Observers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibLite.Bus.Lite.Buses
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
            Add<TEvent>(handler);
            return handler.Token;
        }

        public Task<TResult> Handle<TResult>(IEvent<TResult> @event)
        {
            lock (LockObj)
            {
                var exists = _observers.TryGetValue(@event.GetType(), out var observers);
                if (!exists) { throw new HandlerNotRegisteredException(); }
                var observer = observers.First();
                return (Task<TResult>)observer.Invoke(@event);
            }
        }
    }
}
