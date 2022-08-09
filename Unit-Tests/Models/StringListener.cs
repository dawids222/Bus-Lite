using LibLite.Bus.Lite.Contract;

namespace LibLite.Bus.Lite.Tests.Models
{
    internal class StringListener : IEventListener<string>
    {
        public void OnNotify(string @event) { }
    }
}
