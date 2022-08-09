using System.Threading.Tasks;

namespace LibLite.Bus.Lite.Contract
{
    public interface IEventHandler<TEvent, TResult>
    {
        Task<TResult> Handle(TEvent @event);
    }
}
