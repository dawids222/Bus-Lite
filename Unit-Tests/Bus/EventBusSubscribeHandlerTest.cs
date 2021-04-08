using Bus_Lite;
using Bus_Lite.Exceptions;
using Bus_Lite.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unit_Tests.Models;

namespace Unit_Tests.Bus
{
    [TestClass]
    public class EventBusSubscribeHandlerTest : EventBusBaseTest
    {
        private IEventHandler<string> Handler { get; set; }

        [TestInitialize]
        public override void Before()
        {
            base.Before();
            Handler = new StringHandler();
        }

        [TestMethod]
        public void AddsListener()
        {
            EventBus.Subscribe(this, Handler);

            Assert.AreEqual(1, EventBus.Listeners.Count());
            Assert.AreEqual(this, EventBus.Listeners.ElementAt(0).Owner);
        }

        [TestMethod]
        public void AddsMultipleListeners()
        {
            EventBus.Subscribe(this, Handler);
            EventBus.Subscribe(this, Handler);
            EventBus.Subscribe(this, Handler);
            EventBus.Subscribe(this, Handler);
            EventBus.Subscribe(this, Handler);

            Assert.AreEqual(5, EventBus.Listeners.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(SubscriptionTokenOwnerException))]
        public void SubscriptionTokenCanNotBeAnOwner()
        {
            var token = EventBus.Subscribe(this, Handler);
            EventBus.Subscribe(token, Handler);
        }

        [TestMethod]
        [ExpectedException(typeof(NullHandlerException))]
        public void HandlerCanNotBeNull()
        {
            Handler = null;
            EventBus.Subscribe(this, Handler);
        }

        [TestMethod]
        public void ReturnsSubscriptionToken()
        {
            var token = EventBus.Subscribe(this, Handler);

            Assert.IsTrue(token is SubscriptionToken);
        }
    }
}
