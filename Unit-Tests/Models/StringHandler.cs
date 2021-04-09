using Bus_Lite.Contract;
using System.Threading.Tasks;

namespace Unit_Tests.Models
{
    internal class StringHandler : IEventHandler<StringEvent, string>
    {
        public async Task<string> Handle(StringEvent @event)
        {
            return await Task.FromResult(@event.Value);
        }
    }
}
