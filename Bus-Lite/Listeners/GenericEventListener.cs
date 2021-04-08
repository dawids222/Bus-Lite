using System;

namespace Bus_Lite.Listeners
{
    public class GenericEventListener<TEvent> : IEventListener
    {
        public object Owner { get; }
        public SubscriptionToken Token { get; }
        public Action<TEvent> Callback { get; }

        public GenericEventListener(object owner, Action<TEvent> callback)
        {
            Owner = owner;
            Callback = callback;
            Token = new SubscriptionToken();
        }

        public void Handle(object @event)
        {
            Callback.Invoke((TEvent)@event);
        }

        public bool ShouldHandle(object @event)
        {
            return @event is TEvent;
        }
    }
}
