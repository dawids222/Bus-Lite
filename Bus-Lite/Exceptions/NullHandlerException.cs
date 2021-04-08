using System;

namespace Bus_Lite.Exceptions
{
    public class NullHandlerException : Exception
    {
        private const string ERROR_MESSAGE = "Subscribed NULL as an event handler";
        public NullHandlerException() : base(ERROR_MESSAGE) { }
    }
}
