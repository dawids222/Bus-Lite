namespace LibLite.Bus.Lite.Listeners
{
    public interface IEventObserver
    {
        object Owner { get; }
        ObserverToken Token { get; }
        object Invoke(object @event);
    }
}
