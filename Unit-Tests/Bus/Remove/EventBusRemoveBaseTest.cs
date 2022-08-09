using LibLite.Bus.Lite;
using LibLite.Bus.Lite.Listeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LibLite.Bus.Lite.Tests.Bus.Remove
{
    [TestClass]
    public abstract class EventBusRemoveBaseTest : EventBusBaseTest
    {
        protected abstract ObserverToken SubscribeToBus();
        protected abstract ObserverToken SubscribeToBus(object owner);
        protected abstract IEnumerable<IEventObserver> Observers { get; }

        [TestMethod]
        public void RemovesListenerByToken()
        {
            var token = SubscribeToBus();

            EventBus.Remove(token);

            Assert.AreEqual(0, Observers.Count());
        }

        [TestMethod]
        public void RemovesAllListenersFromOwner()
        {
            SubscribeToBus();
            SubscribeToBus();
            SubscribeToBus();

            EventBus.Remove(this);

            Assert.AreEqual(0, Observers.Count());
        }

        [TestMethod]
        public void RemovesNothingWhenTokenIsNonExisting()
        {
            SubscribeToBus();
            var token = new ObserverToken();

            EventBus.Remove(token);

            Assert.AreEqual(1, Observers.Count());
        }
    }
}
