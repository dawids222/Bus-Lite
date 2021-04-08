namespace Bus_Lite.Listeners
{
    public abstract class BaseEventListener<TEvent> : IEventListener
    {
        public object Owner { get; }
        public SubscriptionToken Token { get; }

        public BaseEventListener(object owner)
        {
            Owner = owner;
            Token = new SubscriptionToken();
        }

        public virtual bool ShouldHandle(object @event)
        {
            return @event is TEvent;
        }

        public abstract object Handle(object @event);
    }
}
