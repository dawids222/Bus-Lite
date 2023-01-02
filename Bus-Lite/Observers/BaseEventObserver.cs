using LibLite.Bus.Lite.Listeners;

namespace LibLite.Bus.Lite.Observers
{
    public abstract class BaseEventObserver<TEvent> : IEventObserver
    {
        public object Owner { get; }
        public ObserverToken Token { get; }

        public BaseEventObserver(object owner)
        {
            Owner = owner;
            Token = new ObserverToken();
        }

        public abstract object Invoke(object @event);
    }
}
