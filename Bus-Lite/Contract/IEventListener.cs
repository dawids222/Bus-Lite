namespace LibLite.Bus.Lite.Contract
{
    public interface IEventListener<TEvent>
    {
        void OnNotify(TEvent @event);
    }
}
