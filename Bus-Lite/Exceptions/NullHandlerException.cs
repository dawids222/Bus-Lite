using System;

namespace Bus_Lite.Exceptions
{
    public class NullObserverException : Exception
    {
        private const string ERROR_MESSAGE = "Subscribed NULL as an event observer";
        public NullObserverException() : base(ERROR_MESSAGE) { }
    }
}
