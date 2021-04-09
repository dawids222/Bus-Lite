using Bus_Lite.Contract;

namespace Unit_Tests.Models
{
    internal class StringListener : IEventListener<string>
    {
        public void OnNotify(string @event) { }
    }
}
