namespace Bus_Lite.Listeners
{
    public interface IEventListener
    {
        object Owner { get; }
        SubscriptionToken Token { get; }
        bool ShouldHandle(object @event);
        object Handle(object @event);
    }
}
