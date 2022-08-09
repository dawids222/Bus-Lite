using LibLite.Bus.Lite.Contract;
using System.Threading.Tasks;

namespace LibLite.Bus.Lite.Tests.Models
{
    internal class StringHandler : IEventHandler<StringEvent, string>
    {
        public async Task<string> Handle(StringEvent @event)
        {
            return await Task.FromResult(@event.Value);
        }
    }
}
