using Bus_Lite;
using Bus_Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests.Bus
{
    [TestClass]
    public class EventBusSubscribeCallbackTest : EventBusSubscribeBaseTest
    {
        protected override SubscriptionToken SubscribeToBus()
        {
            return EventBus.Subscribe<string>(this, (x) => { });
        }

        [TestMethod]
        [ExpectedException(typeof(NullHandlerException))]
        public void CallbackCanNotBeNull()
        {
            Action<string> callback = null;
            EventBus.Subscribe(this, callback);
        }
    }
}
