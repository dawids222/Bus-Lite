using Bus_Lite;
using Bus_Lite.Exceptions;
using Bus_Lite.Listeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Unit_Tests.Bus.Subscribe
{
    [TestClass]
    public abstract class EventBusSubscribeBaseTest : EventBusBaseTest
    {
        protected abstract ObserverToken SubscribeToBus();
        protected abstract ObserverToken SubscribeToBus(object owner);
        protected abstract IEnumerable<IEventObserver> Observers { get; }

        [TestMethod]
        public void AddsListener()
        {
            SubscribeToBus();

            Assert.AreEqual(1, Observers.Count());
            Assert.AreEqual(this, Observers.ElementAt(0).Owner);
        }

        [TestMethod]
        public void AddsMultipleListeners()
        {
            SubscribeToBus();
            SubscribeToBus();
            SubscribeToBus();
            SubscribeToBus();
            SubscribeToBus();

            Assert.AreEqual(5, Observers.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ObserverTokenOwnerException))]
        public void ObserverTokenCanNotBeAnOwner()
        {
            var token = SubscribeToBus();
            SubscribeToBus(token);
        }

        [TestMethod]
        public void ReturnsSubscriptionToken()
        {
            var token = SubscribeToBus();

            Assert.IsTrue(token is ObserverToken);
        }
    }
}
