using System;

namespace Bus_Lite.Exceptions
{
    public class SubscriptionTokenOwnerException : Exception
    {
        private const string ERROR_MESSAGE = "Object of type SubscriptionToken can not be an owner of an event listener";
        public SubscriptionTokenOwnerException() : base(ERROR_MESSAGE) { }
    }
}
