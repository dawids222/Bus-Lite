namespace Bus_Lite.Handlers
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent @event);
    }
}
