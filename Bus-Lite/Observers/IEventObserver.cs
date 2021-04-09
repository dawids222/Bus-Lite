namespace Bus_Lite.Listeners
{
    public interface IEventObserver
    {
        object Owner { get; }
        ObserverToken Token { get; }
        bool ShouldInvoke(object @event);
        object Invoke(object @event);
    }
}
