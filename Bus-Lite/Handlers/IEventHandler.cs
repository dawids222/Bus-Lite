using System.Threading.Tasks;

namespace Bus_Lite.Handlers
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent @event);
    }

    public interface IEventHandler<TEvent, TResult>
    {
        Task<TResult> Handle(TEvent @event);
    }
}
