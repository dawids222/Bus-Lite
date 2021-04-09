using Bus_Lite.Contract;

namespace Unit_Tests.Models
{
    internal class StringHandler : IEventListener<string>
    {
        public void OnNotify(string @event) { }
    }
}
