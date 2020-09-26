using Bus_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Unit_Tests.Bus
{
    [TestClass]
    public class EventBusUnsubscribeTest : EventBusBaseTest
    {
        [TestMethod]
        public void RemovesListenerByToken()
        {
            var token = EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Unsubscribe(token);

            Assert.AreEqual(0, EventBus.Listeners.Count());
        }

        [TestMethod]
        public void RemovesNothingWhenTokenIsNull()
        {
            EventBus.Unsubscribe(null);
        }

        [TestMethod]
        public void RemovesNothingWhenTokenIsNonExisting()
        {
            var token = new SubscriptionToken();
            EventBus.Unsubscribe(token);
        }

        [TestMethod]
        public void RemovesAllListenersFromOwner()
        {
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });

            EventBus.Unsubscribe(this);

            Assert.AreEqual(0, EventBus.Listeners.Count());
        }
    }
}
