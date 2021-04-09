using System;

namespace Bus_Lite.Exceptions
{
    public class ObserverTokenOwnerException : Exception
    {
        private const string ERROR_MESSAGE = "Object of type 'ObserverToken' can not be an owner of an event observer";
        public ObserverTokenOwnerException() : base(ERROR_MESSAGE) { }
    }
}
