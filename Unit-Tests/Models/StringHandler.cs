using Bus_Lite.Handlers;

namespace Unit_Tests.Models
{
    internal class StringHandler : IEventHandler<string>
    {
        public void Handle(string @event) { }
    }
}
