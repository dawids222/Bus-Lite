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
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Unsubscribe(token);

            Assert.AreEqual(1, EventBus.Listeners.Count());
        }

        [TestMethod]
        public void RemovesAllListenersFromOwner()
        {
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>("", (x) => { });

            EventBus.Unsubscribe(this);

            Assert.AreEqual(1, EventBus.Listeners.Count());
            Assert.AreEqual("", EventBus.Listeners.ElementAt(0).Owner);
        }
    }
}
