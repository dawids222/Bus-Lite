namespace Bus_Lite.Contract
{
    public interface IEventListener<TEvent>
    {
        void OnNotify(TEvent @event);
    }
}
