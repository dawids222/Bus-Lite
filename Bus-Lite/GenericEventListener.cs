using System;

namespace Bus_Lite
{
    public class GenericEventListener<TEvent> : IEventListener
    {
        public object Owner { get; }
        public SubscriptionToken Token { get; }
        public Action<TEvent> Callback { get; }

        public GenericEventListener(object owner, SubscriptionToken token, Action<TEvent> callback
        )
        {
            Owner = owner;
            Token = token;
            Callback = callback;
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
