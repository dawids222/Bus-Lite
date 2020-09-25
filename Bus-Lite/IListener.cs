namespace Bus_Lite
{
    public interface IEventListener
    {
        object Owner { get; }
        SubscriptionToken Token { get; }
        bool ShouldHandle(object @event);
        void Handle(object @event);
    }
}
