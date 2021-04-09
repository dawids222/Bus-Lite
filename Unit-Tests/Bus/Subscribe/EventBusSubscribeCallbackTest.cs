using Bus_Lite;
using Bus_Lite.Exceptions;
using Bus_Lite.Listeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Unit_Tests.Bus.Subscribe
{
    [TestClass]
    public class EventBusSubscribeCallbackTest : EventBusSubscribeBaseTest
    {
        protected override IEnumerable<IEventObserver> Observers => EventBus.Listeners;

        protected override ObserverToken SubscribeToBus()
        {
            return SubscribeToBus(this);
        }

        protected override ObserverToken SubscribeToBus(object owner)
        {
            return EventBus.Subscribe<string>(owner, (x) => { });
        }

        [TestMethod]
        [ExpectedException(typeof(NullObserverException))]
        public void CallbackCanNotBeNull()
        {
            Action<string> callback = null;
            EventBus.Subscribe(this, callback);
        }
    }
}
