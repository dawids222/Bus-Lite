using Bus_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Unit_Tests.Bus
{
    [TestClass]
    public class EventBusRemoveTest : EventBusBaseTest
    {
        [TestMethod]
        public void RemovesListenerByToken()
        {
            var token = EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Remove(token);

            Assert.AreEqual(0, EventBus.Listeners.Count());
        }

        [TestMethod]
        public void RemovesNothingWhenTokenIsNull()
        {
            EventBus.Remove(null);
        }

        [TestMethod]
        public void RemovesNothingWhenTokenIsNonExisting()
        {
            var token = new ObserverToken();
            EventBus.Remove(token);
        }

        [TestMethod]
        public void RemovesAllListenersFromOwner()
        {
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });

            EventBus.Remove(this);

            Assert.AreEqual(0, EventBus.Listeners.Count());
        }
    }
}
