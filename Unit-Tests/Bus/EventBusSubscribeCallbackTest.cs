using Bus_Lite;
using Bus_Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Unit_Tests.Bus
{
    [TestClass]
    public class EventBusSubscribeCallbackTest : EventBusBaseTest
    {
        [TestMethod]
        public void AddsListener()
        {
            EventBus.Subscribe<string>(this, (x) => { });

            Assert.AreEqual(1, EventBus.Listeners.Count());
            Assert.AreEqual(this, EventBus.Listeners.ElementAt(0).Owner);
        }

        [TestMethod]
        public void AddsMultipleListeners()
        {
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });

            Assert.AreEqual(5, EventBus.Listeners.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(SubscriptionTokenOwnerException))]
        public void SubscriptionTokenCanNotBeAnOwner()
        {
            var token = EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(token, (x) => { });
        }

        [TestMethod]
        [ExpectedException(typeof(NullHandlerException))]
        public void CallbackCanNotBeNull()
        {
            Action<string> callback = null;
            EventBus.Subscribe(this, callback);
        }

        [TestMethod]
        public void ReturnsSubscriptionToken()
        {
            var token = EventBus.Subscribe<string>(this, (x) => { });

            Assert.IsTrue(token is SubscriptionToken);
        }
    }
}
