using System.Threading.Tasks;

namespace Bus_Lite.Contract
{
    public interface IEventHandler<TEvent, TResult>
    {
        Task<TResult> Handle(TEvent @event);
    }
}
