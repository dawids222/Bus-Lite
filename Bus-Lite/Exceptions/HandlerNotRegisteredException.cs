using System;

namespace Bus_Lite.Exceptions
{
    public class HandlerNotRegisteredException : Exception
    {
        private const string ERROR_MESSAGE = "Handler for this event could not be found. Try registering your handlers first";

        public HandlerNotRegisteredException() : base(ERROR_MESSAGE) { }
    }
}
