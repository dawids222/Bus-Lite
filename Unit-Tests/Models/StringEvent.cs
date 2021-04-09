using Bus_Lite.Contract;

namespace Unit_Tests.Models
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
