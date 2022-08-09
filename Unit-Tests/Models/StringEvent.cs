using LibLite.Bus.Lite.Contract;

namespace LibLite.Bus.Lite.Tests.Models
{
    class StringEvent : IEvent<string>
    {
        public string Value { get; }

        public StringEvent(string value = "")
        {
            Value = value;
        }
    }
}
